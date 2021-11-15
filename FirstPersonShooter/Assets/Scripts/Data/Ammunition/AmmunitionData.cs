using UnityEngine;

namespace ExampleTemplate
{
    [CreateAssetMenu(fileName = "AmmunitionData", menuName = "Data/Ammunition/AmmunitionData")]
    public class AmmunitionData : ScriptableObject
    {
        #region Fields

        [Header("Damage")]
        [SerializeField] protected float _baseDamage = 10;
        [SerializeField] protected float _bonusDamage = 5;
        [SerializeField] protected float _poisonDamage = 1;
        [SerializeField] protected float _poisonDuration = 4;

        [Space]

        [SerializeField] protected float _bulletMass = 0.2f;
        [SerializeField] protected float _timeToDistract = 5;
        [SerializeField] protected float _lossOfDamageAtTime = 0.2f;
        [SerializeField] protected float _finalDamageInPercent = 20;
        [SerializeField] protected float _startPointOfDamageReduction = 100;

        [SerializeField] protected AnimationCurve _damageReductionGraph;

        #endregion


        #region UnityMethods

        public AmmunitionData()
        {
            Keyframe[] damageReductionKeyframes;
            damageReductionKeyframes = new Keyframe[3];

            damageReductionKeyframes[0] = new Keyframe(0, 1);
            damageReductionKeyframes[1] = new Keyframe(_startPointOfDamageReduction / 100, 1);
            damageReductionKeyframes[2] = new Keyframe(1, _finalDamageInPercent / 100);

            _damageReductionGraph = new AnimationCurve(damageReductionKeyframes);
        }

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
        public float GetFinalDamageInPercent()
        {
            return _finalDamageInPercent;
        }
        public float GetStartPointOfDamageReduction()
        {
            return _startPointOfDamageReduction;
        }
        public AnimationCurve GetDamageReductionGraph()
        {
            return _damageReductionGraph;
        }
        public float GetBulletMass()
        {
            return _bulletMass;
        }

        #endregion
    }
}