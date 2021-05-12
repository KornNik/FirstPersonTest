using UnityEngine;

namespace ExampleTemplate
{
    public sealed class GranadeBehaviour : AmmunitionBehaviour
    {
        #region Fields

        private float _radius = 5f;
        private float _force = 700f;

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            RegisterBulletModifier(new BonusDamageModifier(this, _bonusDamage));
        }

        private void OnCollisionEnter(Collision collision)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

            ExplosionForce(colliders);

            ReturnToPool();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, _radius);   
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
                    rigidbody.AddExplosionForce(_force, transform.position, _radius);
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