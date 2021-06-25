using UnityEngine;


namespace ExampleTemplate
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Data/Character/CharacterData")]
    public sealed class CharacterData : ScriptableObject
    {
        #region Fields

        [SerializeField] private float _speed = 10;
        [SerializeField] private float _health = 100;
        [SerializeField] private float _weaponAimingSpeed = 3;
        [SerializeField] private float _jumpPower = 10;

        [HideInInspector] public Camera Camera;
        [HideInInspector] public CharacterBehaviour CharacterBehaviour;
        [HideInInspector] public CharacterAnimationBehaviour CharacterAnimationBehaviour;
        [HideInInspector] public Transform RightHandTarget;
        [HideInInspector] public Transform _cameraPlace;


        #endregion


        #region Methods

        public void Initialization(CharactersType characterType, CharacterPosition point)
        {
            Camera = Services.Instance.CameraServices.CameraMain;
            var characterBehaviour = CustomResources.Load<CharacterBehaviour>
                (AssetsPathCharacters.CharactersGameObject[characterType]);
            CharacterBehaviour = Instantiate(characterBehaviour, point.Position, point.Rotation());

            RightHandTarget = GameObject.FindGameObjectWithTag
                (TagManager.GetTag(TagType.RightHandTarget)).transform;
            _cameraPlace = GameObject.FindGameObjectWithTag
                (TagManager.GetTag(TagType.CameraPlace)).transform;

            CharacterAnimationBehaviour = CharacterBehaviour.GetComponent<CharacterAnimationBehaviour>();

            Camera.transform.SetParent(_cameraPlace);
            Camera.transform.localPosition = Vector3.zero;
            Camera.transform.localRotation = Quaternion.identity;
        }

        public float GetSpeed()
        {
            return _speed;
        }
        public float GetHealth()
        {
            return _health;
        }
        public float GetWeaponAimingSpeed()
        {
            return _weaponAimingSpeed;
        }
        public float GetJumpPower()
        {
            return _jumpPower;
        }

        #endregion
    }
}
