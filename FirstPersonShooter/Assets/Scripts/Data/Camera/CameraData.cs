using UnityEngine;

namespace ExampleTemplate
{
    [CreateAssetMenu(fileName = "CameraData", menuName = "Data/Camera/CameraData")]
    public sealed class CameraData : ScriptableObject
    {
        #region Fields

        [SerializeField] private float _ySensitivity = 2f;
        [SerializeField] private float _xSensitivity = 2f;
        [SerializeField] private float _maximumX = 90f;
        [SerializeField] private float _minimumX = -90f;
        [SerializeField] private float _smoothTime = 100f;
        [SerializeField] private bool _isSmooth = true;
        [SerializeField] private bool _isClampRotation = true;

        #endregion


        #region Methods

        public float GetYSensitivity()
        {
            return _ySensitivity;
        }
        public float GetXSensitivity()
        {
            return _xSensitivity;
        }
        public float GetMaximumX()
        {
            return _maximumX;
        }
        public float GetMimimumX()
        {
            return _minimumX;
        }
        public float GetSmoothTime()
        {
            return _smoothTime;
        }
        public bool GetIsSmooth()
        {
            return _isSmooth;
        }
        public bool GetIsClampRotation()
        {
            return _isClampRotation;
        }

        #endregion
    }
}
