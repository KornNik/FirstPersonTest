using System.Collections;
using UnityEngine;

namespace ExampleTemplate
{
    class PerlinNoiseShakeCamera
    {

        #region Fields

        private Transform _cameraTransform;
        private Coroutine _shakeCoroutinePosition;
        private MonoBehaviour _cameraBehaviour;

        #endregion


        #region ClassLifeCycle

        public PerlinNoiseShakeCamera(MonoBehaviour cameraBehaviour ,Transform cameraTransform, float duration, float magnitude, float noize)
        {
            _cameraBehaviour = cameraBehaviour;
            _cameraTransform = cameraTransform;
        }

        #endregion


        #region Methods

        public void CreateShake(float duration, float magnitude, float noize)
        {
            if (_shakeCoroutinePosition == null)
            {
                _shakeCoroutinePosition = _cameraBehaviour.StartCoroutine(ShakeCameraCor(duration, magnitude, noize));
            }
        }

        #endregion

        private IEnumerator ShakeCameraCor(float duration, float magnitude, float noize)
        {
            float elapsed = 0f;
            Vector3 startPosition = _cameraTransform.localPosition;
            Vector2 noizeStartPoint0 = Random.insideUnitCircle * noize;
            Vector2 noizeStartPoint1 = Random.insideUnitCircle * noize;

            while (elapsed < duration)
            {
                Vector2 currentNoizePoint0 = Vector2.Lerp(noizeStartPoint0, Vector2.zero, elapsed / duration);
                Vector2 currentNoizePoint1 = Vector2.Lerp(noizeStartPoint1, Vector2.zero, elapsed / duration);
                Vector2 cameraPostionDelta = new Vector2(Mathf.PerlinNoise(currentNoizePoint0.x, currentNoizePoint0.y), Mathf.PerlinNoise(currentNoizePoint1.x, currentNoizePoint1.y));
                cameraPostionDelta *= magnitude;

                _cameraTransform.localPosition = startPosition + (Vector3)cameraPostionDelta;

                elapsed += Time.deltaTime;

                yield return null;
            }

            _cameraTransform.localPosition = startPosition;
            _shakeCoroutinePosition = null;
        }
    }
}
