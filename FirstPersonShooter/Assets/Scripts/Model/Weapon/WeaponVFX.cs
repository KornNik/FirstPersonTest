using UnityEngine;

namespace ExampleTemplate
{
    public class WeaponVFX 
    {
        #region Fields

        private ParticleSystem _weaponParticle;

        #endregion


        #region ClassLyfeCycle

        public WeaponVFX(Transform parentTransform, VFXType weaponVFX)
        {
            LoadWeaponParticle(parentTransform, weaponVFX);
        }

        #endregion


        #region Methods

        private void LoadWeaponParticle(Transform parentTransform, VFXType weaponVFX)
        {
            var particle = CustomResources.Load<ParticleSystem>(AssetsPathParticles.ParticlesGameObject[weaponVFX]);
            _weaponParticle = Object.Instantiate(particle, parentTransform.position, parentTransform.rotation, parentTransform);
            _weaponParticle.Stop(true);
        }

        public void PlayWeaponParticle(Vector3 placeToPlay)
        {
            _weaponParticle.transform.position = placeToPlay;
            _weaponParticle.transform.rotation = Quaternion.identity;
            _weaponParticle.Play(true);
        }

        #endregion

    }
}