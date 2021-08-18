using System.Collections;
using UnityEngine;

namespace ExampleTemplate
{
    public sealed class CameraBehaviuor : MonoBehaviour
    {
        #region Fields

        private CameraData _cameraData;
        private Quaternion _cameraTargetRot;
        private Coroutine _shakeCoroutinePosition;

        #endregion


        #region UnityMethods

        private void Awake()
		{
			_cameraData = Data.Instance.Camera;
            _cameraTargetRot = gameObject.transform.localRotation;
            
        }

        private void OnEnable()
        {
            ExplosionAmmunitionBehaviour.AmmunitionExplode += ShakeCamera;
        }
        private void OnDisable()
        {
            ExplosionAmmunitionBehaviour.AmmunitionExplode -= ShakeCamera;
        }

        #endregion


        #region Methods

        public void LookRotation(Vector2 mouseAxis, Transform character)
        {
            character.localRotation *= Quaternion.Euler(0f, mouseAxis.x, 0f);
            _cameraTargetRot *= Quaternion.Euler(-mouseAxis.y, 0f, 0f);

            if (_cameraData.GetIsClampRotation())
            { _cameraTargetRot = ClampRotationAroundXAxis(_cameraTargetRot); }

            gameObject.transform.localRotation = _cameraTargetRot;
        }
        public void ShakeCamera(float duration, float magnitude, float noize)
        {
            if (_shakeCoroutinePosition == null)
            {
                _shakeCoroutinePosition = StartCoroutine(ShakeCameraCor(duration, magnitude, noize));
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

        #region IEnumerator

        private IEnumerator ShakeCameraCor(float duration, float magnitude, float noize)
        {
            float elapsed = 0f;
            Vector3 startPosition = transform.localPosition;
            Vector2 noizeStartPoint0 = Random.insideUnitCircle * noize;
            Vector2 noizeStartPoint1 = Random.insideUnitCircle * noize;

            while (elapsed < duration)
            {
                Vector2 currentNoizePoint0 = Vector2.Lerp(noizeStartPoint0, Vector2.zero, elapsed / duration);
                Vector2 currentNoizePoint1 = Vector2.Lerp(noizeStartPoint1, Vector2.zero, elapsed / duration);
                Vector2 cameraPostionDelta = new Vector2(Mathf.PerlinNoise(currentNoizePoint0.x, currentNoizePoint0.y), Mathf.PerlinNoise(currentNoizePoint1.x, currentNoizePoint1.y));
                cameraPostionDelta *= magnitude;

                transform.localPosition = startPosition + (Vector3)cameraPostionDelta;

                elapsed += Time.deltaTime;

                yield return null;
            }

            transform.localPosition = startPosition;
            _shakeCoroutinePosition = null;
        }

        #endregion
    }
}
