using UnityEngine;

namespace ExampleTemplate
{
    public class UnitAnimationBehaviour : MonoBehaviour
    {
        #region Fields

        protected static readonly int _movingSpeed = Animator.StringToHash(UnitParametrsManager.MOVING_SPEED);
        protected static readonly int _strafe = Animator.StringToHash(UnitParametrsManager.STRAFE);
        protected static readonly int _jump = Animator.StringToHash(UnitParametrsManager.JUMP);
        protected static readonly int _impact = Animator.StringToHash(UnitParametrsManager.IMPACT);
        protected static readonly int _death = Animator.StringToHash(UnitParametrsManager.DEATH);

        protected Animator _unitAnimator;
        protected float _handWeight;

        #endregion

        #region UnityMethods

        protected virtual void Awake()
        {
            _unitAnimator = GetComponent<Animator>();
        }

        #endregion


        #region Methods

        protected virtual void UnitMovingSpeed(float obj)
        {
            _unitAnimator.SetFloat(_movingSpeed, obj);
        }
        protected virtual void UnitStrafeSpeed(float obj)
        {
            _unitAnimator.SetFloat(_strafe, obj);
        }
        protected virtual void UnitJump()
        {
            _unitAnimator.SetTrigger(_jump);
        }
        protected virtual void UnitImpact()
        {
            _unitAnimator.SetTrigger(_impact);
        }
        protected virtual void UnitDeath()
        {
            _unitAnimator.SetBool(_death, true);
        }

        #endregion

    }
}