using UnityEngine;
using UnityEngine.UI;

namespace ExampleTemplate
{
    public class SettingsMenuBehaviour : BaseUi
    {
        #region Fields

        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _videoSettingsButton;

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            _mainMenuButton.onClick.AddListener(ShowMainMenuButtonClick);
            _videoSettingsButton.onClick.AddListener(ShowVideoSettingsMenuButtonClick);
        }
        private void OnDisable()
        {
            _mainMenuButton.onClick.RemoveListener(ShowMainMenuButtonClick);
            _videoSettingsButton.onClick.RemoveListener(ShowVideoSettingsMenuButtonClick);
        }

        #endregion


        #region Methods
        public override void Show()
        {
            gameObject.SetActive(true);
            ShowUI.Invoke();
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
            HideUI.Invoke();
        }

        private void ShowMainMenuButtonClick()
        {
            ScreenInterface.GetInstance().Execute(ScreenType.MainMenu);
        }

        private void ShowVideoSettingsMenuButtonClick()
        {
            ScreenInterface.GetInstance().Execute(ScreenType.VideoSettings);
        }

        #endregion

    }
}