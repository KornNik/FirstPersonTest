using UnityEngine;

namespace ExampleTemplate
{
    public sealed class CameraBehaviuor : MonoBehaviour
    {
        #region Fields

        private CameraData _cameraData;
        private Transform _cameraTransform;
        private Quaternion _cameraTargetRot;

        #endregion


        #region UnityMethods

        private void Awake()
		{
			_cameraData = Data.Instance.Camera;
            _cameraTargetRot = gameObject.transform.rotation;
            _cameraTransform = gameObject.transform;
        }

        #endregion


        #region Methods

        public void LookRotation(Vector2 mouseAxis,Transform character)
        {
            character.rotation *= Quaternion.Euler(0f, mouseAxis.x, 0f);
            _cameraTargetRot *= Quaternion.Euler(-mouseAxis.y, 0f, 0f);

            if (_cameraData.GetIsClampRotation())
                _cameraTargetRot = ClampRotationAroundXAxis(_cameraTargetRot);

            if (_cameraData.GetIsSmooth())
            {
                character.localRotation = Quaternion.Slerp(character.localRotation, character.rotation, _cameraData.GetSmoothTime() * Time.deltaTime);
                _cameraTransform.localRotation = Quaternion.Slerp(_cameraTransform.localRotation, _cameraTargetRot, _cameraData.GetSmoothTime() * Time.deltaTime);
            }
            else
            {
                character.localRotation = character.rotation;
                _cameraTransform.localRotation = _cameraTargetRot;
            }
        }

        private Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

            angleX = Mathf.Clamp(angleX, _cameraData.GetMimimumX(), _cameraData.GetMaximumX());

            q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }

        #endregion
    }
}
