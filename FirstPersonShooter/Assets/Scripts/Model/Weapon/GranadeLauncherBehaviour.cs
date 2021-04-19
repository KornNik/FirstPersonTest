﻿using UnityEngine;

namespace ExampleTemplate
{
    public sealed class GranadeLauncherBehaviour : WeaponBehaviour
	{
		protected override void Awake()
		{
            _countAmmunition = 5;
            base.Awake();

		}

		public override void Fire()
		{
			if (!_isReady) return;
			if (Clip.CountAmmunition <= 0) return;
			if (AmmunitionPool == null) return;
			_shootDirection = _barrel.forward;
            _shootDirection.z += Random.Range(-_spreadFactor, _spreadFactor);
            _shootDirection.y += Random.Range(-_spreadFactor, _spreadFactor);
            var tempAmmunition = AmmunitionPool.GetAmmunition(AmmunitionType.Granade);
            tempAmmunition.AddForce(_shootDirection * _force);
            Clip.CountAmmunition--;
			_isReady = false;
			Invoke(nameof(ReadyShoot), _rechergeTime);
		}
	}
}