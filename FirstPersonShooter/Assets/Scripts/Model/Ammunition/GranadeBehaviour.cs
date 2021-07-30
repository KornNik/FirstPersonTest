using UnityEngine;

namespace ExampleTemplate
{
    public sealed class GranadeBehaviour : AmmunitionBehaviour
    {
        #region Fields

        private GranadeData _granadeData;

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            _ammunitionData = Data.Instance.GranadeData;
            _granadeData = Data.Instance.GranadeData;
            Type = AmmunitionType.Granade;
            base.Awake();
            RegisterBulletModifier(new BonusDamageModifier(this, _ammunitionData.GetBonusDamage()));
        }

        private void OnCollisionEnter(Collision collision)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _granadeData.GetBlastRadius());

            foreach (ContactPoint contact in collision.contacts)
            {
                Services.Instance.BulletVFX.GetHitParticle(Type, contact.point);
            }

            ExplosionForce(colliders);

            ReturnToPool();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, _granadeData.GetBlastRadius());   
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
                    rigidbody.AddExplosionForce(_granadeData.GetBlastForce(), transform.position, _granadeData.GetBlastRadius());
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