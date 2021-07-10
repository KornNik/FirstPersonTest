using UnityEngine;


namespace ExampleTemplate
{
    public sealed class ScreenFactory
    {
        #region Fields

        private GameMenuBehaviour _gameMenu;
        private MainMenuBehaviour _mainMenu;
        private SettingsMenuBehaviour _settingsMenu;
        private VideoSettingsBehaviour _videoSettingsMenu;
        private Canvas _canvas;

        #endregion


        #region ClassLifeCycles

        public ScreenFactory()
        {
            var resources = CustomResources.Load<Canvas>(AssetsPathGameObject.GameObjects[GameObjectType.Canvas]);
            _canvas = Object.Instantiate(resources, Vector3.one, Quaternion.identity);
        }

        #endregion
        

        #region Methods

        public GameMenuBehaviour GetGameMenu()
        {
            if (_gameMenu == null)
            {
                var resources = CustomResources.Load<GameMenuBehaviour>(AssetsPathScreen.Screens[ScreenType.GameMenu].Screen);
                _gameMenu = Object.Instantiate(resources, _canvas.transform.position, Quaternion.identity, _canvas.transform);
            }
            return _gameMenu;
        }

        public MainMenuBehaviour GetMainMenu()
        {
            if (_mainMenu == null)
            {
                var resources = CustomResources.Load<MainMenuBehaviour>(AssetsPathScreen.Screens[ScreenType.MainMenu].Screen);
                _mainMenu = Object.Instantiate(resources, _canvas.transform.position, Quaternion.identity, _canvas.transform);
            }
            return _mainMenu;
        }

        public SettingsMenuBehaviour GetSettingsMenu()
        {
            if (_settingsMenu == null)
            {
                var resourses = CustomResources.Load<SettingsMenuBehaviour>(AssetsPathScreen.Screens[ScreenType.Settings].Screen);
                _settingsMenu = Object.Instantiate(resourses, _canvas.transform.position, Quaternion.identity, _canvas.transform);
            }
            return _settingsMenu;
        }

        public VideoSettingsBehaviour GetVideoSettingsMenu()
        {
            if (_videoSettingsMenu == null)
            {
                var resourses = CustomResources.Load<VideoSettingsBehaviour>(AssetsPathScreen.Screens[ScreenType.VideoSettings].Screen);
                _videoSettingsMenu = Object.Instantiate(resourses, _canvas.transform.position, Quaternion.identity, _canvas.transform);
            }
            return _videoSettingsMenu;
        }

        #endregion
    }
}
