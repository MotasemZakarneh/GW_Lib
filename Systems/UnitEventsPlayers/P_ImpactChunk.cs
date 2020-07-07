using System.Collections;
using UnityEngine;

namespace GW_Lib.Utility
{
    public class P_ImpactChunk : UnitPlayable
    {
        [SerializeField] float radious = 0.5f;
        [SerializeField] float force = 1.0f;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radious);
        }

        public override IEnumerator Behavior()
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, radious);
            foreach (Collider col in cols)
            {
                FracturedChunk chunk = col.GetComponent<FracturedChunk>();
                if (chunk==null)
                {
                    continue;
                }
                chunk.Impact(transform.position, force, radious, true);
                chunk.FracturedObjectSource.SetSingleMeshVisibility(false);
            }

            yield break;
        }
    }
}