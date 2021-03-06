namespace ExampleTemplate
{
    public sealed class EnemyStats : UnitsStats
    {
        #region Fields

        private EnemiesData _enemyData;

        private float _distanceView;

        #endregion

        #region Properties

        public EnemiesData EnemiesData => _enemyData;

        #endregion

        #region ClassLyfeCycle

        public EnemyStats()
        {
            _enemyData = Data.Instance.EnemiesData;
            _health = _enemyData.GetBaseHealth();
            _speed = _enemyData.GetBaseMovingSpeed();
            _armor = _enemyData.GetBaseArmor();
            _distanceView = _enemyData.GetDistanceView();
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
            _health = _enemyData.GetBaseHealth();
        }

        #endregion
    }
}