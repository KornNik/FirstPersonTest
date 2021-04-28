using UnityEngine;
using System;


namespace ExampleTemplate
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class CharacterBehaviour : MonoBehaviour
    {
        #region Fields

        public static Action<float> MovingSpeed;
        public static Action<float> Strafe;

        private CharacterData _characterData;
        private CharacterController _characterController;
        private Vector3 _moveVector;
        private float _gravityForce;
        private float _jumpPower = 10;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _characterController = gameObject.GetComponent<CharacterController>();
            _characterData = Data.Instance.Character;
        }

        #endregion


        #region Methods

        public void CharacterMove(Vector2 inputAxis)
        {
            if (_characterController.isGrounded)
            {
                Vector3 desiredMove = gameObject.transform.forward * inputAxis.y + gameObject.transform.right * inputAxis.x;
                _moveVector.x = desiredMove.x * _characterData.GetSpeed();
                _moveVector.z = desiredMove.z * _characterData.GetSpeed();
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
            if (Input.GetKeyDown(KeyCode.Space) && _characterController.isGrounded) _gravityForce = _jumpPower;
        }

        #endregion
    }
}
