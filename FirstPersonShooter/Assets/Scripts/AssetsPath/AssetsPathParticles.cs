﻿using System.Collections.Generic;

namespace ExampleTemplate
{
    public sealed class AssetsPathParticles 
    {
        #region Fields

        public static Dictionary<VFXType, string> ParticlesGameObject = new Dictionary<VFXType, string>
        {
            {VFXType.TextParticle, "Prefabs/Particles/Prefabs_Particles_TextParticle"},
        };

        #endregion
    }
}