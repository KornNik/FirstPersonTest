using System;
using System.IO;
using UnityEngine;


namespace ExampleTemplate
{
    public sealed class JsonService : Service
    {
        #region Fields

        private const string _folderName = "dataSave";
        private const string _fileName = "data.bat";
        private string _path;
        private Crypto _crypto;

        #endregion


        #region ClassLifeCycles

        public JsonService()
        {
            _crypto = new Crypto();
            _path = Path.Combine(Application.dataPath, _folderName);
        }

        #endregion


        #region Methods

        public void Save<T>(T dataSave, string fileName) where T : SerializableGameObject
        {
            if (!Directory.Exists(Path.Combine(_path)))
            {
                Directory.CreateDirectory(_path);
            }
            var filePath = Path.Combine(_path, fileName);
            var json = JsonUtility.ToJson(dataSave);
            File.WriteAllText(filePath, _crypto.CryptoXOR(json));
        }
        public void Load<T>(string fileName, T dataSave) where T : SerializableGameObject
        {
            var filePath = Path.Combine(_path, fileName);
            if (!File.Exists(filePath)) return;
            var json = File.ReadAllText(filePath);
            JsonUtility.FromJson<T>(_crypto.CryptoXOR(json));
        }

        #endregion
    }
}
