using UnityEngine;

namespace ExampleTemplate
{
    public sealed class WeaponCrosshair
    {
        #region Fields

        private Transform _barrel;
        private Transform _crosshair;

        private float _rayDistance = 200f;
        private float _scaleMulty = 0.01f;
        private int _bulletLayer = 1 << 10;
        private int _crosshairLayer = 1 << 11;
        private int _ignoreLayer;

        #endregion


        #region ClassLifeCycle

        public WeaponCrosshair(Transform barrel, Transform crosshair)
        {
            _barrel = barrel;
            _crosshair = crosshair;
            _ignoreLayer = ~(_bulletLayer | _crosshairLayer);
        }

        #endregion


        #region Methods

        public void CrossHair(bool isActiveMesh)
        {
            var RaycastHit = Physics.Raycast(_barrel.position, _barrel.forward, out RaycastHit hitInfo, _rayDistance, _ignoreLayer);
            if (RaycastHit)
            {
                float scale = Vector3.Distance(_crosshair.position, _barrel.position);
                _crosshair.localScale = Vector3.one * scale * _scaleMulty;
                _crosshair.position = hitInfo.point;
                _crosshair.GetComponent<Renderer>().enabled = isActiveMesh;
            }
            else { _crosshair.GetComponent<Renderer>().enabled = false; }
        }

        #endregion

    }
}