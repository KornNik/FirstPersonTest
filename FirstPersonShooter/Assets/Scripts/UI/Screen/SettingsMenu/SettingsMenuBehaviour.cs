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
        private Sequence _sequence;

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            _panelTween = new SettingsPanelTween(_middlePanel);

            _mainMenuButton.onClick.AddListener(ShowMainMenuButtonClick);
            _videoSettingsButton.onClick.AddListener(ShowVideoSettingsMenuButtonClick);
        }
        private void OnDisable()
        {
            _panelTween = null;

            _mainMenuButton.onClick.RemoveListener(ShowMainMenuButtonClick);
            _videoSettingsButton.onClick.RemoveListener(ShowVideoSettingsMenuButtonClick);
        }

        #endregion


        #region Methods

        private Sequence Move(MoveMode mode)
        {
            float timeScale = 1.0f;

            if (_sequence != null)
            {
                timeScale = _sequence.position / _sequence.Duration();
                _sequence.Kill();
            }

            _sequence = DOTween.Sequence();
            _sequence.Join(_panelTween.Move(mode, timeScale));
            _sequence.AppendCallback(() =>
            {
                _sequence = null;

            });

            return _sequence;
        }

        public override void Show()
        {
            gameObject.SetActive(true);
            _panelTween.GoToEnd(MoveMode.Hide);
            Move(MoveMode.Show);
            ShowUI.Invoke();
        }

        public override void Hide()
        {
            //gameObject.SetActive(false);
            Move(MoveMode.Hide).AppendCallback(() => gameObject.SetActive(false));
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