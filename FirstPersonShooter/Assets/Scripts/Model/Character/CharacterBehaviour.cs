using System;
using UnityEngine;
using System.Collections;


namespace ExampleTemplate
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class CharacterBehaviour : MonoBehaviour, IDamageable
    {
        #region Fields

        public static event Action<float> MovingSpeed;
        public static event Action<float> Strafe;
        public static event Action<float> CharacterHealthChanged;
        public Inventory Inventory;

        private CharacterController _characterController;
        private CharacterStats _characterStats;

        private Vector3 _moveVector;
        private float _gravityForce;
        private bool _isDead;
        private WaitForSeconds _waitForDamage = new WaitForSeconds(1);

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _characterController = gameObject.GetComponent<CharacterController>();
            Inventory = new Inventory(this);
            _characterStats = new CharacterStats();
        }

        #endregion


        #region Methods

        public void CharacterMove(Vector2 inputAxis)
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
            }
        }

        private void Respawn()
        {
            StopAllCoroutines();
            _characterStats.ResetHealth();
            transform.position = Data.Instance.LevelsData.GetCharacterPosition(LevelsType.TestLevel).Position;
            transform.rotation = Data.Instance.LevelsData.GetCharacterPosition(LevelsType.TestLevel).Rotation();
            _isDead = false;
        }

        #endregion


        #region IEnumerator

        private IEnumerator DamageOverTime(float damage, float duration)
        {
            for (int i = 0; i < duration; i++)
            {
                yield return _waitForDamage;
                _characterStats.TakeDamage(damage);
                CharacterHealthChanged?.Invoke(_characterStats.Health/_characterStats.CharacterData.GetBaseHealth());
            }
            if (_characterStats.Health <= 0)
            {
                Respawn();
                _isDead = true;
            }
        }

        #endregion


        #region IDamageable

        public void ReceiveDamage(float damage)
        {
            _characterStats.TakeDamage(damage);
            CharacterHealthChanged?.Invoke(_characterStats.Health / _characterStats.CharacterData.GetBaseHealth());
            if (_characterStats.Health <= 0)
            {
                Respawn();
                _isDead = true;
            }
        }

        public void ReceiveDamageOverTime(float damage, float duration)
        {
            StartCoroutine(DamageOverTime(damage, duration));
        }

        #endregion
    }
}
