using System.Collections.Generic;


namespace ExampleTemplate
{
    public sealed class AssetsPathEnemies
    {
        #region Fields

        public static readonly Dictionary<EnemiesType, string> EnemiesGameObject = new Dictionary<EnemiesType, string>
        {
            {
                EnemiesType.TestEnemy, "Prefabs/Enemies/Prefabs_Enemies_TestEnemy"
            }

        };

        #endregion
    }
}
