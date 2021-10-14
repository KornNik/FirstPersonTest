using UnityEngine;
using System.Collections.Generic;

namespace ExampleTemplate
{
    public abstract class AmmunitionBehaviour : MonoBehaviour, IDamager
    {
		#region Fields

		[HideInInspector] public Transform PoolTransform;

		public AmmunitionType Type = AmmunitionType.Bullet;

		protected float _currentDamage;
		protected AmmunitionData _ammunitionData;

		private Rigidbody _rigidbody;
		private TrailRenderer _trailRenderer;
		private List<AmmunitionModifier> _modifiers = new List<AmmunitionModifier>();

		#endregion


		#region ClassLifeCycle

		protected virtual void Awake()
		{
			_currentDamage = _ammunitionData.GetBaseDamage();
			_rigidbody = GetComponent<Rigidbody>();
			_trailRenderer = GetComponent<TrailRenderer>();
		}

		#endregion


		#region UnityMethods

		//private void OnBecameInvisible()
		//{
		//    if (gameObject.activeSelf)
		//    {
		//        ReturnToPool();
		//    }
		//}

		#endregion


		#region Methods

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
			_currentDamage -= _ammunitionData.GetLossOfDamage();
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
			_currentDamage = _ammunitionData.GetBaseDamage();
			gameObject.SetActive(true);
			InvokeRepeating(nameof(LossOfDamage), 0, 1);
			Invoke(nameof(ReturnToPool), _ammunitionData.GetTimeToDistract());
			transform.SetParent(null);
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