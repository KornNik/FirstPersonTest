using UnityEngine;

namespace ExampleTemplate
{
    public sealed class CharacterStats
    {
        #region Fields

        public CharacterData CharacterData;

        private float _health;
        private float _speed;
        private float _armor;
        private float _jumpPower;

        #endregion


        #region ClassLyfeCycle

        public CharacterStats()
        {
            CharacterData = Data.Instance.Character;
            _health = CharacterData.GetBaseHealth();
            _speed = CharacterData.GetBaseSpeed();
            _armor = CharacterData.GetBaseArmor();
            _jumpPower = CharacterData.GetBaseJumpPower();
        }

        #endregion


        #region Properties

        public float Health 
        { 
            get { return _health; }
        }
        public float Speed 
        { 
            get { return _speed; }
        }
        public float Armor 
        { 
            get { return _armor; } 
        }
        public float JumpPower
        {
            get { return _jumpPower; }
        }

        #endregion


        #region Methods 

        public void TakeDamage(float damage)
        {
            _health -= damage / Armor;
        }
        public void ResetHealth()
        {
            _health = CharacterData.GetBaseHealth();
        }

        #endregion

    }
}