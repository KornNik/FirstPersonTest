using UnityEngine;
using UnityEngine.UI;

namespace ExampleTemplate
{
    public class SettingsSliderBehaviour : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Text _state;
        [SerializeField] private Text _nameSettings;
        [SerializeField] private Slider _changingSlider;

        #endregion


        #region ClassLifeCycle



        #endregion


        #region Properties

        public Text States
        {
            get { return _state; }
            private set { }
        }
        public Text NameSettings
        {
            get { return _nameSettings; }
            private set { }
        }
        public Slider Slider
        {
            get { return _changingSlider; }
            private set { }
        }

        #endregion


        #region Methods



        #endregion

    }
}