using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ExampleTemplate
{
    public abstract class WeaponBehaviour : MonoBehaviour
    {
        #region Fields

        [SerializeField] protected Transform _barrel;
        [SerializeField] protected Transform _placeForClip;
        [SerializeField] protected Transform _placeForMuffler;
        [SerializeField] protected Transform _crosshair;

        [SerializeField] private Transform _poolTransform;

        public Action FireActn;
        public Action ReloadActn;


        protected float _force;
        protected float _rechergeTime;
        protected float _spreadFactor;
        protected int _countAmmunition;
        protected int _countClip;
        protected bool _isReady = true;
        protected bool _isClipModificated = false;
        protected bool _isMufflerModificated = false;

        protected Vector3 _weaponRecoil;
        protected Vector3 _shootDirection;
        protected Vector3 _weaponOriginPosition;

        protected Clip _clip;
        protected WeaponVFX _weaponVFX;
        protected WeaponData _weaponData;
        protected WeaponType _weaponType;
        protected AmmunitionType _ammunitionType;
        protected AmmunitionPool _ammunitionPool;
        protected WeaponCrosshair _weaponCrosshair;
        protected List<IWeaponModification> _weaponModifications = new List<IWeaponModification>();

        private bool _isVisible;
        private Queue<Clip> _clips = new Queue<Clip>();

        private Coroutine _recoilCoroutine;

        float yvelocity = 0.0f;
        float xvelocity = 0.0f;
        float zvelocity = 0.0f;

        #endregion


        #region Properties

        public Transform PlaceForClip => _placeForClip;
        public Transform PlaceForMuffler => _placeForMuffler;
        public Transform Barrel => _barrel;
        public WeaponCrosshair WeaponCrosshair => _weaponCrosshair;
        public Clip Clip => _clip;

        public int CountClip => _clips.Count;

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                var tempRenderer = GetComponent<Renderer>();
                if (tempRenderer)
                    tempRenderer.enabled = _isVisible;
                if (transform.childCount <= 0) return;
                foreach (Transform item in transform)
                {
                    tempRenderer = item.GetComponentInChildren<Renderer>();
                    if (tempRenderer)
                        tempRenderer.enabled = _isVisible;
                }
            }
        }

        #endregion


        #region UnityMethods

        protected virtual void Awake()
        {

            _ammunitionPool = new AmmunitionPool(5, _poolTransform);
            _weaponCrosshair = new WeaponCrosshair(_barrel, _crosshair);

            _countAmmunition = _weaponData.GetCountAmmunition();
            _countClip = _weaponData.GetCountClip();
            _force = _weaponData.GetBulletForce();
            _rechergeTime = _weaponData.GetRechergeTime();
            _spreadFactor = _weaponData.GetSpreadFactor();

            for (var i = 0; i <= _countClip; i++)
            {
                AddClip(new Clip { CountAmmunition = _countAmmunition });
            }

            ReloadClip();

            _weaponOriginPosition = transform.localEulerAngles;

        }

        #endregion


        #region Methods

        public void AddAmmunition(int ammo)
        {
            var countClip = _clips.Count;
            _clips.Clear();
            for (var i = 0; i <= countClip; i++)
            {
                AddClip(new Clip { CountAmmunition = _countAmmunition + ammo });
            }
            ReloadClip();
            WeaponService.AmmunitionChanged?.Invoke(CountClip, _clip.CountAmmunition);
        }
        public void RemoveSpread(float value)
        {
            _spreadFactor -= value;
        }
        public void ReloadClip()
        {
            if (CountClip <= 0) return;
            _clip = _clips.Dequeue();
        }

        public abstract void Fire();

        public void ModificateWeapon()
        {
            _weaponModifications.Add(new ClipModification());
            _weaponModifications.Add(new MufflerModification());
            foreach (var item in _weaponModifications)
            {
                item.AddModification(this);
            }
        }

        protected void WeaponRecoil()
        {
            _weaponRecoil = new Vector3(UnityEngine.Random.Range(-_weaponData.GetWeaponRecoilX(), _weaponData.GetWeaponRecoilX()),
                UnityEngine.Random.Range(0, _weaponData.GetWeaponRecoilY()), 0);
            transform.localEulerAngles -= _weaponRecoil;
            if (_recoilCoroutine == null)
            {
                _recoilCoroutine = StartCoroutine(nameof(RecoilReturn));
            }
        }
        protected void ReadyShoot()
        {
            _isReady = true;
        }
        protected Vector3 SetSpread(Vector3 barrelDirection)
        {
            barrelDirection.z += UnityEngine.Random.Range(-_spreadFactor, _spreadFactor);
            barrelDirection.y += UnityEngine.Random.Range(-_spreadFactor, _spreadFactor);

            return barrelDirection;
        }
        protected void AddClip(Clip clip)
        {
            _clips.Enqueue(clip);
        }

        private void ReturnFromRecoil()
        {

            //float x = Mathf.LerpAngle(transform.localEulerAngles.x, _weaponOriginPosition.x, Time.deltaTime * _weaponData.GetRecoilTimeMultiplier());
            //float y = Mathf.LerpAngle(transform.localEulerAngles.y, _weaponOriginPosition.y, Time.deltaTime * _weaponData.GetRecoilTimeMultiplier());
            //float z = Mathf.LerpAngle(transform.localEulerAngles.z, _weaponOriginPosition.z, Time.deltaTime * _weaponData.GetRecoilTimeMultiplier());
            //transform.localEulerAngles = new Vector3(x, y, z);

            float x = Mathf.SmoothDampAngle(transform.localEulerAngles.x, _weaponOriginPosition.x, ref xvelocity, _weaponData.GetRecoilTimeMultiplier());
            float y = Mathf.SmoothDampAngle(transform.localEulerAngles.y, _weaponOriginPosition.y, ref yvelocity, _weaponData.GetRecoilTimeMultiplier());
            float z = Mathf.SmoothDampAngle(transform.localEulerAngles.z, _weaponOriginPosition.z, ref zvelocity, _weaponData.GetRecoilTimeMultiplier());
            transform.localEulerAngles = new Vector3(x, y, z);
        }

        #endregion


        #region IEnumarator

        private IEnumerator RecoilReturn()
        {
            while (!Mathf.Approximately(_weaponOriginPosition.x, transform.localEulerAngles.x) && !Mathf.Approximately(_weaponOriginPosition.y, transform.localEulerAngles.y))
            {
                yield return _weaponData.GetReturnRecoilDelay();
                ReturnFromRecoil();
            }
            _recoilCoroutine = null;
        }

        #endregion
    }
}
