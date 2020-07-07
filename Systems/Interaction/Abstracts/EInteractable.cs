using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public class EInteractable : Interactable
    {
        public int currInteractionsCount
        {
            get
            {
                return interactionsCount;
            }
            set
            {
                interactionsCount = value;
            }
        }

        [Tooltip("Use -1 for infinite")]
        [SerializeField] int maxInteractions = 1;
        [SerializeField] string eventToListenTo = "";

        Coroutine interactionCoro = null;
        int interactionsCount = 0;
        bool isInfinite => maxInteractions == -1;

        public void RecieveEvent(string e)
        {
            if (e == eventToListenTo)
            {
                TryCallInteraction();
            }
            else if (e.Trim() == eventToListenTo.Trim())
            {
                TryCallInteraction();
            }
        }
        private void TryCallInteraction()
        {
            if (interactionCoro == null)
            {
                if (isInfinite)
                {
                    CallInteraction();
                    return;
                }

                interactionsCount = interactionsCount + 1;
                if(interactionsCount>maxInteractions)
                {
                    return;
                }
                CallInteraction();
            }
        }
        private void CallInteraction()
        {
            interactionCoro = StartCoroutine(Interact(this, OnCompleted, OnCompleted));
        }
        private void OnCompleted()
        {
            interactionCoro = null;
        }
    }
}