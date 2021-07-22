namespace ExampleTemplate
{
    public sealed class EnemyStats : UnitsStats
    {
        #region Fields

        public EnemiesData EnemyData;

        private float _distanceView;

        #endregion


        #region ClassLyfeCycle

        public EnemyStats()
        {
            EnemyData = Data.Instance.EnemiesData;
            _health = EnemyData.GetBaseHealth();
            _speed = EnemyData.GetBaseMovingSpeed();
            _armor = EnemyData.GetBaseArmor();
            _distanceView = EnemyData.GetDistanceView();
        }

        #endregion


        #region Properties

        public float DistanceView
        {
            get { return _distanceView; }
        }

        #endregion

        #region Methods

        public override void ResetHealth()
        {
            _health = EnemyData.GetBaseHealth();
        }

        #endregion
    }
}