using UnityEngine;

namespace ExampleTemplate
{
    public class InventoryWeapons
    {

        #region Fields

        private WeaponBehaviour[] _weapons = new WeaponBehaviour[5];
        private int _selectIndexWeapon = 0;

        #endregion


        #region Properties

        public WeaponBehaviour[] Weapons { get { return _weapons; } private set { } }

        #endregion


        #region UnityMethods

        public InventoryWeapons(CharacterBehaviour character)
        {
            _weapons = character.GetComponentsInChildren<WeaponBehaviour>();
            foreach (var weapon in Weapons)
            {
                weapon.IsVisible = false;
            }
        }

        #endregion


        #region Methods

        public WeaponBehaviour SelectWeapon(int weaponNumber)
        {
            if (weaponNumber < 0 || weaponNumber >= Weapons.Length) return null;
            var tempWeapon = _weapons[weaponNumber];
            return tempWeapon;
        }

        public WeaponBehaviour SelectWeapon(MouseScrollWheel scrollWheel)
        {
            if (scrollWheel == MouseScrollWheel.Up)
            {
                if (_selectIndexWeapon < Weapons.Length - 1)
                {
                    _selectIndexWeapon++;
                }
                else
                {
                    _selectIndexWeapon = -1;
                }
                return SelectWeapon(_selectIndexWeapon);
            }

            if (_selectIndexWeapon <= 0)
            {
                _selectIndexWeapon = Weapons.Length;
            }
            else
            {
                _selectIndexWeapon--;
            }
            return SelectWeapon(_selectIndexWeapon);
        }

        #endregion
    }
}