﻿using System;
using UnityEngine;
using System.Collections.Generic;

namespace ExampleTemplate
{
    public abstract class WeaponBehaviour : MonoBehaviour
    {
        #region Fields

        [SerializeField] protected Transform _barrel;
        [SerializeField] protected Transform _placeForClip;
        [SerializeField] protected Transform _placeForMuffler;
        [SerializeField] protected float _force;
        [SerializeField] protected float _rechergeTime;
        [SerializeField] protected float _spreadFactor;

        public static Action FireActn;

        public Clip Clip;
        public Transform PoolTransform;

        protected int _countAmmunition;
        protected int _countClip;
        protected bool _isReady = true;
        protected bool _isClipModificated = false;
        protected bool _isMufflerModificated = false;

        protected Vector3 _shootDirection;
        protected WeaponData _weaponData;
        protected AmmunitionPool _ammunitionPool;
        protected ClipModification _clipModification;
        protected MufflerModification _mufflerModification;
        protected AmmunitionType[] _ammunitionType = { AmmunitionType.Bullet };

        private bool _isVisible;
        private Queue<Clip> _clips = new Queue<Clip>();

        #endregion


        #region Properties

        public Transform PlaceForClip => _placeForClip;
        public Transform PlaceForMuffler => _placeForMuffler;

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
            _ammunitionPool = new AmmunitionPool(8);
            _mufflerModification = new MufflerModification();
            _clipModification = new ClipModification();

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

        }

        #endregion


        #region Methods

        protected void ReadyShoot()
        {
            _isReady = true;
        }

        protected Vector3 SetSpread(Vector3 barrelDirection)
        {
            var shootDirection = barrelDirection;
            shootDirection.z += UnityEngine.Random.Range(-_spreadFactor, _spreadFactor);
            shootDirection.y += UnityEngine.Random.Range(-_spreadFactor, _spreadFactor);

            return shootDirection;
        }

        protected void AddClip(Clip clip)
        {
            _clips.Enqueue(clip);
        }

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

        #endregion
    }
}
