using UnityEngine;

namespace ExampleTemplate
{
    [CreateAssetMenu(fileName = "AmmunitionData", menuName = "Data/Ammunition/ExplosionAmmunitionData")]
    public class ExplosionAmmunitionData : AmmunitionData
    {
        #region Fields

        [Header("Exploison")]
        [SerializeField] private float _blastRadius = 5;
        [SerializeField] private float _blastForce = 700;
        [SerializeField] private float _cameraShakeForce = 0.5f;

        #endregion


        #region Mehtods

        public float GetBlastRadius()
        {
            return _blastRadius;
        }

        public float GetBlastForce()
        {
            return _blastForce;
        }
        public float GetCameraShakeForce()
        {
            return _cameraShakeForce;
        }

        #endregion
    }
}