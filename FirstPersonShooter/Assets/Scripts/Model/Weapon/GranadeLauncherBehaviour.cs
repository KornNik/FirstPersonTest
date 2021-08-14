namespace ExampleTemplate
{
    public sealed class GranadeLauncherBehaviour : WeaponBehaviour
	{
        #region UnityMethods

        protected override void Awake()
		{
            _weaponData = Data.Instance.GranadeLauncherData;
            base.Awake();
            _weaponVFX = new WeaponVFX(_barrel, VFXType.MuzzleFlash);
            _isMufflerModificated = true;
            _clipModification.IncreasAmmo = 4;
            _weaponType = WeaponType.GranadeLauncher;
            _ammunitionType = AmmunitionType.Granade;
		}

        #endregion


        #region Methods

        public override void Fire()
		{
			if (!_isReady) return;
			if (Clip.CountAmmunition <= 0) return;
			if (_ammunitionPool == null) return;

            _shootDirection = SetSpread(_barrel.forward);

            var tempAmmunition = _ammunitionPool.GetAmmunition(_ammunitionType);
            tempAmmunition.AddForce(_shootDirection * _force);
            _weaponVFX.PlayWeaponParticle(_barrel.position);
            WeaponRecoil();

            Clip.CountAmmunition--;
			_isReady = false;
			Invoke(nameof(ReadyShoot), _rechergeTime);
		}

        #endregion

    }
}
