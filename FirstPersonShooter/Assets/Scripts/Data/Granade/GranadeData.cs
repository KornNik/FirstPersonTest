using UnityEngine;

namespace ExampleTemplate
{
    [CreateAssetMenu(fileName = "GranadeData", menuName = "Data/Granade/GranadeData")]
    public class GranadeData : ScriptableObject
    {
        [SerializeField] private float _radius;
        [SerializeField] private float _damage;
        [SerializeField] private float _poisonDamage;
        [SerializeField] private float _poisonDuration;
        [SerializeField] private float _delayExplosion;
        [SerializeField] private float _timeToDistract;

        public float GetRadius()
        {
            return _radius;
        }
        public float GetDamage()
        {
            return _damage;
        }
        public float GetDelay()
        {
            return _delayExplosion;
        }
        public float GetTimeToDistract()
        {
            return _timeToDistract;
        }
        public float GetPoisonDamage()
        {
            return _poisonDamage;
        }
        public float GetPoisonDuration()
        {
            return _poisonDuration;
        }
    }
}