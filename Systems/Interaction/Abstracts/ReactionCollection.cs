using System.Collections;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    [DisallowMultipleComponent]
    public class ReactionCollection : MonoBehaviour
    {
        Reaction[] reactions = new Reaction[0];

        public IEnumerator BeginReacting()
        {
            yield return 0;
            reactions = GetComponents<Reaction>();
            for (int i = 0; i < reactions.Length; i++)
            {
                yield return StartCoroutine(reactions[i].React());
            }
            yield break;
        }
    }
}
