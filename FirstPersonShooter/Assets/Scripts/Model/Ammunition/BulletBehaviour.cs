using UnityEngine;

namespace ExampleTemplate
{
    public sealed class BulletBehaviour : AmmunitionBehaviour
    {

        #region ClassLifeCycle

        protected override void Awake()
        {
            _ammunitionData = Data.Instance.BulletData;

            base.Awake();

            RegisterBulletModifier(new PoisonDamageModifier(this, _ammunitionData.GetPoisonDamage(), _ammunitionData.GetPoisonDuration()));
        }

        #endregion


        #region UnityMethods

        private void OnCollisionEnter(Collision collision)
        {
            var tempObj = collision.gameObject.GetComponent<IDamageable>();

            foreach (ContactPoint contact in collision.contacts)
            {
                Services.Instance.BulletVFX.GetHitImpactParticle(LayerMask.GetMask(LayerMask.LayerToName(collision.gameObject.layer)), contact.point);
            }
            if (tempObj != null)
            {
                InflictDamage(tempObj);
            }
            ReturnToPool();
        }

        #endregion
    }
}