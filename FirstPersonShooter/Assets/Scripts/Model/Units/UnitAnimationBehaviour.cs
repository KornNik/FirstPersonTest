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
        protected static readonly int _punch = Animator.StringToHash(UnitParametrsManager.PUNCH);
        protected static readonly int _aiming = Animator.StringToHash(UnitParametrsManager.AIMING);
        protected static readonly int _tossGranade = Animator.StringToHash(UnitParametrsManager.TOSS_GRANADE);

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

        protected virtual void OnUnitMovingSpeed(float obj)
        {
            _unitAnimator.SetFloat(_movingSpeed, obj);
        }
        protected virtual void OnUnitStrafeSpeed(float obj)
        {
            _unitAnimator.SetFloat(_strafe, obj);
        }
        protected virtual void OnUnitJump()
        {
            _unitAnimator.SetTrigger(_jump);
        }
        protected virtual void OnUnitImpact()
        {
            _unitAnimator.SetTrigger(_impact);
        }
        protected virtual void OnUnitPunch()
        {
            _unitAnimator.SetTrigger(_punch);
        }
        protected virtual void OnUnitTossGranade()
        {
            _unitAnimator.SetTrigger(_tossGranade);
        }
        protected virtual void OnUnitAiming()
        {
            _unitAnimator.SetBool(_aiming, !_unitAnimator.GetBool(_aiming));
        }
        protected virtual void OnUnitDeath()
        {
            _unitAnimator.SetBool(_death, true);
        }

        #endregion

    }
}