using UnityEngine;

namespace ExampleTemplate
{
    [RequireComponent(typeof(Animator),typeof(WeaponBehaviour))]
    public class WeaponAnimationBehaviour : MonoBehaviour
    {
        #region Fields

        private static readonly int _fireTrigger = Animator.StringToHash(WeaponParametersManager.FIRE);
        private WeaponBehaviour _weaponBehaviour;
        private Animator _weaponAnimator;

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            _weaponBehaviour.FireActn += OnFire;
        }

        private void OnDisable()
        {
            _weaponBehaviour.FireActn -= OnFire;
        }

        private void Awake()
        {
            _weaponAnimator = GetComponent<Animator>();
            _weaponBehaviour = GetComponent<WeaponBehaviour>();
        }
        #endregion


        #region Methods

        private void OnFire()
        {
            _weaponAnimator.SetTrigger(_fireTrigger);
        }

        #endregion
    }
}