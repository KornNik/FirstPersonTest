using System;
using UnityEngine;

namespace ExampleTemplate
{
    public class CharacterMovement
    {
        #region Fields

        private CharacterController _characterController;
        private CharacterStats _characterStats;
        private CharacterBehaviour _characterBehaviour;

        private Vector3 _moveVector;
        private float _gravityForce;
        private float _speedModifier = 1f;
        private bool _isRun;

        #endregion


        #region ClassLifeCycle

        public CharacterMovement(CharacterController controller,CharacterStats stats, CharacterBehaviour behaviour)
        {
            _characterController = controller;
            _characterBehaviour = behaviour;
            _characterStats = stats;
        }

        #endregion


        #region Methods

        public void Move(Vector3 inputAxis)
        {
            if (_characterController.isGrounded)
            {
                if (!_isRun) { _speedModifier = 1f; } else { _speedModifier = 1.5f; }
                Vector3 desiredMove = _characterBehaviour.transform.forward * inputAxis.y + _characterBehaviour.transform.right * inputAxis.x;
                _moveVector.x = desiredMove.x * _characterStats.Speed * _speedModifier;
                _moveVector.z = desiredMove.z * _characterStats.Speed * _speedModifier;
            }

            _characterBehaviour.MovingSpeed?.Invoke(inputAxis.y);
            _characterBehaviour.Strafe?.Invoke(inputAxis.x);

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
                _characterBehaviour.Jump?.Invoke();
            }
        }

        public void Run()
        {
            _isRun = !_isRun;
        }

        #endregion
    }
}
