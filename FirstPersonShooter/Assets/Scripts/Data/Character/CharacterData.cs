using UnityEngine;


namespace ExampleTemplate
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Data/Character/CharacterData")]
    public sealed class CharacterData : UnitsData
    {
        #region Fields

        [SerializeField] private float _baseWeaponAimingSpeed = 3;
        [SerializeField] private float _baseJumpPower = 10;

        [HideInInspector] public Camera Camera;
        [HideInInspector] public CharacterBehaviour CharacterBehaviour;
        [HideInInspector] public CharacterAnimationBehaviour CharacterAnimationBehaviour;
        [HideInInspector] public Transform RightHandTarget;
        [HideInInspector] public Transform CameraPlace;


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
            CameraPlace = GameObject.FindGameObjectWithTag
                (TagManager.GetTag(TagType.CameraPlace)).transform;

            CharacterAnimationBehaviour = CharacterBehaviour.GetComponent<CharacterAnimationBehaviour>();

            Camera.transform.SetParent(CameraPlace);
            Camera.transform.localPosition = Vector3.zero;
            Camera.transform.localRotation = Quaternion.identity;
        }

        public float GetBaseWeaponAimingSpeed()
        {
            return _baseWeaponAimingSpeed;
        }
        public float GetBaseJumpPower()
        {
            return _baseJumpPower;
        }

        #endregion
    }
}
