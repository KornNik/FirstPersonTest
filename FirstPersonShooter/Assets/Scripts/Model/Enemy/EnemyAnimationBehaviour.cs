using UnityEngine;

namespace ExampleTemplate
{
    [RequireComponent(typeof(Animator),(typeof(EnemyBehaviour)))]
    public sealed class EnemyAnimationBehaviour : UnitAnimationBehaviour
    {
        #region Fields

        private EnemiesData _enemiesData;
        private EnemyAi _enemyAi;

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            _enemyAi.MovingSpeed += UnitMovingSpeed;
            _enemyAi.Strafe += UnitStrafeSpeed;
            _enemyAi.Jump += UnitJump;
            _enemyAi.Impact += UnitImpact;
            _enemyAi.Death += UnitDeath;
            _enemyAi.Revive += OnRevive;
        }
        private void OnDisable()
        {
            _enemyAi.MovingSpeed -= UnitMovingSpeed;
            _enemyAi.Strafe -= UnitStrafeSpeed;
            _enemyAi.Jump -= UnitJump;
            _enemyAi.Impact -= UnitImpact;
            _enemyAi.Death -= UnitDeath;
            _enemyAi.Revive -= OnRevive;
        }
        private void OnAnimatorIK()
        {
            SetIK(_handWeight);
        }

        protected override void Awake()
        {
            base.Awake();
            _enemiesData = Data.Instance.EnemiesData;
            _enemyAi = GetComponent<EnemyAi>();
            _handWeight = 1;
        }

        #endregion


        #region Mehtods

        protected override void UnitDeath()
        {
            base.UnitDeath();
            _handWeight = 0;
        }

        private void OnRevive()
        {
            _unitAnimator.ResetTrigger(_jump);
            _unitAnimator.ResetTrigger(_impact);
            _unitAnimator.SetBool(_death, false);
            _handWeight = 1;
        }

        private void SetIK(float handWeight)
        {
            _unitAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, handWeight);
            _unitAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, handWeight);
            _unitAnimator.SetIKPosition(AvatarIKGoal.RightHand, _enemyAi.RightHandTarget.position);
            _unitAnimator.SetIKRotation(AvatarIKGoal.RightHand, _enemyAi.RightHandTarget.rotation);
        }

        #endregion
    }
}