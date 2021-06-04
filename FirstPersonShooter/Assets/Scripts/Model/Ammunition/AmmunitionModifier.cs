namespace ExampleTemplate
{
    public abstract class AmmunitionModifier : IDamager
    {
        #region Fields

        protected IDamager _modifiedDamage;
        protected IDamageable _bulletVictim;

        #endregion


        #region ClassLifeCycles

        public AmmunitionModifier(IDamager modifiedDamage)
        {
            _modifiedDamage = modifiedDamage;
        }

        #endregion


        #region Methods
        protected abstract void Modify();

        #endregion


        #region IDamager

        public void InflictDamage(IDamageable victim)
        {
            if (!_modifiedDamage.Equals(null))
            {
                _bulletVictim = victim;
                Modify();
            }
        }

        public void AddDamage(float bonusDamage) { }

        #endregion
    }
}