using UnityEngine;
using System.Collections;

namespace ExampleTemplate
{
    [RequireComponent(typeof(Animator),typeof(WeaponBehaviour))]
    public class WeaponAnimationBehaviour : MonoBehaviour
    {
        #region Fields

        private static readonly int _fireTrigger = Animator.StringToHash("Fire");
        private Animator _weaponAnimator;

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            WeaponBehaviour.FireActn += OnFire;
        }

        private void OnDisable()
        {
            WeaponBehaviour.FireActn -= OnFire;
        }

        private void Awake()
        {
            _weaponAnimator = gameObject.GetComponent<Animator>();
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