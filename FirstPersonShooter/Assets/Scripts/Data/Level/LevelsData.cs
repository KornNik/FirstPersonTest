using System;
using System.Linq;
using UnityEngine;

namespace ExampleTemplate
{
    [CreateAssetMenu(fileName = "LevelsData", menuName = "Data/Levels/LevelsData")]
    public sealed class LevelsData : ScriptableObject
    {
        #region Fields

        [SerializeField] private LevelData[] _levels;

        #endregion


        #region Methods

        private LevelData GetLevelData(LevelsType levelType)
        {
            var result = _levels.SingleOrDefault(x => x.LevelType == levelType);
            if (result == null)
                throw new ArgumentException("Нет данных для уровня " + levelType);
            return result;
        }

        public GameObject GetPrefabLevel(LevelsType levelType) => GetLevelData(levelType).LocationPrefab;
        public CharacterPosition GetCharacterPosition(LevelsType levelType) => GetLevelData(levelType).HeroPosition;
        public CharacterPosition GetEnemyPosition(LevelsType levelType) => GetLevelData(levelType).EnemyPosition;

        #endregion
    }
}