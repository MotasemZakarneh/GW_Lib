using System.Collections;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public class R_Debug : Reaction
    {
        enum MessageType { Log, Warning, Error }
        [SerializeField] MessageType mType = MessageType.Log;
        [SerializeField] string message = "";
        protected override IEnumerator Activate()
        {
            DoDebug();
            yield break;
        }
        protected override void SpecialInit()
        {

        }
        public void DoDebug()
        {
            switch (mType)
            {
                case MessageType.Log:
                    Debug.Log(message);
                    break;
                case MessageType.Warning:
                    Debug.LogWarning(message);
                    break;
                case MessageType.Error:
                    Debug.LogError(message);
                    break;
            }
        }
    }
}