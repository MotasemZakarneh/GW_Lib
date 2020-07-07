using Honor.Saving;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    [RequireComponent(typeof(EInteractable))]
    public class S_EInteractable : PlayerSavable
    {
        EInteractable eInteractable => GetComponent<EInteractable>();

        public override void RecoverState(object data)
        {
            int currInteractionsCount = FileSaver.JToObject<int>(data);
            eInteractable.currInteractionsCount = currInteractionsCount;
        }
        public override object CaptureState()
        {
            return eInteractable.currInteractionsCount;
        }
    }
}