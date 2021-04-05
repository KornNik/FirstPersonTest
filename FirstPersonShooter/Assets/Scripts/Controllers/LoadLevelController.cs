namespace ExampleTemplate
{
    public class LoadLevelController : IInitialization
    {

        #region IInitialization

        public void Initialization()
        {
            Services.Instance.LoadLevelService.LoadLevel(LevelsType.TestLevel, EnemiesType.TestEnemy, CharactersType.TestCharacter);
        }

        #endregion
    }
}