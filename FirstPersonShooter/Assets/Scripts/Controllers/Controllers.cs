namespace ExampleTemplate
{
    public sealed class Controllers : IInitialization, ICleanUp
    {
        #region Fields
        
        private readonly IExecute[] _executeControllers;
        private readonly ICleanUp[] _cleanUps;
        private readonly IInitialization[] _initializations;

        #endregion


        #region Properties
        
        public int Length => _executeControllers.Length;

        public IExecute this[int index] => _executeControllers[index];
        
        #endregion
        

        #region ClassLifeCycles
        
        public Controllers()
        {
            _initializations = new IInitialization[3];
            _initializations[0] = new LoadLevelController();
            _initializations[1] = new WeaponController();
            _initializations[2] = new FlashLightController();
            _executeControllers = new IExecute[7];
            _executeControllers[0] = new TimeRemainingController();
            _executeControllers[1] = new InputController();
            _executeControllers[2] = new CameraController();
            _executeControllers[3] = new WeaponController();
            _executeControllers[4] = new FlashLightController();
            _executeControllers[5] = new GranadeController();
            _executeControllers[6] = new SaveController();

            _cleanUps = new ICleanUp[1];
            _cleanUps[0] = new TimeRemainingCleanUp();
        }
        
        #endregion


        #region IInitialization

        public void Initialization()
        {
            for (var i = 0; i < _initializations.Length; i++)
            {
                var initialization = _initializations[i];
                initialization.Initialization();
            }
            
            for (var i = 0; i < _executeControllers.Length; i++)
            {
                var execute = _executeControllers[i];
                if (execute is IInitialization initialization)
                {
                    initialization.Initialization();
                }
            }
        }
        
        #endregion
        
        
        #region ICleanUp

        public void Cleaner()
        {
            for (var index = 0; index < _cleanUps.Length; index++)
            {
                var cleanUp = _cleanUps[index];
                cleanUp.Cleaner();
            }
        }

        #endregion
    }
}
