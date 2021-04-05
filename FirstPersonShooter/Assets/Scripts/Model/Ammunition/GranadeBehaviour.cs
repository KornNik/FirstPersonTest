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

        private void OnTriggerEnter(UnityEngine.Collider collision)
        {
            var tempObj = collision.gameObject.GetComponent<IDamageable>();
            var tempRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (tempObj != null)
            {
                InflictDamage(tempObj);
                if (tempRigidbody != null)
                {
                   Collider[] colliders =  Physics.OverlapSphere(transform.position, _radius);
                    foreach(Collider items in colliders)
                    {
                        var rigidbody = items.GetComponent<Rigidbody>();
                        if (rigidbody != null)
                        {
                            rigidbody.AddExplosionForce(_force, transform.position, _radius);
                        }
                    }
                }
            }

            ReturnToPool();
        }

        #endregion

    }
}