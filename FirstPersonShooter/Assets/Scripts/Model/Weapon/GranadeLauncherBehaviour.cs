using UnityEngine;

namespace ExampleTemplate
{
    public sealed class GranadeLauncherBehaviour : WeaponBehaviour
	{
        #region UnityMethods

        protected override void Awake()
		{
            _countAmmunition = 5;
            base.Awake();

		}

        #endregion


        #region Methods

        public override void Fire()
		{
			if (!_isReady) return;
			if (Clip.CountAmmunition <= 0) return;
			if (AmmunitionPool == null) return;

            _shootDirection = SetSpread(_barrel.forward);

            var tempAmmunition = AmmunitionPool.GetAmmunition(AmmunitionType.Granade);
            tempAmmunition.AddForce(_shootDirection * _force);

            Clip.CountAmmunition--;
			_isReady = false;
			Invoke(nameof(ReadyShoot), _rechergeTime);
		}

        #endregion

    }
}
