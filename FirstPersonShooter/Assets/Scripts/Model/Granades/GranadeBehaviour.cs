using UnityEngine;
using System.Collections.Generic;

namespace ExampleTemplate
{
    public abstract class GranadeBehaviour : MonoBehaviour, IDamager
    {
        #region Fields

        public Rigidbody Rigidbody;

        protected GranadeData _granadeData;
        protected ParticleSystem _particleSystem;
        protected Transform _parentTransform;

        protected float _currentDamage;
        protected GranadeType _type = GranadeType.Poison;
        protected bool _isReady;

        private bool _isVisible;
        private bool _isColliderActive;
        private List<AmmunitionModifier> _modifiers = new List<AmmunitionModifier>();

        #endregion


        #region Properties

        public bool IsReady { get { return _isReady; } }

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

        public bool IsColliderActive
        {
            get => _isColliderActive;
            set
            {
                _isColliderActive = value;
                var tempCollider = GetComponent<Collider>();
                if (tempCollider)
                {
                    tempCollider.enabled = _isColliderActive;
                    if (transform.childCount <= 0) return;
                    foreach (Transform item in transform)
                    {
                        tempCollider = item.GetComponentInChildren<Collider>();
                        if (tempCollider)
                        {
                            tempCollider.enabled = _isColliderActive;
                        }
                    }
                }
            }
        }

        #endregion

        #region UnityMethods

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();

            _particleSystem = GetComponent<ParticleSystem>();
            _granadeData = Data.Instance.GranadeData;
            _parentTransform = Services.Instance.CameraServices.CameraMain.transform;

            _currentDamage = _granadeData.GetDamage();
            ReadyThrow();
        }

        #endregion


        #region Methods
        public abstract void Throw();
        public void RegisterBulletModifier(AmmunitionModifier newModifier)
        {
            _modifiers.Add(newModifier);
        }

        protected void AddForce(Vector3 direction)
        {
            if (!Rigidbody) return;
            Rigidbody.isKinematic = false;
            Rigidbody.AddForce(direction*600);

        }

        protected void PlayParticle()
        {
           _particleSystem.Emit(SetParticle(Color.green, transform.position), 1);
        }

        protected void ReadyThrow()
        {
            _isReady = true;
            gameObject.transform.SetParent(_parentTransform);
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.identity;
            Rigidbody.velocity = Vector3.zero;
            Rigidbody.isKinematic = true;
        }

        private ParticleSystem.EmitParams SetParticle(Color color,Vector3 position)
        {
            var emitParams = new ParticleSystem.EmitParams
            {
                startColor = color,
                position = position,
                applyShapeToPosition = true,
                startSize3D = new Vector3(_granadeData.GetRadius(), _granadeData.GetRadius(), _granadeData.GetRadius())
            };
            return emitParams;
        }

        #endregion

        #region IDamager

        public void InflictDamage(IDamageable victim)
        {
            for (int i = 0; i < _modifiers.Count; i++)
            {
                _modifiers[i].InflictDamage(victim);
            }
            victim.ReceiveDamage(_currentDamage);
        }

        public void AddDamage(float extraDamage)
        {
            _currentDamage += extraDamage;
        }

        #endregion
    }
}