using System.Collections.Generic;

namespace ExampleTemplate
{
    public sealed class AssetsPathAmmunition
    {
        #region Fields

        public static readonly Dictionary<AmmunitionType, string> AmmunitionsGameObject = new Dictionary<AmmunitionType, string>
        {
            {
                AmmunitionType.Bullet, "Prefabs/Ammunition/Prefabs_Ammunition_Bullet"
            },
            {
                AmmunitionType.Granade,"Prefabs/Ammunition/Prefabs_Ammunition_Granade"
            }
        };

        #endregion

    }
}