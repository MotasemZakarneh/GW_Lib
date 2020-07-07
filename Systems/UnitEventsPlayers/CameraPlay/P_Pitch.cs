using System.Collections;
using CameraPlayPack;
using UnityEngine;
namespace GW_Lib.Utility
{
    public class P_Pitch : P_CamPlayEffect
    {
        [SerializeField] float pitchTime = 0.4f;
        [SerializeField] float pitchSize = 2.0f;
        [SerializeField] bool wait = false;
        
        public override IEnumerator Behavior()
        {
            Vector3 s = GetPosInViewPort();
            CameraPlay.Pitch(s.x, s.y, pitchTime, pitchSize);
            if(wait)
            {
                yield return new WaitForSeconds(pitchTime);
            }
        }
    }
}