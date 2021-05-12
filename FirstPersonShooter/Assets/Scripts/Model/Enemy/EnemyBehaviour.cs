using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace ExampleTemplate
{
    public sealed class EnemyBehaviour : MonoBehaviour, IDamageable
    {
        #region Fields

        [HideInInspector] public Renderer[] Materials;
        [HideInInspector] public NavMeshAgent Agent;

        public static Action<float> EnemyHealthChanged;

        private WaitForSeconds _waitForDamage = new WaitForSeconds(1);
        private WaitForSeconds _waitForState = new WaitForSeconds(5);
        private StateBotType _stateBot;
        private Vector3 _point;

        private float _waitForRevive = 10.0f;
        private float _health;

        private bool _isAggressive = true;
        private bool _isColliderActive;
        private bool _isVisible;
        private bool _isDead;

        private CharacterData _characterData;
        private LevelsData _levelsData;
        private EnemiesData _enemyData;
        private CharacterBehaviour _target;




        private Collider[] _bufferColliders = new Collider[64];
        private int _targetColliders;



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

        private void Awake()
        {
            _enemyData = Data.Instance.EnemiesData;
            _characterData = Data.Instance.Character;
            _levelsData = Data.Instance.LevelsData;

            _health = _enemyData.GetHealth();

            Agent = gameObject.GetComponent<NavMeshAgent>();
            Materials = gameObject.GetComponentsInChildren<Renderer>();
        }

        #endregion


        #region Methods

        public void Tick()
        {
            switch (_stateBot)
            {
                case StateBotType.None:
                    Default();
                    break;
                case StateBotType.Patrol:
                    Patrolling();
                    break;
                case StateBotType.Detected:
                    Detecting(_target);
                    break;
                case StateBotType.Died:
                    _enemyData.EnemyBehaviour.Die();
                    break;
            }
        }

        public void MovePoint(Vector3 point)
        {
            Agent.SetDestination(point);
        }

        public void ReceiveDamage(float damage)
        {
            _health -= damage;
            EnemyHealthChanged?.Invoke(_health);
            if (_health <= 0 && _stateBot != StateBotType.Died)
            {
                _stateBot = StateBotType.Died;
                _isDead = true;
            }
        }

        public void ReceiveDamageOverTime(float damage, float duration)
        {
            StartCoroutine(DamageOverTime(damage, duration));
        }

        private void Default()
        {
            ColorExtensions.ChangeColor(Color.white, _enemyData.EnemyBehaviour.Materials);
            StartCoroutine(WaitState(StateBotType.Patrol));
        }

        private void Patrolling()
        {
            if (_isAggressive)
            {
                FindEnemy();
            }
            if (!Agent.hasPath)
            {
                ColorExtensions.ChangeColor(Color.blue, _enemyData.EnemyBehaviour.Materials);
                _point = Patrol.GenericPoint(transform.position);
                Agent.speed = 3;
                MovePoint(_point);
                Agent.stoppingDistance = 0;
            }
        }

        private void Detecting(CharacterBehaviour target)
        {
            ColorExtensions.ChangeColor(Color.red, _enemyData.EnemyBehaviour.Materials);
            Agent.speed = 6;
            MovePoint(target.transform.position);
            Debug.Log(target);
            Agent.stoppingDistance = 10;
        }

        private void FindEnemy()
        {
            _targetColliders = Physics.OverlapSphereNonAlloc(transform.position, _enemyData.GetDistanceView(), _bufferColliders);
            for (int i = 0; i < _targetColliders; i++)
            {
                CharacterBehaviour character = _bufferColliders[i].GetComponent<CharacterBehaviour>();
                if (character != null && _stateBot != StateBotType.Detected)
                {
                    _target = character;
                    _stateBot = StateBotType.Detected;
                    break;
                }
            }
        }

        private void ReadyState(StateBotType stateBot)
        {
            if (_stateBot == StateBotType.Died) return;
            _stateBot = stateBot;
        }

        private void Die()
        {
            if (!_isDead) { return; }
            _isDead = false;
            IsVisible = false;
            IsColliderActive = false;
            Agent.ResetPath();
            Invoke(nameof(Revive), _waitForRevive);
        }

        private void Revive()
        {
            _stateBot = StateBotType.None;
            transform.position = Patrol.GenericPoint(_levelsData.GetEnemyPosition(LevelsType.TestLevel).Position);
            transform.rotation = _levelsData.GetEnemyPosition(LevelsType.TestLevel).Rotation();
            _health = _enemyData.GetHealth();
            EnemyHealthChanged?.Invoke(_health);
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
                _health -= damage;
                EnemyHealthChanged?.Invoke(_health);
            }
            if (_health <= 0 && _stateBot != StateBotType.Died)
            {
                _stateBot = StateBotType.Died;
                _isDead = true;
            }
        }

        private IEnumerator WaitState(StateBotType stateBot)
        {
            yield return _waitForState;
            ReadyState(stateBot);
        }

        #endregion

    }
}
