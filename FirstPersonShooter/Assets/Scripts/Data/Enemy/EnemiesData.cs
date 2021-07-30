using UnityEngine;
using System.Collections.Generic;

namespace ExampleTemplate
{
    [CreateAssetMenu(fileName = "Enemies", menuName = "Data/Enemies/EnemiesData")]
    public class EnemiesData : UnitsData
    {
        #region Fields

        [SerializeField] private int _enemyCount = 5;
        [SerializeField] private bool _isAggressive = true;
        [SerializeField] private float _distanceView = 20f;
        [SerializeField] private float _shootingDistance = 10f;
        [SerializeField] private float _shootingDelay = 2f;

        [HideInInspector] public HashSet<EnemyAi> GetAiList { get; } = new HashSet<EnemyAi>();

        #endregion


        #region Methods

        public void Initialization(EnemiesType enemyType, CharacterPosition point)
        {
            if (_enemyCount <= 0) return;
            var enemyAi = CustomResources.Load<EnemyAi>
                (AssetsPathEnemies.EnemiesGameObject[enemyType]);
            for (int index = 0; index < _enemyCount; index++)
            {
                var enemy = Instantiate(enemyAi, Patrol.GenericPoint(point.Position), point.Rotation());
                enemy.Agent.avoidancePriority = index;
                AddBotToList(enemy);
            }
        }

        private void AddBotToList(EnemyAi bot)
        {
            if (!GetAiList.Contains(bot))
            {
                GetAiList.Add(bot);
            }
        }
        public void RemoveBotToList(EnemyAi bot)
        {
            if (GetAiList.Contains(bot))
            {
                GetAiList.Remove(bot);
            }
        }

        public float GetDistanceView()
        {
            return _distanceView;
        }
        public bool GetIsAggressive()
        {
            return _isAggressive;
        }
        public float GetShootingDistance()
        {
            return _shootingDistance;
        }
        public float GetShootingDelay()
        {
            return _shootingDelay;
        }

        #endregion
    }
}
