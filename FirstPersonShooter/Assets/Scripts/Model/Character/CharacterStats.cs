namespace ExampleTemplate
{
    public sealed class CharacterStats : UnitsStats
    {
        #region Fields

        public CharacterData CharacterData;

        private float _jumpPower;

        #endregion


        #region ClassLyfeCycle

        public CharacterStats()
        {
            CharacterData = Data.Instance.Character;
            _health = CharacterData.GetBaseHealth();
            _speed = CharacterData.GetBaseMovingSpeed();
            _armor = CharacterData.GetBaseArmor();
            _jumpPower = CharacterData.GetBaseJumpPower();
        }

        #endregion


        #region Properties

        public float JumpPower
        {
            get { return _jumpPower; }
        }

        #endregion


        #region Methods

        public override void ResetHealth()
        {
            _health = CharacterData.GetBaseHealth();
        }

        #endregion

    }
}