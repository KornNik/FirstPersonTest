using System.Collections.Generic;

namespace ExampleTemplate
{
    public sealed class AssetsPathWeaponModification 
    {
        #region Fields

        public static readonly Dictionary<ModificationType, string> ModificationsGameObject = new Dictionary<ModificationType, string>
        {
            {
                ModificationType.Muffler,"Prefabs/WeaponsAttachments/Prefabs_WeaponsAttachments_Muffler" 
            },
            {
                ModificationType.Clip,"Prefabs/WeaponsAttachments/Prefabs_WeaponsAttachments_Clip"
            }
        };

        #endregion
    }
}