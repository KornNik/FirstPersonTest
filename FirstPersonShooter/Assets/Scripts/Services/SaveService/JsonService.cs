using System;
using System.IO;
using UnityEngine;


namespace ExampleTemplate
{
    public sealed class JsonService : Service
    {
        #region Fields

        private Crypto _crypto;

        #endregion


        #region ClassLifeCycles

        public JsonService()
        {
            _crypto = new Crypto();
        }

        #endregion


        #region Methods

        public void Save(SerializableGameObject dataSave, string fileName)
        {
            var json = JsonUtility.ToJson(dataSave);
            //File.WriteAllText(filePath, json);
            File.WriteAllText(fileName, _crypto.CryptoXOR(json));

        }

        public SerializableGameObject Load<SerializableGameObject>(string fileName)
        {
            var json = File.ReadAllText(fileName);
            //JsonUtility.FromJson(json, dataSave);
            return JsonUtility.FromJson<SerializableGameObject>(_crypto.CryptoXOR(json));
        }

        #endregion
    }
}
