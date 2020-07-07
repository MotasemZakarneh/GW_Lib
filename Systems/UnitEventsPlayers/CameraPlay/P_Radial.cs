using System.Collections;
using CameraPlayPack;
using UnityEngine;

namespace GW_Lib.Utility
{
    public class P_Radial : P_CamPlayEffect
    {
        [SerializeField] float radialTime = 0.5f;
        [SerializeField] float radialSize = 1.0f;
        [SerializeField] bool wait = false;
        public override IEnumerator Behavior()
        {
            Vector3 s = GetPosInViewPort();
            CameraPlay.Radial(s.x,s.y,radialTime,radialSize);
            if(wait)
            {
                yield return new WaitForSeconds(radialTime);
            }
        }
    }
}