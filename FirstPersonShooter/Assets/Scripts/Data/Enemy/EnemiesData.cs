using UnityEngine;

namespace ExampleTemplate
{
    [CreateAssetMenu(fileName = "Enemies", menuName = "Data/Enemies/EnemiesData")]
    public class EnemiesData : ScriptableObject
    {
        #region Fields

        [SerializeField] private int _health;
        [SerializeField] private float _distanceView;

        [HideInInspector] public EnemyBehaviour EnemyBehaviour;

        #endregion


        #region Methods

        public void Initialization(EnemiesType enemyType, CharacterPosition point)
        {
            var enemyBehaviour = CustomResources.Load<EnemyBehaviour>
                (AssetsPathEnemies.EnemiesGameObject[enemyType]);
            EnemyBehaviour = Instantiate(enemyBehaviour, point.Position, point.Rotation());
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
