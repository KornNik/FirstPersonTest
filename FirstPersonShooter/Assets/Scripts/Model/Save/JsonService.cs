using System;
using System.IO;
using UnityEngine;


namespace ExampleTemplate
{
    public sealed class JsonService<T> : ISaveSystem<T>
    {
        #region Fields

        private const string _folderName = "dataSave";
        private const string _fileName = "data.txt";
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

        public void Save(T dataSave)
        {
            if (!Directory.Exists(Path.Combine(_path)))
            {
                Directory.CreateDirectory(_path);
            }

            var filePath = Path.Combine(_path, _fileName);
            var json = JsonUtility.ToJson(dataSave);

            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine(_crypto.CryptoXOR(json));
            }

            //File.WriteAllText(filePath, _crypto.CryptoXOR(json));
        }
        public T Load()
        {

            string json = "";
            var filePath = Path.Combine(_path, _fileName);

            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    json += line;
                }
            }

            return JsonUtility.FromJson<T>(_crypto.CryptoXOR(json));
        }

        #endregion
    }
}
