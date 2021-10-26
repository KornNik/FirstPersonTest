using UnityEngine;

namespace ExampleTemplate
{
    public interface IWeaponModification
    {
        #region Mehtods

        Object AddModification(WeaponBehaviour weapon);
        void RemoveModification(Object obj);

        #endregion
    }
}