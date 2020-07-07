using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

namespace GW_Lib.Interaction_System
{
    public class R_PlayTimeLine : Reaction
    {
        [SerializeField] DirectorWrapMode wrapMode = DirectorWrapMode.None;
        [SerializeField] Command command = Command.Play;
        enum Command { Stop, Play }
        [SerializeField] PlayableDirector timeLine = null;
        protected override IEnumerator Activate()
        {
            if (command == Command.Stop)
            {
                timeLine.Stop();
                isDone = true;
                yield break;
            }
            
            timeLine.extrapolationMode = wrapMode;
            timeLine.Play();

            if (EndWaitTypeIncludesTillDone() == false)
            {
                yield break;
            }

            float dur = (float)timeLine.duration;
            yield return new WaitForSeconds(dur);
            isDone = true;
        }
        protected override void SpecialInit()
        {
            if (wrapMode == DirectorWrapMode.Hold && EndWaitTypeIncludesTillDone())
            {
                Debug.LogWarning
                    ("Disabling TillDone, and setting it to time can't use till done for hold", gameObject);
                waitEndType = WaitEndType.Time;
            }
        }
    }
}