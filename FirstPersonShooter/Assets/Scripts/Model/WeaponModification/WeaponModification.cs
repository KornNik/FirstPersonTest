using UnityEngine;

namespace ExampleTemplate
{
    public abstract class WeaponModification
    {
        #region Mehtods

        public abstract Object AddModification(WeaponBehaviour weapon);
        public abstract void RemoveModification(Object obj);

        #endregion
    }
}