using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public class GlobalConditionCollection : BaseConditionCollection
    {
        public GlobalLevelConditions levelConditions;
        public GlobalConditionCollection()
        {
            description = "Global " + description;
        }
        protected override Condition GetConditionFromSource(Condition basedOnThis)
        {
            Condition condFromSource = levelConditions.GetConditionsSource().GetCondOfId(basedOnThis.iD);
            if (condFromSource == null)
            {
                condFromSource = levelConditions.GetConditionsSource().GetCondOfHash(basedOnThis.Hash);
            }
            return condFromSource;
        }
        protected override void Reset()
        {
            base.Reset();
            GlobalLevelConditionsHolder levelConditionsHolder = FindObjectOfType<GlobalLevelConditionsHolder>();

            if (levelConditionsHolder == null)
            {
                Debug.LogWarning("Couldn't Find A Level Conditions Holder, Each Scene Must Contain One");
            }
            else if (levelConditionsHolder.conditionsOfLevel == null)
            {
                Debug.LogWarning("The Level Does Have A LevelConditionsHolder, But that component, does not have a set conditions asset");
            }
            else
            {
                levelConditions = levelConditionsHolder.conditionsOfLevel;
            }

        }
    }
}