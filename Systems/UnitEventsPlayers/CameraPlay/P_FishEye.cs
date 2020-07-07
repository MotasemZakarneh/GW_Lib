using System.Collections;
using CameraPlayPack;
using UnityEngine;

namespace GW_Lib.Utility
{
    public class P_FishEye : P_CamPlayEffect
    {
        [SerializeField] float fishEyeTime = 0.6f;
        [SerializeField] bool wait = false;

        public override IEnumerator Behavior()
        {
            Vector3 s = GetPosInViewPort();
            CameraPlay.FishEye(s.x,s.y,fishEyeTime);
            if(wait)
            {
                yield return new WaitForSeconds(fishEyeTime);
            }
        }
    }
}