using System.Collections;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public class R_SetPositions : Reaction
    {

        [SerializeField] Transform[] transformsToSet = new Transform[0];
        [SerializeField] Transform target = null;
        [SerializeField] bool matchRot = false;
        protected override IEnumerator Activate()
        {
            foreach (var t in transformsToSet)
            {
                t.position = target.position;
                if(matchRot)
                {
                    t.rotation = target.rotation;
                }
            }
            yield break;
        }

        protected override void SpecialInit()
        {

        }

    }
}