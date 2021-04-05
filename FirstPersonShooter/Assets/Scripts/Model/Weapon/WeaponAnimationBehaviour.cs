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
            WeaponBehaviour.OnFire += Fire;
        }

        private void OnDisable()
        {
            WeaponBehaviour.OnFire -= Fire;
        }

        private void Awake()
        {
            _weaponAnimator = gameObject.GetComponent<Animator>();
        }
        #endregion


        #region Methods

        private void Fire()
        {
            _weaponAnimator.SetTrigger(_fireTrigger);
        }

        #endregion
    }
}