namespace ExampleTemplate
{
    public abstract class UnitsStats
    {
        #region Fields

        protected float _health;
        protected float _speed;
        protected float _armor;

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

        #endregion


        #region Methods

        public virtual void TakeDamage(float damage)
        {
            _health -= damage / Armor;
        }
        public abstract void ResetHealth();

        #endregion
    }
}