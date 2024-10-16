using UnityEngine;

namespace Scripts.Utils.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 WithZeroY(this Vector3 vector)
        {
            return new Vector3(vector.x, 0f, vector.z);
        }

        public static Vector3 WithZeroX(this Vector3 vector)
        {
            return new Vector3(0f, vector.y, vector.z);
        }

        public static Vector3 WithY(this Vector3 vector, float y)
        {
            return new Vector3(vector.x, y, vector.z);
        }

        public static Vector3 WithZ(this Vector3 vector, float z)
        {
            return new Vector3(vector.x, vector.y, z);
        }

        public static Vector3 WithX(this Vector3 vector, float x)
        {
            return new Vector3(x, vector.y, vector.z);
        }

        public static Vector3 SubtractY(this Vector3 vector, float y)
        {
            return new Vector3(vector.x, vector.y - y, vector.z);
        }

        public static Vector3 AddY(this Vector3 vector, float y)
        {
            return new Vector3(vector.x, vector.y + y, vector.z);
        }
    }
}