namespace ExampleTemplate
{
    public sealed class BulletBehaviour : AmmunitionBehaviour
    {
        #region UnityMethods

        protected override void Awake()
        {
            _ammunitionData = Data.Instance.BulletData;
            base.Awake();
            RegisterBulletModifier(new PoisonDamageModifier(this, _ammunitionData.GetPoisonDamage(), _ammunitionData.GetPoisonDuration()));
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