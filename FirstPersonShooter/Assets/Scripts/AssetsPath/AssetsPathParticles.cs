using System.Collections.Generic;

namespace ExampleTemplate
{
    public sealed class AssetsPathParticles 
    {
        #region Fields

        public static Dictionary<VFXType, string> ParticlesGameObject = new Dictionary<VFXType, string>
        {
            {VFXType.TextParticle, "Prefabs/Particles/Prefabs_Particles_TextParticle"},
            {VFXType.MuzzleFlash,"Prefabs/Particles/Prefabs_Particles_MuzzleFlash" },
            {VFXType.HitFlash,"Prefabs/Particles/Prefabs_Particles_HitFlash" },
            {VFXType.BloodSplash,"Prefabs/Particles/Prefabs_Particles_BloodSplash" },
            {VFXType.GranadeExplosion,"Prefabs/Particles/Prefabs_Particles_GranadeExplosion" },
            {VFXType.PoisonExplosion,"Prefabs/Particles/Prefabs_Particles_PoisonExplosion" }
        };

        #endregion
    }
}