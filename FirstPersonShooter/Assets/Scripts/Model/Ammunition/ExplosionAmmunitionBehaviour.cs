using UnityEngine;
using System;

namespace ExampleTemplate
{
    public sealed class ExplosionAmmunitionBehaviour : AmmunitionBehaviour
    {
        #region Fields

        public static event Action<float,float,float> AmmunitionExplode;

        private ExplosionAmmunitionData _explosionData;

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            _ammunitionData = Data.Instance.ExplosionAmmunitionData;
            _explosionData = Data.Instance.ExplosionAmmunitionData;
            Type = AmmunitionType.Granade;
            base.Awake();
            RegisterBulletModifier(new BonusDamageModifier(this, _ammunitionData.GetBonusDamage()));
        }

        private void OnCollisionEnter(Collision collision)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionData.GetBlastRadius());

            foreach (ContactPoint contact in collision.contacts)
            {
                Services.Instance.BulletVFX.GetHitParticle(Type, contact.point);
            }

            AmmunitionExplode?.Invoke(0.3f,_explosionData.GetCameraShakeForce(),100f);
            ExplosionForce(colliders);

            ReturnToPool();
        }

        #endregion


        #region Methods

        private void ExplosionForce(Collider[] colliders)
        {
            foreach (Collider items in colliders)
            {
                var rigidbody = items.GetComponent<Rigidbody>();
                var damageableObject = items.GetComponent<IDamageable>();
                if (rigidbody != null)
                {
                    rigidbody.AddExplosionForce(_explosionData.GetBlastForce(), transform.position, _explosionData.GetBlastRadius());
                }
                if (damageableObject != null)
                {
                    InflictDamage(damageableObject);
                }
            }
        }

        #endregion

    }
}