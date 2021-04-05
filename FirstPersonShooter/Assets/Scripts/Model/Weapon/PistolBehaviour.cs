using UnityEngine;

namespace ExampleTemplate
{
	public sealed class PistolBehaviour : WeaponBehaviour
	{

        #region UnityMethods

        protected override void Awake()
		{
			base.Awake();
		}

		#endregion


		#region Methods

		public override void Fire()
		{
			if (!_isReady) return;
			if (Clip.CountAmmunition <= 0) return;
			if (AmmunitionPool == null) return;
			_shootDirection = _barrel.forward;
			_shootDirection.z += Random.Range(-_spreadFactor, _spreadFactor);
			_shootDirection.y += Random.Range(-_spreadFactor, _spreadFactor);
			var tempAmmunition = AmmunitionPool.GetAmmunition(AmmunitionType.Bullet);
			tempAmmunition.AddForce(_shootDirection * _force);
			OnFire();
			Clip.CountAmmunition--;
			_isReady = false;
			Invoke(nameof(ReadyShoot), _rechergeTime);
		}

        #endregion
    }
}