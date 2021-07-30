using UnityEngine;

namespace ExampleTemplate
{
    public class MufflerModification : WeaponModification
    {

        #region Fields

        private float _reducingSpread = 0.01f;

        #endregion


        #region Properties

        public float ReducingSpread { get { return _reducingSpread; } private set { } }

        #endregion


        #region Methods

        public override Object AddModification(WeaponBehaviour weapon)
        {
            var muffler = Object.Instantiate(Resources.Load<GameObject>(AssetsPathWeaponModification.ModificationsGameObject[ModificationType.Muffler]),
            weapon.PlaceForMuffler.position, weapon.PlaceForMuffler.rotation, weapon.PlaceForMuffler);
            weapon.RemoveSpread(_reducingSpread);
            return muffler;
        }
        public override void RemoveModification(Object obj)
        {
            Object.Destroy(obj);
        }

        #endregion
    }
}