using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public class InstanceConditionCollection : BaseConditionCollection
    {
        [SerializeField] InstanceConditionsSource instanceConditionsSource = null;
        public InstanceConditionsSource InstanceConditionsSource { get { return instanceConditionsSource; } }
        public InstanceConditionCollection()
        {
            description = "Instance " + description;
        }
        protected override Condition GetConditionFromSource(Condition basedOnThis)
        {
            Condition condFromSource = instanceConditionsSource.GetConditionsSource().GetCondOfId(basedOnThis.iD);
            if (condFromSource==null)
            {
                condFromSource = instanceConditionsSource.GetConditionsSource().GetCondOfHash(basedOnThis.Hash);
            }
            return condFromSource;
        }
    }
}