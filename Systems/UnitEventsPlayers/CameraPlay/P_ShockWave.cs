using System.Collections;
using CameraPlayPack;
using UnityEngine;

namespace GW_Lib.Utility
{
    public class P_ShockWave : P_CamPlayEffect
    {
        [SerializeField] float shockWaveTime = 0.6f;
        [SerializeField] float shockWaveSize = 6.0f;
        [SerializeField] bool wait = false;

        public override IEnumerator Behavior()
        {
            Vector3 s = GetPosInViewPort();
            CameraPlay.Shockwave(s.x,s.y,shockWaveTime,shockWaveSize);
            if(wait)
            {
                yield return new WaitForSeconds(shockWaveTime);
            }
        }
    }
}