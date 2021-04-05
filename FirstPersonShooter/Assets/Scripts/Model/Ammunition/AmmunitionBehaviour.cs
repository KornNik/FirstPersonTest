using UnityEngine;
using System.Collections.Generic;

namespace ExampleTemplate
{
    public abstract class AmmunitionBehaviour : MonoBehaviour, IDamager
    {
        #region Fields

		[SerializeField] protected float _baseDamage = 10;
		[SerializeField] protected float _bonusDamage = 5;
		[SerializeField] protected float _poisonDamage = 1;
		[SerializeField] protected float _poisonDuration = 4;

		public AmmunitionType Type = AmmunitionType.Bullet;

		protected float _currentDamage;
        protected float _timeToDestruct = 5;
		protected float _lossOfDamageAtTime = 0.2f;

		private Rigidbody _rigidbody;
		private TrailRenderer _trailRenderer;
		private List<AmmunitionModifier> _modifiers = new List<AmmunitionModifier>();

        #endregion


        #region UnityMethods

        protected virtual void Awake()
		{
			_currentDamage = _baseDamage;
			_rigidbody = GetComponent<Rigidbody>();
			_trailRenderer = GetComponent<TrailRenderer>();
		}

        #endregion


        #region Methods

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

		public void RegisterBulletModifier(AmmunitionModifier newModifier)
		{
			_modifiers.Add(newModifier);
		}

		public void AddForce(Vector3 dir)
		{
			if (!_rigidbody) return;
			ActiveAmmunition();
			_rigidbody.AddForce(dir);
		}

		protected void LossOfDamage()
		{
			_currentDamage -= _lossOfDamageAtTime;
		}

        protected void ReturnToPool()
		{
            transform.SetParent(Services.Instance.WeaponService.Weapon.PoolTransform);
            transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			_rigidbody.velocity = Vector3.zero;
			_trailRenderer.Clear();
			gameObject.SetActive(false);
			CancelInvoke(nameof(ReturnToPool));

            if (!Services.Instance.WeaponService.Weapon.PoolTransform)
            {
                Destroy(gameObject);
            }
        }

		private void ActiveAmmunition()
		{
			_currentDamage = _baseDamage;
			gameObject.SetActive(true);
			InvokeRepeating(nameof(LossOfDamage), 0, 1);
			Invoke(nameof(ReturnToPool), _timeToDestruct);
			transform.SetParent(null);
		}

        private void OnBecameInvisible()
        {
            if (gameObject.activeSelf)
            {
                ReturnToPool();
            }
        }


        #endregion
    }
}