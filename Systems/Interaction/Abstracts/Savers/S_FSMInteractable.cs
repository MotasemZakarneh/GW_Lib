using System.Collections.Generic;
using Honor.Saving;
using HutongGames.PlayMaker;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    [RequireComponent(typeof(PlayMakerFSM))]
    public class S_FSMInteractable : PlayerSavable
    {
        PlayMakerFSM fsm;
        const string currInteractionsCount_KEY = "currInteractionsCount";

        void Start()
        {
            fsm = GetComponent<PlayMakerFSM>();
        }
        public override void RecoverState(object data)
        {
            Dictionary<string,object> state = GetState(data);

            int currInteractionsCount = FileSaver.JToObject<int>(state[currInteractionsCount_KEY]);
            FsmInt i = fsm.FsmVariables.GetFsmInt(currInteractionsCount_KEY);
            if(i!=null)
            {
                Debug.LogError("Un Expected, failed to Load, can't find correct interactable variables");
                return;
            }

            i.Value = currInteractionsCount;
            fsm.SendEvent("Start");
        }

        public override object CaptureState()
        {
            Dictionary<string,object> data = GetNewDataStruct();
            data["name"] = name;
            FsmInt i = fsm.FsmVariables.GetFsmInt(currInteractionsCount_KEY);
            if(i==null)
            {
                Debug.LogError("Un Expected, failed to save, can't find correct interactable variables");
                data[currInteractionsCount_KEY] = 0;
            }
            else
            {
                data[currInteractionsCount_KEY] = i.Value;
            }
            return data;
        }
    }
}