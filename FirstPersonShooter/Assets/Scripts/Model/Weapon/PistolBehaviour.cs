﻿namespace ExampleTemplate
{
	public sealed class PistolBehaviour : WeaponBehaviour
	{

		#region UnityMethods

		protected override void Awake()
		{
			_weaponData = Data.Instance.PistolData;
			base.Awake();
			_weaponType = WeaponType.Pistol;
			_ammunitionType = AmmunitionType.Bullet;
            _weaponVFX = new WeaponVFX(_barrel, VFXType.MuzzleFlash);
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
			tempAmmunition.AddForce(_shootDirection * _force);
			FireActn?.Invoke();
            _weaponVFX.PlayWeaponParticle(_barrel.position);

            _clip.CountAmmunition--;
			_isReady = false;
			Invoke(nameof(ReadyShoot), _rechergeTime);
		}

        #endregion
    }
}