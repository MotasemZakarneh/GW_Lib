using System.Collections;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public class R_BehaviorsSetter : Reaction
    {
        [SerializeField] Transform headToScan = null;
        [SerializeField] bool scanChildren = true;
        [SerializeField] bool setActivityTo = false;
        Behaviour[] behaviours = new Behaviour[0];
        public R_BehaviorsSetter()
        {
            waitEndType = WaitEndType.None;
        }
        protected override IEnumerator Activate()
        {
            foreach (Behaviour behavior in behaviours)
            {
                behavior.enabled = setActivityTo;
            }
            isDone = true;
            yield break;
        }
        protected override void SpecialInit()
        {
            if (scanChildren)
            {
                behaviours = headToScan.GetComponentsInChildren<Behaviour>();
            }
            else
            {
                behaviours = headToScan.GetComponents<Behaviour>();
            }
        }
    }
}