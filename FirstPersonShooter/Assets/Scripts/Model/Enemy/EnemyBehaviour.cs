using System;
using UnityEngine;

namespace ExampleTemplate
{
    [RequireComponent(typeof(EnemyAi))]
    public sealed class EnemyBehaviour : UnitsBehaviour
    {
        #region Fields

        [SerializeField] private Transform _rightHandTarget;

        public static event Action<float> EnemyHealthChanged;

        private EnemyAi _enemyAi;
        private EnemyStats _enemyStats;
        private LevelsData _levelsData;
        private TextRendererParticleSystem _textParticle;

        #endregion


        #region Properties

        public EnemyAi EnemyAi => _enemyAi;
        public EnemyStats EnemyStats => _enemyStats;
        public Transform RightHandTarget => _rightHandTarget;

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();

            _enemyStats = new EnemyStats();

            _levelsData = Data.Instance.LevelsData;
            _enemyAi = GetComponent<EnemyAi>();

            var textParticle = CustomResources.Load<TextRendererParticleSystem>
                (AssetsPathParticles.ParticlesGameObject[VFXType.TextParticle]);
            _textParticle = Instantiate(textParticle, transform.position, transform.rotation, transform);

            _unitsData = _enemyStats.EnemiesData;
            _unitsStats = _enemyStats;
        }

        #endregion


        #region Methods

        public void Die()
        {
            Die(_enemyStats.EnemiesData);
        }

        protected override void Die(UnitsData unitsData)
        {
            base.Die(_enemyStats.EnemiesData);
            _enemyAi.Agent.ResetPath();
        }
        protected override void Respawn()
        {
            base.Respawn();
            _enemyAi.StateBot = StateBotType.None;
            EnemyHealthChanged?.Invoke(_unitsStats.Health / _unitsData.GetBaseHealth());
            Revive?.Invoke();
        }
        protected override void SetRespawnPoint()
        {
            transform.position = Patrol.GenericPoint(_levelsData.GetEnemyPosition(LevelsType.TestLevel).Position);
            transform.rotation = _levelsData.GetEnemyPosition(LevelsType.TestLevel).Rotation();
        }

        #endregion


        #region IDamageable

        public override void ReceiveDamage(float damage)
        {
            base.ReceiveDamage(damage);

            _textParticle.SpawnParticle(transform.position, _enemyStats.Health, Color.red);
            EnemyHealthChanged?.Invoke(_unitsStats.Health / _unitsData.GetBaseHealth());

            if (_unitsStats.Health <= 0 && _isAlive)
            {
                _isDead = true;
                _isAlive = false;
                _enemyAi.StateBot = StateBotType.Died;
            }
        }

        #endregion
    }
}
