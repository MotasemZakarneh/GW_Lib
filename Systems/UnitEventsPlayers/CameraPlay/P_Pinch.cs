using System.Collections;
using CameraPlayPack;
using UnityEngine;

namespace GW_Lib.Utility
{
    public class P_Pinch : P_CamPlayEffect
    {
        [SerializeField] float pinchTime = 1.0f;
        [SerializeField] float pinchSize = 2.0f;
        [SerializeField] bool wait = false;
        public override IEnumerator Behavior()
        {
            Vector3 s = GetPosInViewPort();
            CameraPlay.Pinch(s.x,s.y,pinchTime,pinchSize);

            if(wait)
            {
                yield return new WaitForSeconds(pinchTime);
            }
        }
    }
}