using System;
using UnityEngine;

namespace ExampleTemplate
{
    [Serializable]
    public sealed class LevelData
    {
        #region Fields

        public LevelsType LevelType;
        public GameObject LocationPrefab;
        public CharacterPosition HeroPosition;
        public CharacterPosition EnemyPosition;

        #endregion
    }
}