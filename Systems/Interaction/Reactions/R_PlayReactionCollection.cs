using System.Collections;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public class R_PlayReactionCollection : Reaction
    {
        [SerializeField] ReactionCollection reactionCollectionToPlay = null;
        public R_PlayReactionCollection()
        {
            waitEndType = WaitEndType.TillDone;
        }
        protected override IEnumerator Activate()
        {
            yield return StartCoroutine(reactionCollectionToPlay.BeginReacting());
            isDone = true;
        }
        protected override void SpecialInit(){ }
    }
}