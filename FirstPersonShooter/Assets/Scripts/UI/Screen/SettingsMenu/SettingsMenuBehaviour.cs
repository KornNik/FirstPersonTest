using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace ExampleTemplate
{
    public class SettingsMenuBehaviour : BaseUi
    {
        #region Fields

        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _videoSettingsButton;
        [SerializeField] private RectTransform _middlePanel;

        private SettingsPanelTween _panelTween;
        private SequenceSettings _sequenceSettings;

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            _panelTween = new SettingsPanelTween(_middlePanel);
            _sequenceSettings = new SequenceSettings(_panelTween);


            _mainMenuButton.onClick.AddListener(ShowMainMenuButtonClick);
            _videoSettingsButton.onClick.AddListener(ShowVideoSettingsMenuButtonClick);
        }
        private void OnDisable()
        {
            _panelTween = null;
            _sequenceSettings = null;

            _mainMenuButton.onClick.RemoveListener(ShowMainMenuButtonClick);
            _videoSettingsButton.onClick.RemoveListener(ShowVideoSettingsMenuButtonClick);
        }

        #endregion


        #region Methods

        public override void Show()
        {
            gameObject.SetActive(true);
            _panelTween.GoToEnd(MoveMode.Hide);
            _sequenceSettings.Move(MoveMode.Show);
            ShowUI.Invoke();
        }

        public override void Hide()
        {
            _sequenceSettings.Move(MoveMode.Hide).AppendCallback(() => gameObject.SetActive(false));
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