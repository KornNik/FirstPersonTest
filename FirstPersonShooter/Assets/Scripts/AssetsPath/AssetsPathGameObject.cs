using System.Collections.Generic;

namespace ExampleTemplate
{
    public sealed class AssetsPathGameObject
    {
        #region Fields

        public static readonly Dictionary<GameObjectType, string> GameObjects = new Dictionary<GameObjectType, string>
        {
            {
                GameObjectType.Canvas, "GUI/GUI_Canvas"
            },
            {                
                GameObjectType.Character, "Prefabs/Characters/Prefabs_Characters_FPSCharacter"
            }
        };

        #endregion
    }
}
