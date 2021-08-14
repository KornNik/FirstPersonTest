using UnityEngine;

namespace ExampleTemplate
{
    public abstract class UnitsData : ScriptableObject
    {
        #region Fields

        [SerializeField] protected float _baseMovingSpeed = 10f;
        [SerializeField] protected float _baseHealth = 50f;
        [SerializeField] protected float _baseArmor = 3f;
        [SerializeField] protected float _waitForRevive = 3f;
        [SerializeField] protected float _switchToRagdollTime = 2f;

        #endregion


        #region Methods

        public float GetBaseMovingSpeed()
        {
            return _baseMovingSpeed;
        }
        public float GetBaseHealth()
        {
            return _baseHealth;
        }
        public float GetBaseArmor()
        {
            return _baseArmor;
        }
        public float GetReviveTime()
        {
            return _waitForRevive;
        }
        public float GetRagdollTime()
        {
            return _switchToRagdollTime;
        }

        #endregion
    }
}