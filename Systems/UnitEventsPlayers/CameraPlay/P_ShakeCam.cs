using System.Collections;
using CameraPlayPack;
using UnityEngine;

namespace GW_Lib.Utility
{
    public class P_ShakeCam : P_CamPlayEffect
    {
        [SerializeField] float shakeTime = 0.2f;
        [SerializeField] float shakeSpeed = 45.0f;
        [SerializeField] float shakeSize = 6.0f;
        [SerializeField] bool wait = false;

        public override IEnumerator Behavior()
        {
            CameraPlay.EarthQuakeShake(shakeTime,shakeSpeed,shakeSize);
            if(wait)
            {
                yield return new WaitForSeconds(shakeTime);
            }
        }
    }
}