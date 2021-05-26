using UnityEngine;

namespace ExampleTemplate
{
    [CreateAssetMenu(fileName = "AmmunitionData", menuName = "Data/Ammunition/GranadeData")]
    public class GranadeData : AmmunitionData
    {
        [SerializeField] private float _blastRadius = 5;
        [SerializeField] private float _blastForce = 700;

        public float GetBlastRadius()
        {
            return _blastRadius;
        }

        public float GetBlastForce()
        {
            return _blastForce;
        }

    }
}