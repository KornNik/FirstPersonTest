using UnityEngine;
using System.Collections.Generic;

namespace ExampleTemplate
{
    public abstract class AmmunitionBehaviour : MonoBehaviour, IDamager
    {
		#region Fields

		[HideInInspector] public Transform PoolTransform;

		protected AmmunitionType _type = AmmunitionType.Bullet;

		protected float _currentDamage;

		protected AmmunitionData _ammunitionData;

		private float _startTime;

		private Rigidbody _rigidbody;
		private TrailRenderer _trailRenderer;
		private List<AmmunitionModifier> _modifiers = new List<AmmunitionModifier>();

		#endregion


		#region UnityMethods

		protected virtual void Awake()
		{
			_rigidbody = GetComponent<Rigidbody>();
			_trailRenderer = GetComponent<TrailRenderer>();
			_currentDamage = _ammunitionData.GetBaseDamage();
		}



		#endregion


		#region Methods

		public void RegisterBulletModifier(AmmunitionModifier newModifier)
		{
			_modifiers.Add(newModifier);
		}

        public void ShootBullet(Vector3 direction)
        {
            if (!_rigidbody) return;
            ActiveAmmunition();
            _rigidbody.AddForce(direction);
        }

        protected void ReturnToPool()
		{
            transform.SetParent(PoolTransform);
            transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			_rigidbody.velocity = Vector3.zero;
			_trailRenderer.Clear();
			gameObject.SetActive(false);
			CancelInvoke(nameof(ReturnToPool));

            if (!PoolTransform)
            {
                Destroy(gameObject);
            }
        }

		private void ActiveAmmunition()
		{
			_startTime = Time.time;
			gameObject.SetActive(true);
            Invoke(nameof(ReturnToPool), _ammunitionData.GetTimeToDistract());
			transform.SetParent(null);
		}

		float GetDamageCoefficient()
		{
			float value = 1.0f;
			float CurrentTime = Time.time - _startTime;
			value = _ammunitionData.GetDamageReductionGraph().Evaluate(CurrentTime / _ammunitionData.GetTimeToDistract());

			return value;
		}

		#endregion


		#region IDamager

		public void InflictDamage(IDamageable victim)
		{
			for (int i = 0; i < _modifiers.Count; i++)
			{
				_modifiers[i].InflictDamage(victim);
			}
			victim.ReceiveDamage(_currentDamage * GetDamageCoefficient());
			Debug.Log(_currentDamage * GetDamageCoefficient());
		}

		public void AddDamage(float extraDamage)
		{
			_currentDamage += extraDamage;
		}

		#endregion
	}
}