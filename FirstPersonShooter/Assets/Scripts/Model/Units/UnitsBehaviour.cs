using System;
using UnityEngine;
using System.Collections;

namespace ExampleTemplate
{
    public abstract class UnitsBehaviour : MonoBehaviour, IDamageable
    {

        #region Fields

        public Action Death;
        public Action Revive;
        public Action Impact;
        public Action Aiming;
        public Action Jump;
        public Action TossGranade;
        public Action<float> Strafe;
        public Action<float> MovingSpeed;

        protected WaitForSeconds _waitForDamage = new WaitForSeconds(1);
        protected Rigidbody[] _rigidbodies;
        protected UnitsData _unitsData;
        protected UnitsStats _unitsStats;

        protected bool _isColliderActive;
        protected bool _isVisible;
        protected bool _isDead;
        protected bool _isAlive = true;

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
                        tempCollider = item.GetComponentInChildren<Collider>();
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
                    tempRenderer = item.GetComponentInChildren<Renderer>();
                    if (tempRenderer)
                        tempRenderer.enabled = _isVisible;
                }
            }
        }

        #endregion


        #region UnityMethods

        protected virtual void Awake()
        {
            _rigidbodies = GetComponentsInChildren<Rigidbody>();

            SwitchKinematic();
        }

        #endregion


        #region Methods


        protected virtual void Die(UnitsData unitsData)
        {
            if (!_isDead) return;

            _isDead = false;
            StopAllCoroutines();
            Death?.Invoke();

            Invoke(nameof(Respawn), unitsData.GetReviveTime());
            Invoke(nameof(SwitchAnimator), unitsData.GetRagdollTime());
            Invoke(nameof(SwitchKinematic), unitsData.GetRagdollTime());
        }
        protected virtual void Respawn()
        {
            SwitchAnimator();
            SwitchKinematic();
            SetRespawnPoint();
            _unitsStats.ResetHealth();
            _isAlive = true;
        }

        protected void SwitchVisibility()
        {
            IsVisible = !IsVisible;
            IsColliderActive = !IsColliderActive;
        }

        private void SwitchKinematic()
        {
            foreach (var item in _rigidbodies)
            {
                item.isKinematic = !item.isKinematic;
            }
        }
        private void SwitchAnimator()
        {
            var animator = GetComponent<Animator>();
            animator.enabled = !animator.isActiveAndEnabled;
        }

        protected abstract void SetRespawnPoint();

        #endregion


        #region IEnumerator

        private IEnumerator DamageOverTime(float damage, float duration)
        {
            if (!_isAlive) { StopCoroutine(nameof(DamageOverTime)); }

            for (int i = 0; i < duration; i++)
            {
                yield return _waitForDamage;

                ReceiveDamage(damage);
            }
        }

        #endregion


        #region IDamageable

        public virtual void ReceiveDamage(float damage)
        {
            _unitsStats.TakeDamage(damage);
            Impact?.Invoke();
        }

        public virtual void ReceiveDamageOverTime(float damage, float duration)
        {
            StartCoroutine(DamageOverTime(damage, duration));
        }

        #endregion
    }
}
