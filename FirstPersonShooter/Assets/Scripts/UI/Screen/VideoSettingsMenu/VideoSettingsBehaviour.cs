using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace ExampleTemplate
{
    public class VideoSettingsBehaviour : BaseUi
    {
        #region Fields

        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _applyButton;
        [SerializeField] private Dropdown _presetsDropdown;
        [SerializeField] private Dropdown _resolutinsDropdown;
        [SerializeField] private GameObject _settingsPanel;

        private SettingsSliderBehaviour[] _settingsSliders;
        private Resolution[] _resolutions;

        #endregion

        #region Properties

        public GameObject SettingsPanel { get { return _settingsPanel; } private set { } }

        #endregion

        #region UnityMethods
        protected override void Awake()
        {
            _resolutions = Screen.resolutions;

            DropdownAction(_presetsDropdown, QualitySettings.names.ToList(), QualitySettings.GetQualityLevel());
            DropdownAction(_resolutinsDropdown, FillDropdownResolutions(), 
                GetCurrentResolutionIndex(FillDropdownResolutions()));
        }

        private void OnEnable()
        {
            _settingsButton.onClick.AddListener(ShowSettingsMenuButtonClick);
            _applyButton.onClick.AddListener(ApplySettingsButtonClick);
            _presetsDropdown.onValueChanged.AddListener(delegate { SetQuality(_presetsDropdown.value); });
            _resolutinsDropdown.onValueChanged.AddListener(delegate { SetResolution(_resolutinsDropdown.value); });
        }

        private void OnDisable()
        {
            _settingsButton.onClick.RemoveListener(ShowSettingsMenuButtonClick);
            _applyButton.onClick.RemoveListener(ApplySettingsButtonClick);
            _presetsDropdown.onValueChanged.RemoveListener(delegate { SetQuality(_presetsDropdown.value); });
            _resolutinsDropdown.onValueChanged.RemoveListener(delegate { SetResolution(_resolutinsDropdown.value); });
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

        private void ApplySettingsButtonClick()
        {

        }

        private void SetQuality(int qualityLevel)
        {
            QualitySettings.SetQualityLevel(qualityLevel);
        }

        private void ShowSettingsMenuButtonClick()
        {
            ScreenInterface.GetInstance().Execute(ScreenType.Settings);
        }

        private void DropdownAction(Dropdown dropdown, List<string> options,int value)
        {
            dropdown.ClearOptions();
            dropdown.AddOptions(options);
            dropdown.value = value;
        }

        private List<string> FillDropdownResolutions()
        {
            List<string> listResolutions = new List<string>();
            for (int i = 0; i < _resolutions.Length; i++)
            {
                var resolution = _resolutions[i].width + "x" + _resolutions[i].height;
                listResolutions.Add(resolution);
 
            }
            return listResolutions;
        }

        private int GetCurrentResolutionIndex(List<string> listResolutions)
        {
            var currentResolutionIndex = 0;
            for (int i = 0; i < listResolutions.Count; i++)
            {
                var resolution = listResolutions[i];
                var subs = resolution.Split('x');
                var width = int.Parse(subs[0]);
                var height = int.Parse(subs[1]);

                if (width == Screen.currentResolution.width && 
                    height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }
            return currentResolutionIndex;
        }

        private void SetResolution(int resolutionIndex)
        {
            var resolution = _resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
        }

        #endregion
    }
}