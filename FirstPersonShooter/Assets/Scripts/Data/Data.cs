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

        [SerializeField] private string _characterDataPath;
        [SerializeField] private string _cameraDataPath;
        [SerializeField] private string _levelsDataPath;
        [SerializeField] private string _enemiesDataPath;
        [SerializeField] private string _pistolDataPath;
        [SerializeField] private string _granadeLauncherDataPath;
        [SerializeField] private string _bulletDataPath;
        [SerializeField] private string _explosionAmmunitionDataPath;
        [SerializeField] private string _flashLightDataPath;
        [SerializeField] private string _granadeDataPath;

        private static CharacterData _characterData;
        private static CameraData _cameraData;
        private static LevelsData _levelsData;
        private static EnemiesData _enemiesData;
        private static WeaponData _pistolData;
        private static WeaponData _granadeLauncherData;
        private static AmmunitionData _bulletData;
        private static ExplosionAmmunitionData _explosionAmmunitionData;
        private static FlashLightData _flashLightData;
        private static GranadeData _granadeData;


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
        public WeaponData PistolData
        {
            get
            {
                if (_pistolData == null)
                {
                    _pistolData = Load<WeaponData>("Data/" + Instance._pistolDataPath);
                }
                return _pistolData;
            }
        }
        public WeaponData GranadeLauncherData
        {
            get
            {
                if (_granadeLauncherData == null)
                {
                    _granadeLauncherData = Load<WeaponData>("Data/" + Instance._granadeLauncherDataPath);
                }
                return _granadeLauncherData;
            }
        }
        public AmmunitionData BulletData
        {
            get
            {
                if (_bulletData == null)
                {
                    _bulletData = Load<AmmunitionData>("Data/" + Instance._bulletDataPath);
                }
                return _bulletData;
            }
        }
        public ExplosionAmmunitionData ExplosionAmmunitionData
        {
            get
            {
                if (_explosionAmmunitionData == null)
                {
                    _explosionAmmunitionData = Load<ExplosionAmmunitionData>("Data/" + Instance._explosionAmmunitionDataPath);
                }
                return _explosionAmmunitionData;
            }
        }
        public FlashLightData FlashLightData
        {
            get
            {
                if (_flashLightData == null)
                {
                    _flashLightData = Load<FlashLightData>("Data/" + Instance._flashLightDataPath);
                }
                return _flashLightData;
            }
        }
        public GranadeData GranadeData
        {
            get
            {
                if (_granadeData == null)
                {
                    _granadeData = Load<GranadeData>("Data/" + Instance._granadeDataPath);
                }
                return _granadeData;
            }
        }

        #endregion


        #region Methods

        private static T Load<T>(string resourcesPath) where T : Object =>
            CustomResources.Load<T>(Path.ChangeExtension(resourcesPath, null));
    
        #endregion
    }
}
