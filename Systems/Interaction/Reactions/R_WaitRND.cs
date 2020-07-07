using System.Collections;
using GW_Lib.Utility;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public class R_WaitRND: Reaction
    {
        [SerializeField] MinMaxRange w = new MinMaxRange(0, 2, 1.5f, 2);
        protected override IEnumerator Activate()
        {
            yield return new WaitForSeconds(w.GetValue());
        }
        protected override void SpecialInit()
        {

        }
    }
}