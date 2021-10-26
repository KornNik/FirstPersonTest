using UnityEngine;

namespace ExampleTemplate
{
    [RequireComponent(typeof(Animator),(typeof(EnemyBehaviour)))]
    public sealed class EnemyAnimationBehaviour : UnitAnimationBehaviour
    {
        #region Fields

        private EnemyBehaviour _enemy;

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            _enemy.MovingSpeed += OnUnitMovingSpeed;
            _enemy.Strafe += OnUnitStrafeSpeed;
            _enemy.Jump += OnUnitJump;
            _enemy.Impact += OnUnitImpact;
            _enemy.Death += OnUnitDeath;
            _enemy.Revive += OnRevive;
        }
        private void OnDisable()
        {
            _enemy.MovingSpeed -= OnUnitMovingSpeed;
            _enemy.Strafe -= OnUnitStrafeSpeed;
            _enemy.Jump -= OnUnitJump;
            _enemy.Impact -= OnUnitImpact;
            _enemy.Death -= OnUnitDeath;
            _enemy.Revive -= OnRevive;
        }
        private void OnAnimatorIK()
        {
            SetIK(_handWeight);
        }

        protected override void Awake()
        {
            base.Awake();
            _enemy = GetComponent<EnemyBehaviour>();
            _handWeight = 1;
        }

        #endregion


        #region Mehtods

        protected override void OnUnitDeath()
        {
            base.OnUnitDeath();
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
            _unitAnimator.SetIKPosition(AvatarIKGoal.RightHand, _enemy.RightHandTarget.position);
            _unitAnimator.SetIKRotation(AvatarIKGoal.RightHand, _enemy.RightHandTarget.rotation);
        }

        #endregion
    }
}