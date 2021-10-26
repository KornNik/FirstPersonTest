using System;
using UnityEngine;


namespace ExampleTemplate
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class CharacterBehaviour : UnitsBehaviour
    {
        #region Fields

        [SerializeField] private Transform _cameraPlace;

        public static event Action<float> CharacterHealthChanged;

        private Inventory _inventory;

        private CharacterStats _characterStats;
        private CharacterMovement _characterMovement;

        #endregion


        #region Properties

        public CharacterMovement CharacterMovement => _characterMovement;
        public Transform CameraPlace => _cameraPlace;
        public Inventory Inventory => _inventory;

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();

            _inventory = new Inventory(this);
            _characterStats = new CharacterStats();
            _characterMovement = new CharacterMovement(GetComponent<CharacterController>(),_characterStats,this);

            _unitsData = _characterStats.CharacterData;
            _unitsStats = _characterStats;
            
        }

        #endregion


        #region Methods

        protected override void Die(UnitsData unitsData)
        {
            base.Die(_characterStats.CharacterData);
        }

        protected override void Respawn()
        {
            base.Respawn();
            CharacterHealthChanged?.Invoke(_unitsStats.Health / _unitsData.GetBaseHealth());
        }
        protected override void SetRespawnPoint()
        {
            transform.position = Data.Instance.LevelsData.GetCharacterPosition(LevelsType.TestLevel).Position;
            transform.rotation = Data.Instance.LevelsData.GetCharacterPosition(LevelsType.TestLevel).Rotation();
        }

        #endregion


        #region IDamageable

        public override void ReceiveDamage(float damage)
        {
            base.ReceiveDamage(damage);

            CharacterHealthChanged?.Invoke(_unitsStats.Health / _unitsData.GetBaseHealth());

            if (_unitsStats.Health <= 0 && _isAlive)
            {
                _isDead = true;
                _isAlive = false;
                Die(_unitsData);
            }
        }

        #endregion
    }
}
