using UnityEngine;

namespace ExampleTemplate
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Data/Weapon/WeaponData")]
    public class WeaponData : ScriptableObject
    {

        [SerializeField] private int _countAmmunition = 10;
        [SerializeField] private int _countClip = 5;
        [SerializeField] private float _bulletForce = 600;
        [SerializeField] private float _spreadFactor = 0.03f;
        [SerializeField] private float _rechergeTime = 0.3f;

        public int GetCountAmmunition()
        {
            return _countAmmunition;
        }
        public int GetCountClip()
        {
            return _countClip;
        }
        public float GetBulletForce()
        {
            return _bulletForce;
        }
        public float GetSpreadFactor()
        {
            return _spreadFactor;
        }
        public float GetRechergeTime()
        {
            return _rechergeTime;
        }
    }
}