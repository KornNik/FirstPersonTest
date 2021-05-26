namespace ExampleTemplate
{
    public sealed class GranadeLauncherBehaviour : WeaponBehaviour
	{
        #region UnityMethods

        protected override void Awake()
		{
            _weaponData = Data.Instance.GranadeLauncherData;
            base.Awake();
		}

        #endregion


        #region Methods

        public override void Fire()
		{
			if (!_isReady) return;
			if (Clip.CountAmmunition <= 0) return;
			if (_ammunitionPool == null) return;

            _shootDirection = SetSpread(_barrel.forward);

            var tempAmmunition = _ammunitionPool.GetAmmunition(AmmunitionType.Granade);
            tempAmmunition.AddForce(_shootDirection * _force);

            Clip.CountAmmunition--;
			_isReady = false;
			Invoke(nameof(ReadyShoot), _rechergeTime);
		}

        #endregion

    }
}
