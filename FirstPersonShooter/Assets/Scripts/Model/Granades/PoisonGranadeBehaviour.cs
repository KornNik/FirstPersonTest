using UnityEngine;

namespace ExampleTemplate
{
    public sealed class PoisonGranadeBehaviour : GranadeBehaviour
    {

        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            RegisterBulletModifier(new PoisonDamageModifier(this, _granadeData.GetPoisonDamage(), _granadeData.GetPoisonDuration()));
        }

        private void OnParticleCollision(GameObject other)
        {
            Debug.Log(other.transform.name);
            var tempTarget = other.GetComponent<IDamageable>();
            if (tempTarget != null)
            {
                InflictDamage(tempTarget);
            }
        }

        #endregion


        #region Methods

        public override void Throw()
        {
            if (!_isReady) return;
            _isReady = false;
            var direction = Services.Instance.CameraServices.CameraMain.transform.forward;
            gameObject.transform.SetParent(null);
            AddForce(direction);
            Invoke(nameof(PlayParticle), _granadeData.GetDelay());
            Invoke(nameof(ReadyThrow), _particleSystem.main.duration + _granadeData.GetDelay());
        }

        #endregion

    }
}