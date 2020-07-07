using GW_Lib.Interaction_System;
using UnityEngine;

namespace Honor.Saving
{
    [RequireComponent(typeof(StartInteractableCaller))]
    public class S_StartInteractable : PlayerSavable
    {
        StartInteractableCaller caller => GetComponent<StartInteractableCaller>();
        public override void RecoverState(object data)
        {
            caller.RunOnStart = (bool)data;
        }
        public override object CaptureState()
        {
            return caller.RunOnStart;
        }
    }
}