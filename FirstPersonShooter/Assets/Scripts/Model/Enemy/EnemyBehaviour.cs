using System;
using UnityEngine;

namespace ExampleTemplate
{
    [RequireComponent(typeof(EnemyAi))]
    public sealed class EnemyBehaviour : UnitsBehaviour
    {
        #region Fields

        public static event Action<float> EnemyHealthChanged;

        [HideInInspector] public EnemyAi EnemyAi;
        [HideInInspector] public EnemyStats EnemyStats;

        public Transform RightHandTarget;

        private LevelsData _levelsData;
        private EnemiesData _enemiesData;
        private TextRendererParticleSystem _textParticle;

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();

            EnemyStats = new EnemyStats();

            _levelsData = Data.Instance.LevelsData;
            _enemiesData = Data.Instance.EnemiesData;
            EnemyAi = GetComponent<EnemyAi>();

            var textParticle = CustomResources.Load<TextRendererParticleSystem>
                (AssetsPathParticles.ParticlesGameObject[VFXType.TextParticle]);
            _textParticle = Instantiate(textParticle, transform.position, transform.rotation, transform);

            _unitsData = _enemiesData;
            _unitsStats = EnemyStats;
        }

        #endregion


        #region Methods

        public override void Move(Vector3 vectorMove)
        {
            throw new NotImplementedException();
        }
        public void Die()
        {
            Die(_enemiesData);
        }

        protected override void Die(UnitsData unitsData)
        {
            base.Die(_enemiesData);
            EnemyAi.Agent.ResetPath();
        }
        protected override void Respawn()
        {
            base.Respawn();
            EnemyAi.StateBot = StateBotType.None;
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

            _textParticle.SpawnParticle(transform.position, EnemyStats.Health, Color.red);
            EnemyHealthChanged?.Invoke(_unitsStats.Health / _unitsData.GetBaseHealth());

            if (_unitsStats.Health <= 0 && _isAlive)
            {
                _isDead = true;
                _isAlive = false;
                EnemyAi.StateBot = StateBotType.Died;
            }
        }

        #endregion
    }
}
