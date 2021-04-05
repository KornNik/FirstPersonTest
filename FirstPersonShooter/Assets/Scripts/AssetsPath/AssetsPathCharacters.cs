using System.Collections.Generic;

namespace ExampleTemplate
{
    public sealed class AssetsPathCharacters
    {
        #region Fields

        public static readonly Dictionary<CharactersType, string> CharactersGameObject = new Dictionary<CharactersType, string>
        {
            {
                CharactersType.TestCharacter, "Prefabs/Characters/Prefabs_Characters_FPSCharacter"
            }

        };

        #endregion
    }
}
