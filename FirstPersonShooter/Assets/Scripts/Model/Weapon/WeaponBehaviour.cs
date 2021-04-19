using System;
using UnityEngine;
using System.Collections.Generic;

namespace ExampleTemplate
{
    public abstract class WeaponBehaviour : MonoBehaviour
    {
        #region Fields

        public Clip Clip;
        public Transform PoolTransform;
        public AmmunitionPool AmmunitionPool;
        public static Action OnFire;

		protected int _countAmmunition = 30;
		protected int _countClip = 5;
        protected bool _isReady = true;
        protected AmmunitionType[] _ammunitionType = { AmmunitionType.Bullet };
        protected Vector3 _shootDirection;
        protected CharacterData _characterData;

        private Queue<Clip> _clips = new Queue<Clip>();
        private bool _isVisible;
        public bool _isClipModificated = false;
        public bool _isMufflerModificated = false;

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
                foreach (Transform d in transform)
                {
                    tempRenderer = d.gameObject.GetComponentInChildren<Renderer>();
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
            WeaponService.OnAmmunitionChange?.Invoke(CountClip, Clip.CountAmmunition);
        }

        public void RemoveSpread(float value)
        {
            _spreadFactor -= value;
        }
        public abstract void Fire();

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

        #endregion
    }
}
