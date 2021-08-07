using UnityEngine;

namespace ExampleTemplate
{
    [RequireComponent(typeof(Animator),typeof(CharacterBehaviour))]
    public class CharacterAnimationBehaviour : MonoBehaviour
    {
        #region Fields

        private static readonly int _movingSpeed = Animator.StringToHash(CharacterParametersManager.MOVING_SPEED);
        private static readonly int _strafe = Animator.StringToHash(CharacterParametersManager.STRAFE);
        private static readonly int _jump = Animator.StringToHash(CharacterParametersManager.JUMP);

        private Animator _characterAnimator;
        private CharacterData _characterData;
        private float _handWeight;

        #endregion


        #region UnityMethods
        private void OnEnable()
        {
            CharacterBehaviour.MovingSpeed += CharacterMovingSpeed;
            CharacterBehaviour.Strafe += CharacterStrafeSpeed;
            CharacterBehaviour.Jump += CharacterJump;
        }

        private void OnDisable()
        {
            CharacterBehaviour.MovingSpeed -= CharacterMovingSpeed;
            CharacterBehaviour.Strafe -= CharacterStrafeSpeed;
            CharacterBehaviour.Jump -= CharacterJump;
        }

        private void Awake()
        {
            _characterAnimator = gameObject.GetComponent<Animator>();
            _characterData = Data.Instance.Character;

        }

        private void OnAnimatorIK()
        {
            _characterAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, _handWeight);
            _characterAnimator.SetIKPosition(AvatarIKGoal.RightHand, _characterData.RightHandTarget.position);

            _characterAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, _handWeight);
            _characterAnimator.SetIKRotation(AvatarIKGoal.RightHand, _characterData.RightHandTarget.rotation);

        }

        #endregion


        #region Methods

        private void CharacterMovingSpeed(float obj)
        {
            _characterAnimator.SetFloat(_movingSpeed, obj);
        }
        private void CharacterStrafeSpeed(float obj)
        {
            _characterAnimator.SetFloat(_strafe, obj);
        }
        private void CharacterJump()
        {
            _characterAnimator.SetTrigger(_jump);
        }
        public void SetHandWeight(float handWeight)
        {
            _handWeight = handWeight;
        }

        #endregion
    }
}