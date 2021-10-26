using UnityEngine;

namespace ExampleTemplate
{
    public class ClipModification : IWeaponModification
    {
        #region Fields

        private int _increasAmmo = 10;

        #endregion


        #region Properties

        public int IncreasAmmo { get { return _increasAmmo; } set { _increasAmmo = value; } }

        #endregion


        #region Methods

        public Object AddModification(WeaponBehaviour weapon)
        {
            var clip = Object.Instantiate(Resources.Load<GameObject>(AssetsPathWeaponModification.ModificationsGameObject[ModificationType.Clip]),
            weapon.PlaceForClip.position, weapon.PlaceForClip.rotation, weapon.PlaceForClip);
            weapon.AddAmmunition(_increasAmmo);
            return clip;
        }
        public void RemoveModification(Object obj)
        {
            Object.Destroy(obj);
        }

        #endregion
    }
}