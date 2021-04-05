using UnityEngine;

namespace ExampleTemplate
{
    public class WeaponModificationBehaviour : MonoBehaviour
    {
        #region Fields

        private MufflerModification _mufflerModification;
        private ClipModification _clipModification;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _mufflerModification = new MufflerModification();
            _clipModification = new ClipModification();
         
        }

        private void OnTriggerEnter(Collider other)
        {
            var character = other.transform.root.GetComponent<CharacterBehaviour>();
            if (character != null)
            {
                if (Services.Instance.WeaponService.Weapon != null)
                {
                    if (!Services.Instance.WeaponService.Weapon._isClipModificated)
                    {
                        var clip = _clipModification.AddModification(Services.Instance.WeaponService.Weapon);
                        Services.Instance.WeaponService.Weapon._isClipModificated = true;
                    }
                    if (!Services.Instance.WeaponService.Weapon._isMufflerModificated)
                    {
                        var muffler = _mufflerModification.AddModification(Services.Instance.WeaponService.Weapon);
                        Services.Instance.WeaponService.Weapon._isMufflerModificated = true;
                    }
                }
            }

        }

        #endregion
    }
}