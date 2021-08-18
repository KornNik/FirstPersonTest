namespace ExampleTemplate
{
    public class Inventory
    {

        #region Fields

        private WeaponBehaviour[] _weapons = new WeaponBehaviour[5];
        private GranadeBehaviour[] _granades = new GranadeBehaviour[5];
        private FlashLightBehaviour _flashLight;
        private int _selectIndexWeapon = 0;
        private int _selectIndexGranade = 0;


        #endregion


        #region Properties

        public WeaponBehaviour[] Weapons { get { return _weapons; } private set { } }
        public FlashLightBehaviour FlashLight { get { return _flashLight; } private set { } }

        public GranadeBehaviour[] Granades { get { return _granades; } private set { } }

        #endregion


        #region UnityMethods

        public Inventory(CharacterBehaviour character)
        {
            _flashLight = character.GetComponentInChildren<FlashLightBehaviour>();
            _weapons = character.GetComponentsInChildren<WeaponBehaviour>();
            _granades = character.GetComponentsInChildren<GranadeBehaviour>();

            foreach (var weapon in Weapons)
            {
                weapon.IsVisible = false;
            }
            foreach (var granade in Granades)
            {
                granade.IsVisible = false;
                granade.IsColliderActive = false;
            }

            _flashLight.Switch(false);

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

        public GranadeBehaviour SelectGranade()
        {
            if (_selectIndexGranade < Granades.Length - 1)
            {
                _selectIndexGranade++;
            }
            else
            {
                _selectIndexGranade = -1;
            }
            return SelectGranade(_selectIndexGranade);
        }
        public GranadeBehaviour SelectGranade(int granadeNumber)
        {
            if (granadeNumber < 0 || granadeNumber >= Granades.Length) return null;
            var tempGranade = _granades[granadeNumber];
            return tempGranade;
        }
        #endregion
    }
}