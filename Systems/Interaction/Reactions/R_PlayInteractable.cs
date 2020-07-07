using System.Collections;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
	public class R_PlayInteractable : Reaction
	{
 		[SerializeField] Interactable interactable = null;
        [SerializeField] bool useSelf = true;

        protected R_PlayInteractable()
        {
			waitEndType = WaitEndType.TillDone;
        }
        protected override IEnumerator Activate()
        {
            if (useSelf)
            {
                yield return StartCoroutine(interactable.Interact(interactable));
            }
            else
            {
                yield return interactable.StartCoroutine(interactable.Interact(interactable));
            }
			isDone = true;
        }
        protected override void SpecialInit(){}
		public void RecieveInteractable(Interactable interactable)
		{
			this.interactable=interactable;
		}
	}
}