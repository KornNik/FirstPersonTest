using UnityEngine;

namespace ExampleTemplate
{
    public class ClipModification : WeaponModification
    {
        #region Fields

        private int _increasAmmo = 10; 

        #endregion

        #region Methods

        public override Object AddModification(WeaponBehaviour weapon)
        {
            var clip = Object.Instantiate(Resources.Load<GameObject>(AssetsPathWeaponModification.ModificationsGameObject[ModificationType.Clip]),
            weapon.ClipTransform().position, weapon.ClipTransform().rotation, weapon.ClipTransform());
            weapon.AddAmmunition(_increasAmmo);
            return clip;
        }
        public override void RemoveModification(Object obj)
        {
            Object.Destroy(obj);
        }

        #endregion
    }
}