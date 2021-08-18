using UnityEngine;

namespace ExampleTemplate
{
    [CreateAssetMenu(fileName = "FlashLightData", menuName = "Data/FlashLight/FlashLightData")]
    public class FlashLightData : ScriptableObject
    {

        #region Fields

        [SerializeField] private float _range = 10;
        [SerializeField] private float _spotAngle = 45;
        [SerializeField] private float _batteryChargeMax = 10.0f;
        [SerializeField] private float _maxIntensity = 1.5f;
        [SerializeField] private float _rotationSpeed = 10.0f;
        [SerializeField] private Color _originColor = Color.white;

        #endregion


        #region Methods

        public float GetRange()
        {
            return _range;
        }
        public float GetSpotAngle()
        {
            return _spotAngle;
        }
        public float GetBatteryChargeMax()
        {
            return _batteryChargeMax;
        }
        public float GetMaxIntensity()
        {
            return _maxIntensity;
        }
        public float GetRotationSpeed()
        {
            return _rotationSpeed;
        }
        public Color GetColor()
        {
            return _originColor;
        }
        #endregion

    }
}