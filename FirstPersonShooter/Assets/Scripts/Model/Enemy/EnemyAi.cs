using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace ExampleTemplate
{
    public class EnemyAi : MonoBehaviour
    {
        #region Fields

        [HideInInspector] public NavMeshAgent Agent;
        [HideInInspector] public EnemyStats EnemyStats;

        public Transform RightHandTarget;

        public Action<float> MovingSpeed;
        public Action<float> Strafe;
        public Action Impact;
        public Action Jump;
        public Action Death;
        public Action Revive;

        private StateBotType _stateBot;
        private WeaponBehaviour _weapon;
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
            _materials = GetComponentsInChildren<Renderer>();
            _weapon = GetComponentInChildren<WeaponBehaviour>();
            Agent = GetComponent<NavMeshAgent>();
            EnemyStats = new EnemyStats();

            Agent.autoRepath = true;
        }

        #endregion


        #region Methods

        public void Tick()
        {
            MovingSpeed?.Invoke(Agent.velocity.normalized.magnitude);

            if (_weapon.Clip.CountAmmunition == 0)
            {
                _weapon.ReloadClip();
            }
            if (EnemyStats.EnemyData.GetIsAggressive() && _stateBot != StateBotType.Died)
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
            Agent.speed = EnemyStats.Speed;
            MovePoint(_point);
            Agent.stoppingDistance = 0;

        }
        private bool FindTarget()
        {
            var isFindTarget = Physics.CheckSphere(transform.position, EnemyStats.DistanceView, LayerManager.PlayerLayer);
            return isFindTarget;
        }
        private void ChaseTarget(Vector3 target)
        {
            ColorExtensions.ChangeColor(Color.red, _materials);
            Agent.speed = EnemyStats.Speed * 2;
            MovePoint(target);
            Agent.stoppingDistance = 10;
            if (AtGunpoint() && !_isDelay)
            {
                _weapon.Fire();
                _isDelay = true;
                Invoke(nameof(ReadyShoot), EnemyStats.EnemyData.GetShootingDelay());
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
            _targetColliders = Physics.OverlapSphereNonAlloc(transform.position, EnemyStats.DistanceView, _bufferColliders);
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

        private void Die()
        {
            Death?.Invoke();
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