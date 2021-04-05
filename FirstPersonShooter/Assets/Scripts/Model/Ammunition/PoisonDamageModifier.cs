using UnityEngine;

namespace ExampleTemplate
{
    public class PoisonDamageModifier : AmmunitionModifier
    {
        [SerializeField] private float _poisonDamage;
        [SerializeField] private float _poisonDuration;

        public PoisonDamageModifier(IDamager modifiedDamage, float poisonDamage, float poisonDuration) : base(modifiedDamage)
        {
            _poisonDamage = poisonDamage;
            _poisonDuration = poisonDuration;
        }

        protected override void Modify()
        {
            _bulletVictim.ReceiveDamageOverTime(_poisonDamage, _poisonDuration);
        }

    }
}