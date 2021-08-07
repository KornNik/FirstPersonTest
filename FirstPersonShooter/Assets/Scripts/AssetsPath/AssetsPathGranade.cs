using System.Collections.Generic;

namespace ExampleTemplate
{
    public sealed class AssetsPathGranade
    {

        #region Fields

        public static readonly Dictionary<GranadeType, string> GranadeGameObjects = new Dictionary<GranadeType, string>
        {
            {
                GranadeType.Poison, "Prefabs/Granade/Prefabs_Granade_PoisonGranade"
            }

        };

        #endregion
    }
}