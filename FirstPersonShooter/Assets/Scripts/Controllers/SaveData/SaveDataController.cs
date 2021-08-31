using System;
using System.IO;
using UnityEngine;

namespace ExampleTemplate
{
    public class SaveDataController : IExecute
    {
        #region Fields

        private const string _folderName = "dataSave";
        private const string _fileName = "data.bat";
        private string _path;

        private LevelsData _levelsData;
        private JsonService _jsonService;

        #endregion

        #region ClassLifeCycle

        public SaveDataController()
        {
            _levelsData = Data.Instance.LevelsData;
            _jsonService = Services.Instance.JsonService;
            _path = Path.Combine(Application.dataPath, _folderName);
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (Input.GetKeyUp(KeyManager.SAVE))
            {
                Save();
            }
            if (Input.GetKeyUp(KeyManager.LOAD))
            {
                Load();
            }
        }

        #endregion

        #region Methods

        private void Save()
        {
            if (!Directory.Exists(Path.Combine(_path)))
            {
                Directory.CreateDirectory(_path);
            }
            var player = new SerializableGameObject
            {
                Name =     Data.Instance.Character.CharacterBehaviour.name,
                Position = Data.Instance.Character.CharacterBehaviour.transform.position,
                Rotation = Data.Instance.Character.CharacterBehaviour.transform.rotation,
                IsEnable = Data.Instance.Character.CharacterBehaviour.enabled
            };
            _jsonService.Save(player, Path.Combine(_path, _fileName));
        }

        private void Load()
        {
            var filePath = Path.Combine(_path, _fileName);
            if (!File.Exists(filePath)) return;
            var newPlayer = _jsonService.Load<SerializableGameObject>(filePath);
            Data.Instance.Character.CharacterBehaviour.transform.position = newPlayer.Position;
            Data.Instance.Character.CharacterBehaviour.transform.rotation = newPlayer.Rotation;
            Data.Instance.Character.CharacterBehaviour.enabled = newPlayer.IsEnable;
        }

        #endregion
    }
}