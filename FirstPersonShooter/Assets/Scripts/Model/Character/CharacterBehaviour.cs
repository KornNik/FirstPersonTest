using System;
using UnityEngine;


namespace ExampleTemplate
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class CharacterBehaviour : UnitsBehaviour
    {
        #region Fields

        public static event Action<float> CharacterHealthChanged;

        public Inventory Inventory;

        private CharacterController _characterController;
        private CharacterStats _characterStats;

        private Vector3 _moveVector;
        private float _gravityForce;

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();

            _characterController = GetComponent<CharacterController>();
            Inventory = new Inventory(this);
            _characterStats = new CharacterStats();

            _unitsData = _characterStats.CharacterData;
            _unitsStats = _characterStats;
            
        }

        #endregion


        #region Methods

        public override void Move(Vector3 inputAxis)
        {
            if (_characterController.isGrounded)
            {
                Vector3 desiredMove = gameObject.transform.forward * inputAxis.y + gameObject.transform.right * inputAxis.x;
                _moveVector.x = desiredMove.x * _characterStats.Speed;
                _moveVector.z = desiredMove.z * _characterStats.Speed;
            }

            MovingSpeed?.Invoke(inputAxis.y);
            Strafe?.Invoke(inputAxis.x);

            _moveVector.y = _gravityForce;
            _characterController.Move(_moveVector * Time.deltaTime);
        }

        public void GamingGravity()
        {
            if (!_characterController.isGrounded) _gravityForce -= 30 * Time.deltaTime;
            else _gravityForce = -1;
        }

        public void CharacterJump()
        {
            if (_characterController.isGrounded)
            {
                _gravityForce = _characterStats.JumpPower;
                Jump?.Invoke();
            }
        }
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
