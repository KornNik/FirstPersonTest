using System;
using UnityEngine;
using System.Collections;

namespace ExampleTemplate
{
    public sealed class EnemyBehaviour : MonoBehaviour , IDamageable
    {
        #region Fields

        public static Action<float> OnHealthChanged;

        private WaitForSeconds _waitOneSecond = new WaitForSeconds(1);
        private EnemiesData _enemyData;
        private Rigidbody _rigidbody;
        private bool _isVisible;
        private bool _isColliderActive;
        private float _health;

        #endregion


        #region Properties

        public bool IsColliderActive
        {
            get => _isColliderActive;
            set
            {
                _isColliderActive = value;
                var tempCollider = GetComponent<Collider>();
                if (tempCollider)
                {
                    tempCollider.enabled = _isColliderActive;
                    if (transform.childCount <= 0) return;
                    foreach (Transform item in transform)
                    {
                        tempCollider = item.gameObject.GetComponentInChildren<Collider>();
                        if (tempCollider)
                        {
                            tempCollider.enabled = _isColliderActive;
                        }
                    }
                }
            }
        }

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                var tempRenderer = GetComponent<Renderer>();
                if (tempRenderer)
                    tempRenderer.enabled = _isVisible;
                if (transform.childCount <= 0) return;
                foreach (Transform item in transform)
                {
                    tempRenderer = item.gameObject.GetComponentInChildren<Renderer>();
                    if (tempRenderer)
                        tempRenderer.enabled = _isVisible;
                }
            }
        }

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _enemyData = Data.Instance.EnemiesData;
            _health = _enemyData.GetHealth();
            _rigidbody = gameObject.GetComponent<Rigidbody>();
        }

        #endregion


        #region Methods

        public void ReceiveDamage(float damage)
        {
            _health -= damage;
            OnHealthChanged?.Invoke(_health);
            if (_health <= 0) { Die(); }
        }

        public void ReceiveDamageOverTime(float damage, float duration)
        {
            StartCoroutine(DamageOverTime(damage, duration));
        }

        private void Die()
        {
            IsVisible = false;
            IsColliderActive = false;
            _rigidbody.isKinematic = true;
            StartCoroutine(WaitForRevive());

        }
        private void Revive()
        {
            transform.position = Data.Instance.LevelsData.GetEnemyPosition(LevelsType.TestLevel).Position;
            _health = _enemyData.GetHealth();
            OnHealthChanged?.Invoke(_health);
            IsVisible = true;
            IsColliderActive = true;
            _rigidbody.isKinematic = false;

        }

        private IEnumerator WaitForRevive()
        {
            yield return _waitOneSecond;
            Revive();

        }
        private IEnumerator DamageOverTime(float damage, float duration)
        {
            for (int i = 0; i < duration; i++)
            {
                yield return _waitOneSecond;
                _health -= damage;
                OnHealthChanged?.Invoke(_health);
                if (_health <= 0) { Die(); }
            }
        }

        #endregion

    }
}
