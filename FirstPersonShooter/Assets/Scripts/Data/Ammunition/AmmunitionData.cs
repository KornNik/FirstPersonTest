using UnityEngine;

namespace ExampleTemplate
{
    [CreateAssetMenu(fileName = "AmmunitionData", menuName = "Data/Ammunition/AmmunitionData")]
    public class AmmunitionData : ScriptableObject
    {
        #region Fields

        [SerializeField] protected float _baseDamage = 10;
        [SerializeField] protected float _bonusDamage = 5;
        [SerializeField] protected float _poisonDamage = 1;
        [SerializeField] protected float _poisonDuration = 4;
        [SerializeField] protected float _timeToDistract = 5;
        [SerializeField] protected float _lossOfDamageAtTime = 0.2f;

        #endregion


        #region Methods

        public float GetBaseDamage()
        {
            return _baseDamage;
        }
        public float GetBonusDamage()
        {
            return _bonusDamage;
        }
        public float GetPoisonDamage()
        {
            return _poisonDamage;
        }
        public float GetPoisonDuration()
        {
            return _poisonDuration;
        }
        public float GetTimeToDistract()
        {
            return _timeToDistract;
        }
        public float GetLossOfDamage()
        {
            return _lossOfDamageAtTime;
        }

        #endregion
    }
}