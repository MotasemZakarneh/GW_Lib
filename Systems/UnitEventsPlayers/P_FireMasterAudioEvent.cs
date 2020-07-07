using UnityEngine;
using DarkTonic.MasterAudio;
using System.Collections;

namespace GW_Lib.Utility
{
    public class P_FireMasterAudioEvent : UnitPlayable
    {
        [SerializeField] string audioEvent = "";
        [SerializeField] Transform on = null;
        public override IEnumerator Behavior()
        {
            if (MasterAudio.Instance)
            {
                MasterAudio.FireCustomEvent(audioEvent, on);
            }
            yield break;
        }
    }
}