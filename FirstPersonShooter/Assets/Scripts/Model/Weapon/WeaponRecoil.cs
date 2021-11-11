using UnityEngine;
using System.Collections;

namespace ExampleTemplate
{
    public class WeaponRecoil
    {

        #region Fields

        private WeaponData _weaponData;
        private Coroutine _recoilCoroutine;
        private Transform _weaponTransform;
        private MonoBehaviour _weaponBehaviour;

        private Vector3 _weaponRecoil;
        protected Vector3 _weaponOriginPosition;

        float yvelocity = 0.0f;
        float xvelocity = 0.0f;
        float zvelocity = 0.0f;

        #endregion


        #region ClassLifeCycle

        public WeaponRecoil(MonoBehaviour weaponBehaviour, WeaponData weaponData, Transform weaponTransform)
        {
            _weaponBehaviour = weaponBehaviour;
            _weaponData = weaponData;
            _weaponTransform = weaponTransform;
            _weaponOriginPosition = _weaponTransform.localEulerAngles;
        }

        #endregion


        #region Methods

        public void MakeRecoil()
        {
            _weaponRecoil = new Vector3(Random.Range(-_weaponData.GetWeaponRecoilX(), _weaponData.GetWeaponRecoilX()),
                Random.Range(0, _weaponData.GetWeaponRecoilY()), 0);
            _weaponTransform.localEulerAngles -= _weaponRecoil;
            if (_recoilCoroutine == null)
            {
                _recoilCoroutine = _weaponBehaviour.StartCoroutine(RecoilReturn());
            }
        }

        private void ReturnFromRecoil()
        {
            float x = Mathf.SmoothDampAngle(_weaponTransform.localEulerAngles.x, _weaponOriginPosition.x, ref xvelocity, _weaponData.GetRecoilTimeMultiplier());
            float y = Mathf.SmoothDampAngle(_weaponTransform.localEulerAngles.y, _weaponOriginPosition.y, ref yvelocity, _weaponData.GetRecoilTimeMultiplier());
            float z = Mathf.SmoothDampAngle(_weaponTransform.localEulerAngles.z, _weaponOriginPosition.z, ref zvelocity, _weaponData.GetRecoilTimeMultiplier());
            _weaponTransform.localEulerAngles = new Vector3(x, y, z);
        }

        #endregion

        #region IEnumerator

        private IEnumerator RecoilReturn()
        {
            while (!Mathf.Approximately(_weaponOriginPosition.x, _weaponTransform.localEulerAngles.x) 
                && !Mathf.Approximately(_weaponOriginPosition.y, _weaponTransform.localEulerAngles.y))
            {
                yield return _weaponData.GetReturnRecoilDelay();
                ReturnFromRecoil();
            }
            _recoilCoroutine = null;
        }

        #endregion
    }
}
