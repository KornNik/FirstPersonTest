using System.Collections.Generic;

namespace ExampleTemplate
{
    public sealed class AssetsPathWeapons
    {
        #region Fields

        public static readonly Dictionary<WeaponType, string> WeaponsGameObject = new Dictionary<WeaponType, string>
        {
            {
                WeaponType.Pistol, "Prefabs/Weapons/Prefab_Weapons_Pistol" 
            },
            {
                WeaponType.GranadeLauncher, "Prefabs/Weapons/Prefab_Weapons_GranadeLauncher"
            }
        };

        #endregion
    }
}