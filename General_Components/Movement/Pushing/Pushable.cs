using UnityEngine;

namespace GW_Lib.Utility
{
    [System.Serializable]
    public class PushableData
    {
        public float force = 148.0f;
        public float radius = 0.5f;
        public float upwardsModifier = -0.5f;
        public PushableData GetClone()
        {
            return (PushableData)MemberwiseClone();
        }
    }

    [RequireComponent(typeof(Rigidbody))]
    public class Pushable : MonoBehaviour
    {
        protected Rigidbody rb3d = null;

        protected virtual void Awake()
        {
            rb3d = GetComponent<Rigidbody>();
        }
        public void PushSimple(Vector3 force,Vector3 atPos)
        {
            rb3d.AddForceAtPosition(force,atPos);
        }
        public void PushExplosive(PushableData data,Vector3 atPos)
        {
            rb3d.AddExplosionForce(data.force,atPos,data.radius,data.upwardsModifier);
        }
    }
}