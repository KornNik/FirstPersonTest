using System.Collections;
using UnityEngine;

namespace ExampleTemplate
{
    public sealed class CameraBehaviuor : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Transform _rightHandTarget;

        private CameraData _cameraData;
        private Quaternion _cameraTargetRot;

        private DOTweenShakeCamera _tweenShakeCamera;
        private PerlinNoiseShakeCamera _perlinShakeCamera;

        #endregion


        #region Properties

        public Transform RightHandTarget => _rightHandTarget;

        #endregion


        #region UnityMethods

        private void Awake()
		{
			_cameraData = Data.Instance.Camera;
            _cameraTargetRot = gameObject.transform.localRotation;
        }

        private void OnEnable()
        {
            //ExplosionAmmunitionBehaviour.AmmunitionExplode += CreatePerlinNoiseShake;
            ExplosionAmmunitionBehaviour.AmmunitionExplodeTween += CreateDOTweenShake;
        }
        private void OnDisable()
        {
            //ExplosionAmmunitionBehaviour.AmmunitionExplode -= CreatePerlinNoiseShake;
            ExplosionAmmunitionBehaviour.AmmunitionExplodeTween -= CreateDOTweenShake;
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

        private void CreateDOTweenShake(float duration, float strength, int vibrato, float randomness)
        {
            if (_tweenShakeCamera == null)
            {
                _tweenShakeCamera = new DOTweenShakeCamera(transform, duration, strength, vibrato, randomness);
            }
            _tweenShakeCamera.CreateShake(duration, strength, vibrato, randomness);
        }

        private void CreatePerlinNoiseShake(float duration, float magnitude, float noize)
        {
            if (_perlinShakeCamera == null)
            {
                _perlinShakeCamera = new PerlinNoiseShakeCamera(this, transform, duration, magnitude, noize);
            }
            _perlinShakeCamera.CreateShake(duration, magnitude, noize);

        }

        #endregion
    }
}
