using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace ExampleTemplate
{
    public sealed class EnemyBehaviour : MonoBehaviour, IDamageable
    {
        #region Fields

        [HideInInspector] public NavMeshAgent Agent;

        public static event Action<float> EnemyHealthChanged;

        private WaitForSeconds _waitForDamage = new WaitForSeconds(1);
        private WaitForSeconds _waitForState = new WaitForSeconds(5);
        private StateBotType _stateBot;
        private Renderer[] _materials;
        private Vector3 _point;
        private Coroutine _waitStateRoutine;

        private float _health;

        private bool _isColliderActive;
        private bool _isVisible;
        private bool _isDead;

        private CharacterData _characterData;
        private LevelsData _levelsData;
        private EnemiesData _enemyData;
        private CharacterBehaviour _target;




        private Collider[] _bufferColliders = new Collider[64];
        private int _targetColliders;
        private int _targetLayer = 1 << 8;



        #endregion


        #region Properties

        public StateBotType StateBot { get => _stateBot; set { _stateBot = value; } }

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
            _materials = gameObject.GetComponentsInChildren<Renderer>();
        }

        #endregion


        #region Methods

        public void Tick()
        {
            if (_enemyData.GetIsAggressive()&&_stateBot!=StateBotType.Died)
            {
                if (FindTarget())
                {
                    _stateBot = StateBotType.Detected;
                }
                if (!FindTarget() && _stateBot == StateBotType.Detected)
                {
                    Agent.ResetPath();
                    _stateBot = StateBotType.Patrol;
                }
            }
            switch (_stateBot)
            {
                case StateBotType.None:
                    Default();
                    break;
                case StateBotType.Patrol:
                    Patrolling();
                    break;
                case StateBotType.Detected:
                    Detecting();
                    break;
                case StateBotType.Died:
                    Die();
                    break;
            }
        }

        public void MovePoint(Vector3 point)
        {
            Agent.SetDestination(point);
        }

        private void Default()
        {
            ColorExtensions.ChangeColor(Color.white, _materials);
            if (_waitStateRoutine == null)
            {
                _waitStateRoutine = StartCoroutine(WaitState(StateBotType.Patrol));
            }
        }

        private void Patrolling()
        {
            if (Agent.hasPath) return;
            ColorExtensions.ChangeColor(Color.blue, _materials);
            _point = Patrol.GenericPoint(transform.position);
            Agent.speed = 3;
            MovePoint(_point);
            Agent.stoppingDistance = 0;

        }

        private bool FindTarget()
        {
            var isFindTarget = Physics.CheckSphere(transform.position, _enemyData.GetDistanceView(), _targetLayer);
            return isFindTarget;
        }
        private void ChaseTarget(Vector3 target)
        {
            ColorExtensions.ChangeColor(Color.red, _materials);
            Agent.speed = 6;
            MovePoint(target);
            Agent.stoppingDistance = 5;
        }
        private void Detecting()
        {
            _targetColliders = Physics.OverlapSphereNonAlloc(transform.position, _enemyData.GetDistanceView(), _bufferColliders);
            for (int i = 0; i < _targetColliders; i++)
            {
                CharacterBehaviour character = _bufferColliders[i].GetComponent<CharacterBehaviour>();
                if (character != null )
                {
                    ChaseTarget(character.transform.position);
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
            StopAllCoroutines();
            Invoke(nameof(Revive), _enemyData.GetReviveTime());
        }

        private void Revive()
        {
            _stateBot = StateBotType.None;
            transform.position = Patrol.GenericPoint(_levelsData.GetEnemyPosition(LevelsType.TestLevel).Position);
            transform.rotation = _levelsData.GetEnemyPosition(LevelsType.TestLevel).Rotation();
            _health = _enemyData.GetHealth();
            EnemyHealthChanged?.Invoke(_health/_enemyData.GetHealth());
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
                EnemyHealthChanged?.Invoke(_health / _enemyData.GetHealth());
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
            _waitStateRoutine = null;
        }

        #endregion


        #region IDamageable

        public void ReceiveDamage(float damage)
        {
            if (_stateBot == StateBotType.Died) return;
            _health -= damage;
            EnemyHealthChanged?.Invoke(_health / _enemyData.GetHealth());
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

        #endregion


    }
}
