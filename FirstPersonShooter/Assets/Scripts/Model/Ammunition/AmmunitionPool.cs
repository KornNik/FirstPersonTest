using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ExampleTemplate
{
    public sealed class AmmunitionPool
    {
        #region Fields

        private readonly Dictionary<AmmunitionType, HashSet<AmmunitionBehaviour>> _ammunitionPool;
        private readonly int _capacityPool;
        private readonly Transform _poolTransform;

        #endregion


        #region ClassLifeCycles

        public AmmunitionPool(int capacityPool,Transform poolTransform)
        {
            _ammunitionPool = new Dictionary<AmmunitionType, HashSet<AmmunitionBehaviour>>();
            _capacityPool = capacityPool;
            _poolTransform = poolTransform;
        }

        #endregion


        #region Methods
        public AmmunitionBehaviour GetAmmunition(AmmunitionType type)
        {
            AmmunitionBehaviour result;
            switch (type)
            {
                case AmmunitionType.Bullet:
                    result = GetBullet(GetListAmmunition(type));
                    break;
                case AmmunitionType.Granade:
                    result = GetGranade(GetListAmmunition(type));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return result;
        }

        private HashSet<AmmunitionBehaviour> GetListAmmunition(AmmunitionType type)
        {
            return _ammunitionPool.ContainsKey(type) ? _ammunitionPool[type] : _ammunitionPool[type] = new HashSet<AmmunitionBehaviour>();
        }

        private AmmunitionBehaviour GetBullet(HashSet<AmmunitionBehaviour> ammunitions)
        {
            var ammunition = ammunitions.FirstOrDefault(a => !a.gameObject.activeSelf);
            if (ammunition == null)
            {
                var bullet = CustomResources.Load<BulletBehaviour>(AssetsPathAmmunition.AmmunitionsGameObject[AmmunitionType.Bullet]);
                for (var i = 0; i < _capacityPool; i++)
                {
                    var instantiate = Object.Instantiate(bullet);
                    ReturnToPool(instantiate.transform);
                    instantiate.PoolTransform = _poolTransform;
                    ammunitions.Add(instantiate);
                }

                GetBullet(ammunitions);
            }
            ammunition = ammunitions.FirstOrDefault(a => !a.gameObject.activeSelf);
            return ammunition;
        }
        private AmmunitionBehaviour GetGranade(HashSet<AmmunitionBehaviour> ammunitions)
        {
            var ammunition = ammunitions.FirstOrDefault(a => !a.gameObject.activeSelf);
            if (ammunition == null)
            {
                var granade = CustomResources.Load<GranadeBehaviour>(AssetsPathAmmunition.AmmunitionsGameObject[AmmunitionType.Granade]);
                for (var i = 0; i < _capacityPool; i++)
                {
                    var instantiate = Object.Instantiate(granade);
                    ReturnToPool(instantiate.transform);
                    instantiate.PoolTransform = _poolTransform;
                    ammunitions.Add(instantiate);
                }

                GetGranade(ammunitions);
            }
            ammunition = ammunitions.FirstOrDefault(a => !a.gameObject.activeSelf);
            return ammunition;
        }

        private void ReturnToPool(Transform transform)
        {
            transform.SetParent(_poolTransform);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.gameObject.SetActive(false);
        }

        public void RemovePool()
        {
            Object.Destroy(_poolTransform.gameObject);
        }

        #endregion

    }
}