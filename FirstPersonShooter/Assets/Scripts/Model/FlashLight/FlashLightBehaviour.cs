using System;
using UnityEngine;

namespace ExampleTemplate
{
    public sealed class FlashLightBehaviour : MonoBehaviour
    {
		#region Fields

		public static event Action<float> ChargeChange;

        private Light _light;
		private Camera _camera;
		private Vector3 _vecOffset;
		private Transform _goFollow;
		private FlashLightData _flashLightData;

		private float _share;
		private float _batteryChargeMax;
		private float _takeAwayTheIntensity;
		private float _batteryChargeCurrent;

		private bool _lightIsConfigure;


		#endregion


		#region Properties

		public float Charge => _batteryChargeCurrent / BatteryChargeMax;
		public float BatteryChargeMax => _batteryChargeMax;

		public float BatteryChargeCurrent => _batteryChargeCurrent;

		#endregion


		#region UnityMethods

		private void Awake()
		{
			_camera = Services.Instance.CameraServices.CameraMain;
			_flashLightData = Data.Instance.FlashLightData;

			_batteryChargeMax = _flashLightData.GetBatteryChargeMax();

			_goFollow = _camera.transform;
			transform.position = _camera.transform.position;
			_vecOffset = transform.position - _goFollow.position;

			_batteryChargeCurrent = BatteryChargeMax;
			_share = BatteryChargeMax / 4;
			_takeAwayTheIntensity = _flashLightData.GetMaxIntensity() / (BatteryChargeMax * 200);

		}

        #endregion


        #region Methods

        public void Switch(bool value)
		{
            if (!TryGetComponent<Light>(out _light)) { return; }

            _light.enabled = value;

			if (!value) { return; }
            if (!_lightIsConfigure) { ConfigureLight(_light); }

			transform.position = _goFollow.position + _vecOffset;
			transform.rotation = _goFollow.rotation;
		}

		public void Rotation()
		{
			if (!_light) return;
			transform.position = _goFollow.position + _vecOffset;
			transform.rotation = Quaternion.Lerp(transform.rotation, _goFollow.rotation, 
				_flashLightData.GetRotationSpeed() * Time.deltaTime);
		}

		public bool EditBatteryCharge()
		{
			if (_batteryChargeCurrent > 0)
			{
				_batteryChargeCurrent -= Time.deltaTime;
				ChargeChange?.Invoke(_batteryChargeCurrent/_flashLightData.GetBatteryChargeMax());
				_light.intensity -= _takeAwayTheIntensity;

				if (_batteryChargeCurrent < _share)
				{
					_light.enabled = UnityEngine.Random.Range(0, 100) >= UnityEngine.Random.Range(0, 10);
				}

				return true;
			}

			return false;
		}

		public bool BatteryRecharge()
		{
			if (_batteryChargeCurrent < BatteryChargeMax)
			{
				_batteryChargeCurrent += Time.deltaTime;
				ChargeChange?.Invoke(_batteryChargeCurrent/_flashLightData.GetBatteryChargeMax());
				_light.intensity += _takeAwayTheIntensity;
				_light.intensity = Mathf.Clamp(_light.intensity, 0, 1.5f);
				return true;
			}
			return false;
		}

		private void ConfigureLight(Light light)
        {
			light.intensity = _flashLightData.GetMaxIntensity();
			light.spotAngle = _flashLightData.GetSpotAngle();
			light.range = _flashLightData.GetRange();
			light.color = _flashLightData.GetColor();
			_lightIsConfigure = true;
		}

        #endregion
    }
}