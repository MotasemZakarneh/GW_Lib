using System.Collections;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public class R_ChangeCondition : Reaction
    {
        [Header("Reaction Specific")]
        public bool changeGlobalCond;
        public InstanceConditionsSource instanceConditionsSource;
        
        public GlobalLevelConditions levelConditions;

        public Condition newInstanceCondState;
        ConditionsSource conditionsSource;
        protected R_ChangeCondition()
        {
            waitEndType = WaitEndType.None;
        }
        void Reset()
        {
            changeGlobalCond = true;
            GlobalLevelConditionsHolder levelConditionsHolder = FindObjectOfType<GlobalLevelConditionsHolder>();
            if (levelConditionsHolder==null)
            {
                Debug.LogWarning("Each level must have at least one LevelConditionsHolder component on one of its objects");
            }
            else if (levelConditionsHolder.conditionsOfLevel == null)
            {
                Debug.LogWarning("The LevelConditionsHolder of this level, does not have a LevelConditions Asset plugged in it");
            }
            else
            {
                newInstanceCondState = levelConditionsHolder.conditionsOfLevel.GetConditionsSource().GetCondOfId(0);
                levelConditions = levelConditionsHolder.conditionsOfLevel;
            }
        }
        protected override void SpecialInit()
        {
            if (changeGlobalCond)
            {
                conditionsSource = levelConditions.GetConditionsSource();
                return;
            }
            conditionsSource = instanceConditionsSource.GetConditionsSource();
            return;
        }
        protected override IEnumerator Activate()
        {
            conditionsSource.ModifyCondition(newInstanceCondState);
            yield return 0;
        }
    }
}