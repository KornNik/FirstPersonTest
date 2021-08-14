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
        [SerializeField] protected float _force;
        [SerializeField] protected float _rechergeTime;
        [SerializeField] protected float _spreadFactor;

        public Action FireActn;
        public Action ReloadActn;

        public Clip Clip;
        public Transform PoolTransform;
        public WeaponCrosshair WeaponCrosshair;

        protected int _countAmmunition;
        protected int _countClip;
        protected bool _isReady = true;
        protected bool _isClipModificated = false;
        protected bool _isMufflerModificated = false;

        protected Vector3 _weaponRecoil;
        protected Vector3 _shootDirection;
        protected Vector3 _weaponOriginPosition;

        protected WeaponData _weaponData;
        protected AmmunitionType _ammunitionType;
        protected WeaponType _weaponType;
        protected AmmunitionPool _ammunitionPool;
        protected ClipModification _clipModification;
        protected MufflerModification _mufflerModification;
        protected WeaponVFX _weaponVFX;

        private bool _isVisible;
        private Queue<Clip> _clips = new Queue<Clip>();

        private Coroutine _recoilCoroutine;

        #endregion


        #region Properties

        public Transform PlaceForClip => _placeForClip;
        public Transform PlaceForMuffler => _placeForMuffler;
        public Transform Barrel => _barrel;

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

            _ammunitionPool = new AmmunitionPool(8,PoolTransform);
            _mufflerModification = new MufflerModification();
            _clipModification = new ClipModification();
            WeaponCrosshair = new WeaponCrosshair(_barrel,_crosshair);

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
            WeaponService.AmmunitionChanged?.Invoke(CountClip, Clip.CountAmmunition);
        }
        public void RemoveSpread(float value)
        {
            _spreadFactor -= value;
        }
        public void ReloadClip()
        {
            if (CountClip <= 0) return;
            Clip = _clips.Dequeue();
        }

        public abstract void Fire();

        public void AddModifications()
        {
            AddMufflerModificaton();
            AddClipModification();
        }
        public void AddMufflerModificaton()
        {
            if (!_isClipModificated)
            {
                var clip = _clipModification.AddModification(this);
                _isClipModificated = true;
            }
        }
        public void AddClipModification()
        {
            if (!_isMufflerModificated)
            {
                var muffler = _mufflerModification.AddModification(this);
                _isMufflerModificated = true;
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
        //protected void WeaponRecoil()
        //{
        //    var elapsed = 0f;
        //    var duration = 0.1f;
        //    var magnitude = 5f;

        //    Vector2 noizeStartPoint0 = UnityEngine.Random.insideUnitCircle * 10;
        //    Vector2 noizeStartPoint1 = UnityEngine.Random.insideUnitCircle * 10;

        //    Vector2 currentNoizePoint0 = Vector2.Lerp(noizeStartPoint0, Vector2.zero, elapsed / duration);
        //    Vector2 currentNoizePoint1 = Vector2.Lerp(noizeStartPoint1, Vector2.zero, elapsed / duration);

        //    _weaponRecoil = new Vector3(Mathf.PerlinNoise(currentNoizePoint0.x, currentNoizePoint0.y), Mathf.PerlinNoise(currentNoizePoint1.x, currentNoizePoint1.y), 0);
        //    _weaponRecoil *= magnitude;

        //    transform.localEulerAngles -= _weaponRecoil;
        //    if (_recoilCoroutine == null)
        //    {
        //        _recoilCoroutine = StartCoroutine(nameof(RecoilReturn));
        //    }
        //}
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
            float x = Mathf.LerpAngle(transform.localEulerAngles.x, _weaponOriginPosition.x, Time.deltaTime * _weaponData.GetRecoilTimeMultiplier());
            float y = Mathf.LerpAngle(transform.localEulerAngles.y, _weaponOriginPosition.y, Time.deltaTime * _weaponData.GetRecoilTimeMultiplier());
            float z = Mathf.LerpAngle(transform.localEulerAngles.z, _weaponOriginPosition.z, Time.deltaTime * _weaponData.GetRecoilTimeMultiplier());
            transform.localEulerAngles = new Vector3(x, y, z);
        }

        #endregion

        #region IEnumarator

        private IEnumerator RecoilReturn()
        {
            while (transform.localEulerAngles != _weaponOriginPosition)
            {
                yield return _weaponData.GetReturnRecoilDelay();
                ReturnFromRecoil();
            }
        }

        #endregion
    }
}
