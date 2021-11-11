using UnityEngine;
using DG.Tweening;

namespace ExampleTemplate
{
    public class DOTweenShakeCamera
    {
        #region Fields

        private Transform _cameraTransform;

        #endregion


        #region ClassLifeCycle

        public DOTweenShakeCamera(Transform cameraTransform, float duration, float strength, int vibrato, float randomness)
        {
            _cameraTransform = cameraTransform;
        }

        #endregion


        #region Methods

        public void CreateShake(float duration, float strength, int vibrato, float randomness)
        {
            Tweener tweener = DOTween.Shake(() => _cameraTransform.localPosition, pos => _cameraTransform.localPosition = pos, duration, strength, vibrato, randomness, true);
        }

        #endregion
    }
}
