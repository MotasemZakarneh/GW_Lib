using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public class R_TweenColor : Reaction
    {
        [SerializeField] GameObject[] objs = new GameObject[0];
        [SerializeField] string[] colorProps = { "_Color", "_EmissionColor", "_LavaEmissionColor", "_RimColor" };
        [ColorUsage(true,true)]
        [SerializeField] Color targetColor = Color.white;
        [SerializeField] float dur = 0.45f;
        List<MeshRenderer> mrs = new List<MeshRenderer>();

        protected override IEnumerator Activate()
        {
            foreach (Renderer r in mrs)
            {
                DOTween.To(() => r.material.color, (c) => r.material.color = c, targetColor, dur).SetEase(Ease.InOutSine);
            }
            yield break;
        }

        protected override void SpecialInit()
        {

            foreach(GameObject o in objs)
            {
                foreach (var r in o.GetComponents<MeshRenderer>())
                {
                    if (mrs.Contains(r)) continue;
                    mrs.Add(r);
                }
            }
        }
    }
}