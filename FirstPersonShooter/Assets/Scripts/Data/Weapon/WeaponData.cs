using UnityEngine;

namespace ExampleTemplate
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Data/Weapon/WeaponData")]
    public class WeaponData : ScriptableObject
    {

        [SerializeField] private int _countAmmunition = 10;
        [SerializeField] private int _countClip = 5;
        [SerializeField] private float _bulletForce = 600f;
        [SerializeField] private float _spreadFactor = 0.03f;
        [SerializeField] private float _rechergeTime = 0.3f;
        [SerializeField] private float _waitForReturnRecoil = 0.005f;
        [SerializeField] private float _recoilTimeMultiplier = 2f;
        [Range(1f, 7f)]
        [SerializeField] private float _weaponRecoilX = 5f;
        [Range(1f, 15f)]
        [SerializeField] private float _weaponRecoilY = 8f;

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
        public float GetReturnRecoilDelay()
        {
            return _waitForReturnRecoil;
        }
        public float GetRecoilTimeMultiplier()
        {
            return _recoilTimeMultiplier;
        }
        public float GetWeaponRecoilX()
        {
            return _weaponRecoilX;
        }
        public float GetWeaponRecoilY()
        {
            return _weaponRecoilY;
        }
    }
}