using System.Collections;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public class R_SetBehaviorsActivity : Reaction
    {
        [SerializeField] bool setActivityTo = false;
        [SerializeField] Behaviour[] behaviors = new Behaviour[0];
        protected R_SetBehaviorsActivity()
        {
            waitEndType = WaitEndType.None;
        }
        protected override void SpecialInit() { }
        protected override IEnumerator Activate()
        {
            yield return 0;
            foreach (Behaviour b in behaviors)
            {
                b.enabled = setActivityTo;
            }
            yield break;
        }
    }
}