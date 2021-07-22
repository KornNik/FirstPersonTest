using System;
using UnityEngine;
using System.Collections;

namespace ExampleTemplate
{
    public sealed class EnemyBehaviour : MonoBehaviour, IDamageable
    {
        #region Fields

        [HideInInspector] public EnemyAi EnemyAi;

        public static event Action<float> EnemyHealthChanged;

        private WaitForSeconds _waitForDamage = new WaitForSeconds(1);

        private bool _isColliderActive;
        private bool _isVisible;
        private bool _isDead;

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
            EnemyAi.IsDead += OnDie;
        }
        private void OnDisable()
        {
            EnemyAi.IsDead -= OnDie;
        }
        private void Awake()
        {
            _levelsData = Data.Instance.LevelsData;
            EnemyAi = GetComponent<EnemyAi>();

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
            IsVisible = false;
            IsColliderActive = false;
            EnemyAi.Agent.ResetPath();
            StopAllCoroutines();
            Invoke(nameof(Revive), EnemyAi.EnemyStats.EnemyData.GetReviveTime());
        }

        private void Revive()
        {
            EnemyAi.StateBot = StateBotType.None;
            transform.position = Patrol.GenericPoint(_levelsData.GetEnemyPosition(LevelsType.TestLevel).Position);
            transform.rotation = _levelsData.GetEnemyPosition(LevelsType.TestLevel).Rotation();
            EnemyAi.EnemyStats.ResetHealth();
            EnemyHealthChanged?.Invoke(EnemyAi.EnemyStats.Health / EnemyAi.EnemyStats.EnemyData.GetBaseHealth());
            IsVisible = true;
            IsColliderActive = true;
        }

        #endregion


        #region IEnumerator

        private IEnumerator DamageOverTime(float damage, float duration)
        {

            for (int i = 0; i < duration; i++)
            {
                yield return _waitForDamage;
                EnemyAi.EnemyStats.TakeDamage(damage);
                EnemyHealthChanged?.Invoke(EnemyAi.EnemyStats.Health / EnemyAi.EnemyStats.EnemyData.GetBaseHealth());
                _textParticle.SpawnParticle(transform.position, EnemyAi.EnemyStats.Health, Color.red);
            }
            if (EnemyAi.EnemyStats.Health <= 0 && EnemyAi.StateBot != StateBotType.Died)
            {
                EnemyAi.StateBot = StateBotType.Died;
                _isDead = true;
            }
        }

        #endregion


        #region IDamageable

        public void ReceiveDamage(float damage)
        {
            if (EnemyAi.StateBot == StateBotType.Died) return;
            EnemyAi.EnemyStats.TakeDamage(damage);
            EnemyHealthChanged?.Invoke(EnemyAi.EnemyStats.Health / EnemyAi.EnemyStats.EnemyData.GetBaseHealth());
            _textParticle.SpawnParticle(transform.position, EnemyAi.EnemyStats.Health, Color.red);
            if (EnemyAi.EnemyStats.Health <= 0 && EnemyAi.StateBot != StateBotType.Died)
            {
                EnemyAi.StateBot = StateBotType.Died;
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
