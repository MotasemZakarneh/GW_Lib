using System.Collections.Generic;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    [System.Serializable]
    public class ConditionsSource
    {
        public Condition[] conditions = new Condition[0];
        public List<bool> initialStates = new List<bool>();

        public ConditionsSource()
        {
            conditions = new Condition[1];
            conditions[0] = new Condition(0, "Initial Condition", false);
            initialStates = new List<bool>();
            initialStates.Add(false);
        }
        public Condition GetCondOfKey(string key, bool getOri = false)
        {
            return GetCondOfHash(Animator.StringToHash(key), getOri);
        }
        public Condition GetCondOfHash(int hash, bool getOri = false)
        {
            foreach (Condition cond in conditions)
            {
                if (cond.Hash == hash)
                {
                    return SafeGetCond(cond, getOri);
                }
            }
            return null;
        }
        public Condition GetCondOfId(int id, bool getOri = false)
        {
            foreach (Condition cond in conditions)
            {
                if (cond.iD == id)
                {
                    return SafeGetCond(cond, getOri);
                }
            }
            return null;
        }

        public void ModifyCondition(Condition newInstanceCondState)
        {
            Condition cToModify = GetCondOfId(newInstanceCondState.iD, true);
            cToModify.satisfied = newInstanceCondState.satisfied;
        }

        public bool TryGetCondition(int hash, ref Condition c, bool getOri = false)
        {
            foreach (Condition cond in conditions)
            {
                if (cond.Hash == hash)
                {
                    c = SafeGetCond(cond, getOri);
                    return true;
                }
            }
            return false;
        }
        private Condition SafeGetCond(Condition c, bool getOri)
        {
            if (getOri)
            {
                return c;
            }
            return c.Clone();
        }
        public string[] GetNamesOfCondsInSource()
        {
            string[] conditionsNames = new string[conditions.Length];
            for (int i = 0; i < conditionsNames.Length; i++)
            {
                conditionsNames[i] = conditions[i].description;
            }
            return conditionsNames;
        }
        public void ReSet()
        {
            for (int i = 0; i < conditions.Length; i++)
            {
                conditions[i].satisfied = initialStates[i];
            }
        }
    }
}