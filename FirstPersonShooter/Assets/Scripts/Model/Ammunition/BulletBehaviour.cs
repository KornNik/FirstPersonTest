namespace ExampleTemplate
{
    public sealed class BulletBehaviour : AmmunitionBehaviour
    {
        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            RegisterBulletModifier(new PoisonDamageModifier(this, _poisonDamage, _poisonDuration));
        }

        private void OnCollisionEnter(UnityEngine.Collision collision)
        {
            var tempObj = collision.gameObject.GetComponent<IDamageable>();

            if (tempObj != null)
            {
                InflictDamage(tempObj);
            }

            ReturnToPool();
        }

        #endregion
    }
}