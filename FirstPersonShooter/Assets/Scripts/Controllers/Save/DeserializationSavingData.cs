
namespace ExampleTemplate
{
    public class DeserializationSavingData
    {
        #region Fields

        private SaveData _myData;
        private CharacterData _characterData;
        private EnemiesData _enemiesData;
        private LevelsData _levelsData;

        private ISaveSystem<SaveData> _saveSystem;

        #endregion


        #region ClassLifeSycle

        public DeserializationSavingData()
        {
            _myData = new SaveData();
            _saveSystem = new JsonService<SaveData>();
            _levelsData = Data.Instance.LevelsData;
            _characterData = Data.Instance.Character;
            _enemiesData = Data.Instance.EnemiesData;
        }

        #endregion


        #region Methods

        public void Save()
        {
            _myData.Level = "test";
            _myData.serializableGameObjects.Add(new SerializableGameObject
            {
                Name = _characterData.CharacterBehaviour.name,
                Position = _characterData.CharacterBehaviour.transform.position,
                Rotation = _characterData.CharacterBehaviour.transform.rotation,
                IsEnable = _characterData.CharacterBehaviour.enabled
            });
            foreach(var item in _enemiesData.GetAiList)
            {
                _myData.serializableGameObjects.Add(new SerializableGameObject
                {
                    Name = item.GetInstanceID().ToString(),
                    Position = item.transform.position,
                    Rotation = item.transform.rotation,
                    IsEnable = item.enabled
                });
            }
            _saveSystem.Save(_myData);
        }

        public void Load()
        {
            _myData = _saveSystem.Load();
            foreach(var item in _myData.serializableGameObjects)
            {
                if(item.Name == _characterData.CharacterBehaviour.name)
                {
                    _characterData.CharacterBehaviour.transform.position = item.Position;
                    _characterData.CharacterBehaviour.transform.rotation = item.Rotation;
                    _characterData.CharacterBehaviour.enabled = item.IsEnable;
                }
                foreach(var enemy in _enemiesData.GetAiList)
                {
                    if (item.Name == enemy.GetInstanceID().ToString())
                    {
                        enemy.transform.position = item.Position;
                        enemy.transform.rotation = item.Rotation;
                        enemy.enabled = item.IsEnable;
                    }
                }
                
            }
        }

        #endregion
    }
}
