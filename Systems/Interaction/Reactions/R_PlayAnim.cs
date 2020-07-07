using System.Collections;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public class R_PlayAnim : Reaction
    {
        [SerializeField] Animator animator = null;
        enum AnimVarSettings{Trigger,Bool}
        [SerializeField] AnimVarSettings settings = AnimVarSettings.Trigger;
        [SerializeField] string animVar = "";

        [Header("Bool Values")]
        [SerializeField] bool animVarBValue = false;

        protected override IEnumerator Activate()
        {
            switch (settings)
            {
                case AnimVarSettings.Trigger:
                    animator.SetTrigger(animVar);
                    break;
                case AnimVarSettings.Bool:
                    animator.SetBool(animVar,animVarBValue);
                    break;
            }

            yield break;
        }
        protected override void SpecialInit(){}
    }
}