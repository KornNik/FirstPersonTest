using UnityEngine;
using System.Collections.Generic;

namespace ExampleTemplate
{
    [CreateAssetMenu(fileName = "Enemies", menuName = "Data/Enemies/EnemiesData")]
    public class EnemiesData : UnitsData
    {
        #region Fields

        [SerializeField] private int _enemyCount = 5;
        [SerializeField] private float _distanceView = 20f;
        [SerializeField] private float _shootingDistance = 10f;
        [SerializeField] private float _shootingDelay = 2f;
        [SerializeField] private bool _isAggressive = false;

        [HideInInspector] public HashSet<EnemyBehaviour> GetBotList { get; } = new HashSet<EnemyBehaviour>();

        #endregion


        #region Methods

        public void Initialization(EnemiesType enemyType, CharacterPosition point)
        {
            if (_enemyCount <= 0) return;
            var enemyAi = CustomResources.Load<EnemyBehaviour>
                (AssetsPathEnemies.EnemiesGameObject[enemyType]);
            for (int index = 0; index < _enemyCount; index++)
            {
                var enemy = Instantiate(enemyAi, Patrol.GenericPoint(point.Position), point.Rotation());
                enemy.EnemyAi.Agent.avoidancePriority = index;
                AddBotToList(enemy);
            }
        }

        private void AddBotToList(EnemyBehaviour bot)
        {
            if (!GetBotList.Contains(bot))
            {
                GetBotList.Add(bot);
            }
        }
        public void RemoveBotToList(EnemyBehaviour bot)
        {
            if (GetBotList.Contains(bot))
            {
                GetBotList.Remove(bot);
            }
        }

        public float GetDistanceView()
        {
            return _distanceView;
        }
        public float GetShootingDistance()
        {
            return _shootingDistance;
        }
        public float GetShootingDelay()
        {
            return _shootingDelay;
        }

        public bool GetAggressive()
        {
            return _isAggressive;
        }

        #endregion
    }
}
