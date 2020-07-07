using System.Collections;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public class R_SetObjsActivity : Reaction
    {
        [SerializeField] GameObject[] objs = new GameObject[0];
        [SerializeField] bool setActivityTo = false;
        public R_SetObjsActivity()
        {
            waitEndType = WaitEndType.None;
        }
        protected override IEnumerator Activate()
        {
            yield return 0;
            foreach (GameObject o in objs)
            {
                o.SetActive(setActivityTo);
            }
            isDone = true;
        }
        protected override void SpecialInit(){ }
    }
}