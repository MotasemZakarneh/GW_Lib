using System.Collections;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public class R_ColsRendsSetter : Reaction
    {
        [SerializeField] Transform scanTransform = null;
        [SerializeField] bool scanChildren = true;
        [SerializeField] bool scanRenderers = true;
        [SerializeField] bool scanColliders = true;
        [SerializeField] bool setScannedTo = false;

        public R_ColsRendsSetter()
        {
            waitEndType = WaitEndType.None;
        }
        protected override IEnumerator Activate()
        {
            yield return 0;
            if (scanChildren)
            {
                if (scanRenderers)
                {
                    Renderer[] renderers = scanTransform.GetComponentsInChildren<Renderer>();
                    foreach (Renderer r in renderers)
                    {
                        r.enabled = setScannedTo;
                    }
                }
                if (scanColliders)
                {
                    Collider[] cols = scanTransform.GetComponentsInChildren<Collider>();
                    foreach (Collider col in cols)
                    {
                        col.enabled = setScannedTo;
                    }
                }
            }
            else
            {
                Renderer r = scanTransform.GetComponent<Renderer>();
                Collider col = scanTransform.GetComponent<Collider>();
                if (r)
                {
                    r.enabled = setScannedTo;
                    col.enabled = setScannedTo;
                }
            }
        }
        protected override void SpecialInit() {}
    }
}