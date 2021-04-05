using UnityEngine;

namespace ExampleTemplate
{
    public class BonusDamageModifier : AmmunitionModifier
    {
        #region Fields

        [SerializeField] private float _bonusDamage;

        #endregion


        #region ClassLifeCycles

        public BonusDamageModifier(IDamager modifiedDamage, float bonusDamage) : base(modifiedDamage)
        {
            _bonusDamage = bonusDamage;
        }

        #endregion


        #region Methods

        protected override void Modify()
        {
            _modifiedDamage.AddDamage(_bonusDamage);
        }

        #endregion

    }
}