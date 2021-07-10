using System;

namespace ExampleTemplate
{
    public class FlashLightService
    {

        #region Fields

        private FlashLightBehaviour _flashLight;
        private bool _isFlashLight;

        #endregion


        #region Properties

        public FlashLightBehaviour FlashLight { get { return _flashLight; } private set { } }
        public bool IsFlashLight { get { return _isFlashLight; } private set { } }

        #endregion


        #region Methods

        public void On()
        {
            if (_isFlashLight) { return; }
            _isFlashLight = true;

            if (_flashLight == null) { return; }
            if (_flashLight.BatteryChargeCurrent <= 0) return;

            _flashLight.Switch(true);

        }

        public void Off()
        {
            if (!_isFlashLight) { return; }
            _isFlashLight = false;
            _flashLight.Switch(false);

        }

        public void Switch(FlashLightBehaviour flashLightBehaviour)
        {
            _flashLight = flashLightBehaviour as FlashLightBehaviour;
            if (_isFlashLight)
            {
                Off();
            }
            else
            {
                On();
            }
        }

        #endregion
    }
}
