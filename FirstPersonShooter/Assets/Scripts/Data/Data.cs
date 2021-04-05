using System;
using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;


namespace ExampleTemplate
{
    [CreateAssetMenu(fileName = "Data", menuName = "Data/Data")]
    public sealed class Data : ScriptableObject
    {
        #region Fields

        [SerializeField] private string _shakeDataPath;
        [SerializeField] private string _characterDataPath;
        [SerializeField] private string _cameraDataPath;
        [SerializeField] private string _levelsDataPath;
        [SerializeField] private string _enemiesDataPath;
        [SerializeField] private string _weaponsDataPath;

        private static CharacterData _characterData;
        private static CameraData _cameraData;
        private static LevelsData _levelsData;
        private static EnemiesData _enemiesData;

        private static readonly Lazy<Data> _instance = new Lazy<Data>(() => Load<Data>("Data/" + typeof(Data).Name));
        
        #endregion
        

        #region Properties

        public static Data Instance => _instance.Value;

        public CharacterData Character
        {
            get
            {
                if (_characterData == null)
                {
                    _characterData = Load<CharacterData>("Data/" + Instance._characterDataPath);
                }

                return _characterData;
            }
        }
        public CameraData Camera
        {
            get
            {
                if (_cameraData == null)
                {
                    _cameraData = Load<CameraData>("Data/" + Instance._cameraDataPath);
                }
                return _cameraData;
            }
        }
        public LevelsData LevelsData
        {
            get
            {
                if (_levelsData == null)
                {
                    _levelsData = Load<LevelsData>("Data/" + Instance._levelsDataPath);
                }
                return _levelsData;
            }
        }
        public EnemiesData EnemiesData
        {
            get
            {
                if (_enemiesData == null)
                {
                    _enemiesData = Load<EnemiesData>("Data/" + Instance._enemiesDataPath);
                }
                return _enemiesData;
            }
        }

        #endregion


        #region Methods

        private static T Load<T>(string resourcesPath) where T : Object =>
            CustomResources.Load<T>(Path.ChangeExtension(resourcesPath, null));
    
        #endregion
    }
}
