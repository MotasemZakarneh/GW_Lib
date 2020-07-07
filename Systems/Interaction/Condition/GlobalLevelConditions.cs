using UnityEngine;
using System.Collections.Generic;

namespace GW_Lib.Interaction_System
{
    [CreateAssetMenu(fileName = "Level Conditions" , menuName ="GW_Lib/Scene Essentials/Level Conditions")]
    public class GlobalLevelConditions : ScriptableObject, IConditionsSource
    {
        public Condition[] conditions { get { return conditionsSource.conditions; } }
        public List<bool> initialStates { get { return conditionsSource.initialStates; } }

        [SerializeField] ConditionsSource conditionsSource = null;

        public void ReSet()
        {
            conditionsSource.ReSet();
        }
        public ConditionsSource GetConditionsSource()
        {
            return conditionsSource;
        }

        public Object GetHolderObject()
        {
            return this;
        }
    }
}