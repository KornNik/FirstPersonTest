using UnityEngine;


namespace ExampleTemplate
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Data/Character/CharacterData")]
    public sealed class CharacterData : ScriptableObject
    {
        #region Fields

        [SerializeField] private float _speed;
        [SerializeField] private float _health;
        [SerializeField] private float _weaponAimingSpeed;

        [HideInInspector] public CharacterBehaviour CharacterBehaviour;
        [HideInInspector] public CharacterAnimationBehaviour CharacterAnimationBehaviour;
        [HideInInspector] public CameraBehaviuor CameraBehaviuor;
        [HideInInspector] public InventoryBehaviour InventoryBehaviour;
        [HideInInspector] public Transform RightHandTarget;

        #endregion


        #region Methods

        public void Initialization(CharactersType characterType, CharacterPosition point)
        {
            var characterBehaviour = CustomResources.Load<CharacterBehaviour>
                (AssetsPathCharacters.CharactersGameObject[characterType]);
            CharacterBehaviour = Instantiate(characterBehaviour, point.Position, point.Rotation());
            RightHandTarget = GameObject.FindGameObjectWithTag
                (TagManager.GetTag(TagType.RightHandTarget)).transform;
            CharacterAnimationBehaviour = CharacterBehaviour.GetComponent<CharacterAnimationBehaviour>();
            InventoryBehaviour = CharacterBehaviour.GetComponent<InventoryBehaviour>();
            CameraBehaviuor = CharacterBehaviour.GetComponentInChildren<CameraBehaviuor>();

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

        #endregion
    }
}
