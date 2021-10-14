using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace ExampleTemplate
{
    [RequireComponent(typeof(EnemyBehaviour))]
    public class EnemyAi : MonoBehaviour
    {
        #region Fields

        [HideInInspector] public NavMeshAgent Agent;

        private StateBotType _stateBot;
        private WeaponBehaviour _weapon;
        private EnemyBehaviour _enemyBehaviour;
        private EnemiesData _enemiesData;

        private Coroutine _waitStateRoutine;
        private Renderer[] _materials;
        private Vector3 _point;

        private bool _isDelay;
        private int _targetColliders;
        private float _handWeight;

        private Collider[] _bufferColliders = new Collider[64];
        private WaitForSeconds _waitForState = new WaitForSeconds(5);


        #endregion


        #region Properties

        public StateBotType StateBot { get => _stateBot; set { _stateBot = value; } }

        #endregion


        #region UnityMethods

        public void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            _materials = GetComponentsInChildren<Renderer>();
            _enemyBehaviour = GetComponent<EnemyBehaviour>();
            _weapon = GetComponentInChildren<WeaponBehaviour>();
            _enemiesData = Data.Instance.EnemiesData;

            Agent.autoRepath = true;
        }

        #endregion


        #region Methods

        public void Tick()
        {
            _enemyBehaviour.MovingSpeed?.Invoke(Agent.velocity.normalized.magnitude);

            if (_weapon.Clip.CountAmmunition == 0)
            {
                _weapon.ReloadClip();
            }
            if (_enemyBehaviour.EnemyStats.IsAggressive && _stateBot != StateBotType.Died)
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
                    _enemyBehaviour.Die();
                    break;
            }
        }
        public void Move(Vector3 point)
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
            Agent.speed = _enemyBehaviour.EnemyStats.Speed;
            Move(_point);
            Agent.stoppingDistance = 0;

        }
        private bool FindTarget()
        {
            var isFindTarget = Physics.CheckSphere(transform.position, _enemyBehaviour.EnemyStats.DistanceView, LayerManager.PlayerLayer);
            return isFindTarget;
        }
        private void ChaseTarget(Vector3 target)
        {
            ColorExtensions.ChangeColor(Color.red, _materials);
            Agent.speed = _enemyBehaviour.EnemyStats.Speed * 2;
            Move(target);
            Agent.stoppingDistance = 10;
            if (AtGunpoint() && !_isDelay)
            {
                _weapon.Fire();
                _isDelay = true;
                Invoke(nameof(ReadyShoot), _enemiesData.GetShootingDelay());
            }
        }
        private bool AtGunpoint()
        {
            RaycastHit hit;
            if (Physics.Raycast(_weapon.Barrel.position, _weapon.Barrel.forward, out hit,
                LayerManager.PlayerLayer))
            {
                return true;
            }
            return false;
        }
        private void ReadyShoot()
        {
            _isDelay = false;
        }
        private void Detecting()
        {
            _targetColliders = Physics.OverlapSphereNonAlloc(transform.position, _enemyBehaviour.EnemyStats.DistanceView, _bufferColliders);
            for (int i = 0; i < _targetColliders; i++)
            {
                CharacterBehaviour character = _bufferColliders[i].GetComponent<CharacterBehaviour>();
                if (character != null)
                {
                    ChaseTarget(character.transform.position);
                    gameObject.transform.LookAt(character.transform);
                    break;
                }
            }
        }
        private void ReadyState(StateBotType stateBot)
        {
            if (_stateBot == StateBotType.Died) return;
            _stateBot = stateBot;
        }

        #endregion


        #region IEnumerator

        private IEnumerator WaitState(StateBotType stateBot)
        {
            yield return _waitForState;
            ReadyState(stateBot);
            _waitStateRoutine = null;
        }

        #endregion
    }
}