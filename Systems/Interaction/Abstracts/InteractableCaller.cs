using System.Collections;
using GW_Lib.Interaction_System;
using UnityEngine;

namespace Honor.Utility
{
    [RequireComponent(typeof(Interactable))]
    public class InteractableCaller : MonoBehaviour
    {
        public string onDoneEvent = "Interactable/onInteractionDone";
        public PlayMakerFSM fsm = null;

        void Reset()
        {
            fsm = GetComponent<PlayMakerFSM>();
        }
        public void Interact()
        {
            StartCoroutine(CallInteract());
        }

        private IEnumerator CallInteract()
        {
            yield return StartCoroutine(GetComponent<Interactable>().Interact(this));
            yield return 0;
            if(fsm == null)
            {
                yield break;
            }
            fsm.SendEvent(onDoneEvent);
        }
    }
}