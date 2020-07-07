using System.Collections;
using GW_Lib.Interaction_System;
using UnityEngine;

namespace GW_Lib.Utility
{
    public class P_PlayInteractable : UnitPlayable
    {
        [SerializeField] Interactable interactable = null;
        [SerializeField] bool wait = false;
        public override IEnumerator Behavior()
        {
            if (interactable == null)
            {
                yield break;
            }
            if (wait)
            {
                yield return StartCoroutine(interactable.Interact(this));
                yield break;
            }
            StartCoroutine(interactable.Interact(this));
        }
    }
}