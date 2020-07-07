using System.Collections;
using CameraPlayPack;
using UnityEngine;

namespace GW_Lib.Utility
{
    public class P_HurtCamEffect : UnitPlayable
    {
        [SerializeField] bool wait = false;
        public override IEnumerator Behavior()
        {
            float dur = 0.5f;
            CameraPlay.Shockwave(dur, 0.5f, 0.250f);
            CameraPlay.Glitch(dur);
            CameraPlay.EarthQuakeShake(0.3f);
            if(!wait)
            {
                yield break;
            }
            yield return new WaitForSeconds(dur);
        }
    }
}