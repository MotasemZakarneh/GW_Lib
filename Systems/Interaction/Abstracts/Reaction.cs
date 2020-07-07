using System.Collections;
using StealthGame;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    [RequireComponent(typeof (ReactionCollection))]
    public abstract class Reaction : MonoBehaviour
    {
        [SerializeField] protected float timeBeforeStart;
        [Tooltip("Gets Used Only If, Wait Type Includes Time")]
        [SerializeField] protected float reactionDuration;
        protected enum WaitEndType{None,Time,TillDone,TimeAndTillDone}
        [Tooltip("if TillDone is included, it will have to be fixed within code of the reaction")]
        [SerializeField] protected WaitEndType waitEndType = WaitEndType.Time;

        protected bool isDone = true;
        protected bool IsDone() { return isDone; }
        public IEnumerator React()
        {
            if (EndWaitTypeIncludesTillDone())
            {
                isDone=false;    
            }
            SpecialInit();
            yield return StartCoroutine(StartReaction());
        }
        protected abstract void SpecialInit();
        protected abstract IEnumerator Activate();
        protected virtual void CancelReaction()
        {

        }
        private IEnumerator StartReaction()
        {
            //Debug.Log(transform.parent.parent);
            //Debug.Log("Starting Reaction_(" + GetType() + ")");
            yield return new WaitForSeconds(timeBeforeStart);
            StartCoroutine( Activate());
            yield return StartCoroutine(CallReactionEnd());
        }
        private IEnumerator CallReactionEnd()
        {
            if (EndWaitTypeIncludesTime())
            {
                yield return new WaitForSeconds(reactionDuration);    
            }
            if (EndWaitTypeIncludesTillDone()==false)
            {
                isDone=true;
            }
            else if (EndWaitTypeIncludesTillDone())
            {
                yield return new WaitUntil(IsDone);
            }
            yield return 0;
            OnReactionDone();
        }


        protected virtual void OnReactionDone(){ }
        protected bool EndWaitTypeIncludesTime()
        {
            return waitEndType==WaitEndType.Time||waitEndType == WaitEndType.TimeAndTillDone;
        }
        protected bool EndWaitTypeIncludesTillDone()
        {
            return waitEndType == WaitEndType.TimeAndTillDone|| waitEndType == WaitEndType.TillDone;
        }
    }
}