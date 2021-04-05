using UnityEngine;

namespace ExampleTemplate
{
    public class LoadLevelFromEditor : MonoBehaviour
    {
        [SerializeField] private LevelsType _levelType;
        [SerializeField] private EnemiesType _enemyType;
        [SerializeField] private CharactersType _characterType;

        public void Load()
        {
            if (Application.isPlaying)
                Services.Instance.LoadLevelService.LoadLevel(_levelType, _enemyType, _characterType);
        }
    }
}
