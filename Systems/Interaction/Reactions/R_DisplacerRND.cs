using System.Collections;
using GW_Lib.Utility;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public class R_DisplacerRND : Reaction
    {
        [SerializeField] Transform[] targets = null;
        [SerializeField] bool displaceOnStart = true;
        [SerializeField] MinMaxRange dist = new MinMaxRange(0, 4, 0.75f, 1.65f);
        [Header("Sampling Dasta")]
        [SerializeField] bool sampleToNavMesh = true;
        [SerializeField] float heigth = 2;
        [SerializeField] int minHeigthStep = -3, maxHeigthStep = 3,maxSampleMulti = 4;

        protected override IEnumerator Activate()
        {
            foreach (var target in targets)
            {
                Displace(target, dist.GetValue());
            }
            yield return 0;
        }
        protected override void SpecialInit()
        {

        }

        public void Displace(Transform target,float f)
        {
            Vector3 pos = target.position + UnityEngine.Random.onUnitSphere * f;
            pos = Extentions.GetNavigablePos(target.position, pos, heigth, minHeigthStep, maxHeigthStep, maxSampleMulti, false);
            target.position = pos;
        }

    }
}