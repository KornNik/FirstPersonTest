using UnityEngine;

namespace ExampleTemplate
{
    public class LoadLevelService : Service
    {
        #region Fields

        private GameObject _currentLevel;
        private EnemiesType _enemyType;
        private CharactersType _characterType;
        private LevelsType _levelType;

        #endregion


        #region Methods

        public void LoadLevel(LevelsType levelType, EnemiesType enemyType, CharactersType characterType)
        {
            DestroyLevel();
            _levelType = levelType;
            _characterType = characterType;
            _enemyType = enemyType;
            _currentLevel = GameObject.Instantiate(Data.Instance.LevelsData.GetPrefabLevel(levelType));
            var characterPosition = Data.Instance.LevelsData.GetCharacterPosition(levelType);
            var enemyPosition = Data.Instance.LevelsData.GetEnemyPosition(levelType);
            Data.Instance.Character.Initialization(characterType, characterPosition);
            Data.Instance.EnemiesData.Initialization(enemyType, enemyPosition);
            Time.timeScale = 1;
        }

        public void LoadLevel()
        {
            LoadLevel(_levelType, _enemyType, _characterType);
        }

        public void DestroyLevel()
        {
            if (_currentLevel == null) return;
            GameObject.Destroy(_currentLevel);
            GameObject.Destroy(Data.Instance.Character.CharacterBehaviour.gameObject);
        }

        public bool IsLvlLoaded()
        {
            return _currentLevel != null;
        }

        #endregion

    }
}
