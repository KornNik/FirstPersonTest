using System;
using UnityEngine;
using System.Collections;

namespace ExampleTemplate
{

    public sealed class EnemyBehaviour : MonoBehaviour, IDamageable
    {
        #region Fields


        public static event Action<float> EnemyHealthChanged;

        private WaitForSeconds _waitForDamage = new WaitForSeconds(1);

        private bool _isColliderActive;
        private bool _isVisible;
        private bool _isDead;

        private EnemyAi _enemyAi;
        private LevelsData _levelsData;
        private TextRendererParticleSystem _textParticle;

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

        private void OnEnable()
        {
            _enemyAi.Death += OnDie;
        }
        private void OnDisable()
        {
            _enemyAi.Death -= OnDie;
        }
        private void Awake()
        {
            _levelsData = Data.Instance.LevelsData;
            _enemyAi = GetComponent<EnemyAi>();

            var textParticle = CustomResources.Load<TextRendererParticleSystem>
                (AssetsPathParticles.ParticlesGameObject[VFXType.TextParticle]);
            _textParticle = Instantiate(textParticle, transform.position, transform.rotation, transform);
        }

        #endregion


        #region Methods

        private void OnDie()
        {
            if (!_isDead) { return; }
            _isDead = false;
            _enemyAi.Agent.ResetPath();
            StopAllCoroutines();
            Invoke(nameof(Revive), _enemyAi.EnemyStats.EnemyData.GetReviveTime());
        }
        private void Revive()
        {
            _enemyAi.StateBot = StateBotType.None;
            transform.position = Patrol.GenericPoint(_levelsData.GetEnemyPosition(LevelsType.TestLevel).Position);
            transform.rotation = _levelsData.GetEnemyPosition(LevelsType.TestLevel).Rotation();
            _enemyAi.EnemyStats.ResetHealth();
            EnemyHealthChanged?.Invoke(_enemyAi.EnemyStats.Health / _enemyAi.EnemyStats.EnemyData.GetBaseHealth());
            _enemyAi.Revive?.Invoke();
        }

        private void SwitchVisibility()
        {
            IsVisible = !IsVisible;
            IsColliderActive = !IsColliderActive;
        }

        #endregion


        #region IEnumerator

        private IEnumerator DamageOverTime(float damage, float duration)
        {

            for (int i = 0; i < duration; i++)
            {
                yield return _waitForDamage;
                _enemyAi.EnemyStats.TakeDamage(damage);
                EnemyHealthChanged?.Invoke(_enemyAi.EnemyStats.Health / _enemyAi.EnemyStats.EnemyData.GetBaseHealth());
                _textParticle.SpawnParticle(transform.position, _enemyAi.EnemyStats.Health, Color.red);
            }
            if (_enemyAi.EnemyStats.Health <= 0 && _enemyAi.StateBot != StateBotType.Died)
            {
                _enemyAi.StateBot = StateBotType.Died;
                _isDead = true;
            }
        }

        #endregion


        #region IDamageable

        public void ReceiveDamage(float damage)
        {
            if (_enemyAi.StateBot == StateBotType.Died) return;
            _enemyAi.EnemyStats.TakeDamage(damage);
            _enemyAi.Impact?.Invoke();
            EnemyHealthChanged?.Invoke(_enemyAi.EnemyStats.Health / _enemyAi.EnemyStats.EnemyData.GetBaseHealth());
            _textParticle.SpawnParticle(transform.position, _enemyAi.EnemyStats.Health, Color.red);
            if (_enemyAi.EnemyStats.Health <= 0 && _enemyAi.StateBot != StateBotType.Died)
            {
                _enemyAi.StateBot = StateBotType.Died;
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
