using System.Collections;
using CameraPlayPack;
using UnityEngine;

namespace GW_Lib.Utility
{
    public class P_Glitch : P_CamPlayEffect
    {
        [SerializeField] float glitchTime = 0.2f;
        [SerializeField] bool wait = false;
        public override IEnumerator Behavior()
        {
            CameraPlay.Glitch(glitchTime);
            if(wait)
            {
                yield return new WaitForSeconds(glitchTime);
            }
        }
    }
}