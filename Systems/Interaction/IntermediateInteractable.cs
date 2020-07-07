using System.Collections;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public class IntermediateInteractable : Interactivity
    {
		[SerializeField] Interactable interactableToPlay;
        public override IEnumerator Interact(MonoBehaviour caller)
        {
            if(caller==null)
            {
                caller = this;
            }
            yield return caller.StartCoroutine( interactableToPlay.Interact(caller));
        }
        public void SetInteractable(Interactable interactable)
        {
            interactableToPlay=interactable;
        }
    }
}