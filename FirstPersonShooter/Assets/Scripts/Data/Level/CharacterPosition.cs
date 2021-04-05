using System;
using UnityEngine;

namespace ExampleTemplate
{
    [Serializable]
    public sealed class CharacterPosition
    {
        #region Fields

        public Vector3 Position;
        public Vector3 RotationVector3;

        #endregion


        #region Properties

        public Quaternion Rotation()
        {
            return Quaternion.Euler(RotationVector3);
        }

        #endregion
    }
}