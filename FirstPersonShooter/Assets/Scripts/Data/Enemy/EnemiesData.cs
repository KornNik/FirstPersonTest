using UnityEngine;
using System.Collections.Generic;

namespace ExampleTemplate
{
    [CreateAssetMenu(fileName = "Enemies", menuName = "Data/Enemies/EnemiesData")]
    public class EnemiesData : ScriptableObject
    {
        #region Fields

        [SerializeField] private int _health;
        [SerializeField] private float _distanceView;
        [SerializeField] public int EnemyCount = 5;

        [HideInInspector] public EnemyBehaviour EnemyBehaviour;
        [HideInInspector] public HashSet<EnemyBehaviour> GetBotList { get; } = new HashSet<EnemyBehaviour>();

        #endregion


        #region Methods

        public void Initialization(EnemiesType enemyType, CharacterPosition point)
        {
            if (EnemyCount <= 0) return;
            var enemyBehaviour = CustomResources.Load<EnemyBehaviour>
                (AssetsPathEnemies.EnemiesGameObject[enemyType]);
            for (int index = 0; index < EnemyCount; index++)
            {
                EnemyBehaviour = Instantiate(enemyBehaviour, Patrol.GenericPoint(point.Position), point.Rotation());
                EnemyBehaviour.Agent.avoidancePriority = index;
                AddBotToList(EnemyBehaviour);
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

        public int GetHealth()
        {
            return _health;
        }
        public float GetDistanceView()
        {
            return _distanceView;
        }

        #endregion
    }
}
