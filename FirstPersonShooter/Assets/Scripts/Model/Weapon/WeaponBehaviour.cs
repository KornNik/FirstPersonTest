using System;
using UnityEngine;
using System.Collections.Generic;

namespace ExampleTemplate
{
    public abstract class WeaponBehaviour : MonoBehaviour
    {
        #region Fields

        public static Action FireActn;

        public Clip Clip;
        public Transform PoolTransform;
        public AmmunitionPool AmmunitionPool;
        public bool IsClipModificated = false;
        public bool IsMufflerModificated = false;

		protected int _countAmmunition = 30;
		protected int _countClip = 5;
        protected bool _isReady = true;
        protected Vector3 _shootDirection;
        protected CharacterData _characterData;
        protected AmmunitionType[] _ammunitionType = { AmmunitionType.Bullet };

        private bool _isVisible;
        private Queue<Clip> _clips = new Queue<Clip>();

        [SerializeField] protected Transform _barrel;
        [SerializeField] protected Transform _placeForClip;
        [SerializeField] protected Transform _placeForMuffler;
        [SerializeField] protected float _force = 700;
        [SerializeField] protected float _rechergeTime = 0.2f;
        [SerializeField] protected float _spreadFactor = 0.02f;

        #endregion


        #region Properties

        public Transform Barrel { get { return _barrel; } private set { } }

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
            for (var i = 0; i <= _countClip; i++)
            {
                AddClip(new Clip { CountAmmunition = _countAmmunition });
            }

            ReloadClip();

            AmmunitionPool = new AmmunitionPool(8);
            _characterData = Data.Instance.Character;

        }

        #endregion


        #region Methods

        protected void ReadyShoot()
        {
            _isReady = true;
            _barrel.forward = _characterData.CameraBehaviuor.transform.forward;
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

        public Transform MufflerTransform()
        {
            return _placeForMuffler;
        }
        public Transform ClipTransform()
        {
            return _placeForClip;
        }
        public abstract void Fire();

        #endregion
    }
}
