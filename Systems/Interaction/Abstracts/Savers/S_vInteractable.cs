using Honor.Saving;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    [RequireComponent(typeof(vInteractable))]
    public class S_vInteractable : PlayerSavable
    {
        vInteractable interactable => GetComponent<vInteractable>();

        public override void RecoverState(object data)
        {
            interactable.IsActive = (bool)data;
        }
        public override object CaptureState()
        {
            return interactable.IsActive;
        }
    }
}