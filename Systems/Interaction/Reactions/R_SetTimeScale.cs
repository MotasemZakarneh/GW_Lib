using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public class R_SetTimeScale : Reaction
    {
        [SerializeField] float targetScale = 1.0f;
        [SerializeField] float changeDuration = 0.25f;

        [Header("Read Only")]
        float counter = 0;

        private void Reset()
        {
            waitEndType = WaitEndType.TillDone;
        }

        protected override IEnumerator Activate()
        {
            float startScale = Time.timeScale;
            while(Time.timeScale!=targetScale)
            {
                counter = counter + Time.unscaledDeltaTime/changeDuration;
                Time.timeScale = Mathf.Lerp(startScale,targetScale, counter);

                yield return 0;
            }

            isDone = true;
        }

        protected override void SpecialInit()
        {
            counter = 0;
        }

    }
}