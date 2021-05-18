using System;

namespace ExampleTemplate
{
    public sealed class WeaponService
	{
        #region Fields

        public static Action<int, int> AmmunitionChanged;

        private WeaponBehaviour _weapon;
		private bool _isWeapon;

		#endregion


		#region Properties

		public WeaponBehaviour Weapon { get { return _weapon; } private set { } }
		public bool IsWeapon { get { return _isWeapon; }private set { } }

        #endregion


        #region Methods

        public void On(WeaponBehaviour weapon)
		{
			if (_isWeapon) return;
			_isWeapon = true;
			_weapon = weapon as WeaponBehaviour;
			if (_weapon == null) return;
			_weapon.IsVisible = true;
			AmmunitionChanged?.Invoke(_weapon.CountClip, _weapon.Clip.CountAmmunition);
		}

		public void Off()
		{
			if (!_isWeapon) return;
			_isWeapon = false;
			_weapon.IsVisible = false;
		}

		public void ReloadClip()
		{
			if (_weapon == null) return;
			_weapon.ReloadClip();
			AmmunitionChanged?.Invoke(_weapon.CountClip, _weapon.Clip.CountAmmunition);
		}

        #endregion
    }
}
