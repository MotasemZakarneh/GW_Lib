using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public class InstanceConditionsSource : MonoBehaviour, IConditionsSource
    {
        [SerializeField] ConditionsSource conditionsSource = null;
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