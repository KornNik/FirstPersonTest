using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ExampleTemplate
{
    public abstract class WeaponBehaviour : MonoBehaviour
    {
        #region Fields

        [Header("Transforms")]
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

        protected Vector3 _shootDirection;

        protected WeaponType _weaponType;
        protected AmmunitionType _ammunitionType;

        protected Clip _clip;
        protected WeaponVFX _weaponVFX;
        protected WeaponData _weaponData;
        protected WeaponRecoil _weaponRecoil;
        protected AmmunitionPool _ammunitionPool;
        protected WeaponCrosshair _weaponCrosshair;

        protected List<IWeaponModification> _weaponModifications = new List<IWeaponModification>();

        private bool _isVisible;
        private Queue<Clip> _clips = new Queue<Clip>();

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
            _weaponRecoil = new WeaponRecoil(this, _weaponData,transform);

            FillData();

            for (var i = 0; i <= _countClip; i++)
            {
                AddClip(new Clip { CountAmmunition = _countAmmunition });
            }

            ReloadClip();
            AddAvailableModification();
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
            foreach (var item in _weaponModifications)
            {
                item.AddModification(this);
            }
        }

        protected virtual void AddAvailableModification()
        {
            _weaponModifications.Add(new ClipModification());
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
        private void FillData()
        {
            _countAmmunition = _weaponData.GetCountAmmunition();
            _countClip = _weaponData.GetCountClip();
            _force = _weaponData.GetBulletForce();
            _rechergeTime = _weaponData.GetRechergeTime();
            _spreadFactor = _weaponData.GetSpreadFactor();
        }

        #endregion
    }
}
