namespace ExampleTemplate
{
    public sealed class GranadeLauncherBehaviour : WeaponBehaviour
	{
        #region UnityMethods

        protected override void Awake()
		{
            _weaponData = Data.Instance.GranadeLauncherData;
            _ammunitionData = Data.Instance.ExplosionAmmunitionData;
            base.Awake();
            _weaponVFX = new WeaponVFX(_barrel, VFXType.MuzzleFlash);
            _weaponType = WeaponType.GranadeLauncher;
            _ammunitionType = AmmunitionType.Granade;
		}

        #endregion


        #region Methods

        public override void Fire()
        {
            if (!_isReady) return;
            if (_clip.CountAmmunition <= 0) return;
            if (_ammunitionPool == null) return;

            _shootDirection = SetSpread(_barrel.forward);
            var tempAmmunition = _ammunitionPool.GetAmmunition(_ammunitionType);
            tempAmmunition.ShootBullet(_shootDirection * _force);
            _weaponVFX.PlayWeaponParticle(_barrel.position);
            _weaponRecoil.MakeRecoil();

            _clip.CountAmmunition--;
            _isReady = false;
            Invoke(nameof(ReadyShoot), _rechergeTime);
        }

        #endregion

    }
}
