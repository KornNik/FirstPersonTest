using UnityEngine;

namespace ExampleTemplate
{
    [RequireComponent(typeof(Animator),typeof(CharacterBehaviour))]
    public sealed class CharacterAnimationBehaviour : UnitAnimationBehaviour
    {
        #region Fields

        private CharacterData _characterData;
        private CharacterBehaviour _characterBehaviour;

        #endregion


        #region UnityMethods
        private void OnEnable()
        {
            _characterBehaviour.MovingSpeed += OnUnitMovingSpeed;
            _characterBehaviour.Strafe += OnUnitStrafeSpeed;
            _characterBehaviour.Jump += OnUnitJump;
            _characterBehaviour.Impact += OnUnitImpact;
            _characterBehaviour.Death += OnUnitDeath;
        }

        private void OnDisable()
        {
            _characterBehaviour.MovingSpeed -= OnUnitMovingSpeed;
            _characterBehaviour.Strafe -= OnUnitStrafeSpeed;
            _characterBehaviour.Jump -= OnUnitJump;
            _characterBehaviour.Impact -= OnUnitImpact;
            _characterBehaviour.Death -= OnUnitDeath;
        }
        private void OnAnimatorIK()
        {
            SetIK(_handWeight);
        }

        protected override void Awake()
        {
            base.Awake();
            _characterData = Data.Instance.Character;
            _characterBehaviour = GetComponent<CharacterBehaviour>();
        }

        #endregion


        #region Methods

        public void SetHandWeight(float handWeight)
        {
            _handWeight = handWeight;
        }

        private void SetIK(float handWeight)
        {
            _unitAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, handWeight);
            _unitAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, handWeight);
            _unitAnimator.SetIKPosition(AvatarIKGoal.RightHand, _characterData.RightHandTarget.position);
            _unitAnimator.SetIKRotation(AvatarIKGoal.RightHand, _characterData.RightHandTarget.rotation);
        }

        #endregion
    }
}