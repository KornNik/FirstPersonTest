using UnityEngine;

namespace ExampleTemplate
{
    [CreateAssetMenu(fileName = "GranadeData", menuName = "Data/Granade/GranadeData")]
    public class GranadeData : ScriptableObject
    {
        [SerializeField] private float _radius = 25;
        [SerializeField] private float _damage = 15;
        [SerializeField] private float _poisonDamage = 5;
        [SerializeField] private float _poisonDuration = 5;
        [SerializeField] private float _delayExplosion = 5;
        [SerializeField] private float _timeToDistract = 5;
        [SerializeField] private float _throwForce = 800;
        [SerializeField] private int _particlesCount = 20;

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
        public float GetTrowForce()
        {
            return _throwForce;
        }
        public int GetParticlesCount()
        {
            return _particlesCount;
        }
    }
}