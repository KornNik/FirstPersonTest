using UnityEngine;
using System;

namespace ExampleTemplate
{
    public sealed class ExplosionAmmunitionBehaviour : AmmunitionBehaviour
    {
        #region Fields

        /// <summary>
        /// float - duartion, float - magnitude, float - noize
        /// </summary>
        public static event Action<float, float, float> AmmunitionExplode;
        /// <summary>
        /// float - duration, float - strength, int - vibrato, float - randomness
        /// </summary>
        public static event Action<float, float, int, float> AmmunitionExplodeTween;

        private ExplosionAmmunitionData _explosionData;

        #endregion


        #region ClassLifeCycle

        protected override void Awake ()
        {
            _ammunitionData = Data.Instance.BulletData;
            _explosionData = Data.Instance.ExplosionAmmunitionData;

            base.Awake();

            _type = AmmunitionType.Granade;
            RegisterBulletModifier(new BonusDamageModifier(this, _ammunitionData.GetBonusDamage()));
        }

        #endregion


        #region UnityMethods

        private void OnCollisionEnter(Collision collision)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionData.GetBlastRadius());

            foreach (ContactPoint contact in collision.contacts)
            {
                Services.Instance.BulletVFX.GetHitParticle(_type, contact.point);
            }

            AmmunitionExplode?.Invoke(0.3f, _explosionData.GetCameraShakeForce(), 100f);
            AmmunitionExplodeTween?.Invoke(0.3f, _explosionData.GetCameraShakeForce(), 10, 90f);
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