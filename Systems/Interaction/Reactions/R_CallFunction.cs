using System.Collections;
using GW_Lib.Utility.Events;
using UnityEngine;
using UnityEngine.Events;

namespace GW_Lib.Interaction_System
{
    public class R_CallFunction : Reaction
    {
        enum EventToUse{Generic,Callabale,Both}
        [SerializeField] EventToUse eventToUse = EventToUse.Generic;
        [SerializeField] UnityEvent genericEvent = null;
        [SerializeField] UnityCallBackEvent callabaleEvent = null;

        protected override IEnumerator Activate()
        {
            if (eventToUse == EventToUse.Generic)
            {
                genericEvent?.Invoke();
            }
            else if(eventToUse == EventToUse.Callabale)
            {
                callabaleEvent?.Invoke(CallBack);
            }
            else if(eventToUse == EventToUse.Callabale)
            {
                callabaleEvent?.Invoke(CallBack);
                genericEvent?.Invoke();
            }
            yield break;
        }

        protected override void SpecialInit()
        {

        }
        private void CallBack()
        {
            isDone = true;
        }
    }
}