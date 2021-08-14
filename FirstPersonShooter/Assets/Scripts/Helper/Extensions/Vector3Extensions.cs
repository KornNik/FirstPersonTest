using UnityEngine;

namespace ExampleTemplate
{
    public static partial class Vector3Extensions
    {
        public static Vector3 GetRoundPosition(this Vector3 value)
        {
            value.x = Mathf.Round(value.x * 100f)/100f;
            value.y = Mathf.Round(value.y * 100f)/100f;
            value.z = Mathf.Round(value.z * 100f)/100f;
            return value;
        }

        public static Vector3 MultiplyZ(this Vector3 value, int multiplayer)
        {
            value.z *= multiplayer;
            return value;
        }
        public static Vector3 MultiplyY(this Vector3 value, int multiplayer)
        {
            value.x *= multiplayer;
            return value;
        }
        public static Vector3 MultiplyX(this Vector3 value, int multiplayer)
        {
            value.y *= multiplayer;
            return value;
        }
        public static Vector3 ClampVector(this Vector3 fromClamp, Vector3 clampTo)
        {
            var tempVector = Vector3.zero;
            tempVector.x = Mathf.Clamp(tempVector.x, fromClamp.x, clampTo.x);
            tempVector.y = Mathf.Clamp(tempVector.y, fromClamp.y, clampTo.y);
            tempVector.z = Mathf.Clamp(tempVector.z, fromClamp.z, clampTo.z);
            return tempVector;
        }
    }
}
