using UnityEngine;

namespace ExampleTemplate
{
    public sealed class BulletVFX : Service
    {
        #region Fields

        private ParticleSystem _enemyHitParticle;
        private ParticleSystem _groundHitParticle;
        private ParticleSystem _granadeExplosionParticle;

        #endregion


        #region ClassLyfeCycle

        public BulletVFX()
        {
            _enemyHitParticle = LoadParticle(VFXType.BloodSplash);
            _groundHitParticle = LoadParticle(VFXType.HitFlash);
            _granadeExplosionParticle = LoadParticle(VFXType.GranadeExplosion);
        }

        #endregion


        #region Methods

        public void GetHitImpactParticle(int hitMask, Vector3 placeToPlay)
        {
            if (hitMask == LayerManager.EnemyLayer)
            {
                PlayParticle(_enemyHitParticle, placeToPlay);
            }
            if (hitMask == LayerManager.GroundLayer)
            {
                PlayParticle(_groundHitParticle, placeToPlay);
            }
        }
        public void GetHitParticle(AmmunitionType ammunitionType, Vector3 placeToPlay)
        {
            if(ammunitionType == AmmunitionType.Granade)
            {
                PlayParticle(_granadeExplosionParticle, placeToPlay);
            }
        }

        private ParticleSystem LoadParticle(VFXType type)
        {
            var particle = CustomResources.Load<ParticleSystem>(AssetsPathParticles.ParticlesGameObject[type]);
            var hitParticle = Object.Instantiate(particle);
            hitParticle.Stop(true);
            return hitParticle;
        }
        private void PlayParticle(ParticleSystem hitParticle, Vector3 placeToPlay)
        {
            hitParticle.transform.position = placeToPlay;
            hitParticle.transform.rotation = Quaternion.identity;
            hitParticle.Play(true);
        }

        #endregion

    }
}