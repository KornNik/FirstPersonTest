namespace ExampleTemplate
{
    public sealed class BulletBehaviour : AmmunitionBehaviour
    {
        #region UnityMethods

        protected override void Awake()
        {
            _ammunitionData = Data.Instance.BulletData;
            base.Awake();
            _ammunitionVFX = new WeaponVFX(VFXType.HitFlash);
            RegisterBulletModifier(new PoisonDamageModifier(this, _ammunitionData.GetPoisonDamage(), _ammunitionData.GetPoisonDuration()));
        }

        private void OnCollisionEnter(UnityEngine.Collision collision)
        {
            var tempObj = collision.gameObject.GetComponent<IDamageable>();

            foreach(UnityEngine.ContactPoint contact in collision.contacts)
            {
                _ammunitionVFX.PlayWeaponParticle(contact.point);
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