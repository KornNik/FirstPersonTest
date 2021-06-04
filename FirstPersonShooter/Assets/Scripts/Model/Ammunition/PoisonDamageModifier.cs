using UnityEngine;

namespace ExampleTemplate
{
    public class PoisonDamageModifier : AmmunitionModifier
    {
        #region Fields

        [SerializeField] private float _poisonDamage;
        [SerializeField] private float _poisonDuration;

        #endregion

        #region ClassLyfeCycle

        public PoisonDamageModifier(IDamager modifiedDamage, float poisonDamage, float poisonDuration) : base(modifiedDamage)
        {
            _poisonDamage = poisonDamage;
            _poisonDuration = poisonDuration;
        }

        #endregion


        #region Methods

        protected override void Modify()
        {
            _bulletVictim.ReceiveDamageOverTime(_poisonDamage, _poisonDuration);
        }

        #endregion

    }
}