using System.Collections.Generic;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public abstract class BaseConditionCollection : MonoBehaviour
    {
        #region EditorVariables
        public string description = "Collection Description";
        #endregion

        public Condition[] instanceConditions = new Condition[0];
        public ReactionCollection reactionCollection;
        [SerializeField] DecisionOperator testOperator = DecisionOperator.And;
        protected abstract Condition GetConditionFromSource(Condition basedOnThis);
        protected virtual void Reset()
        {
            GameObject reactionObj = new GameObject("Reactions Obj (" + transform.childCount + ")");
            reactionObj.transform.SetParent(transform);
            reactionObj.transform.localPosition = Vector3.zero;
            reactionObj.transform.localRotation = Quaternion.identity;

            ReactionCollection reactionCollection = reactionObj.AddComponent<ReactionCollection>();
            this.reactionCollection = reactionCollection;
        }
        public bool CollectionSatisfied()
        {
            bool satisfied = true;
            List<bool> testResults = new List<bool>();
            foreach (Condition c in instanceConditions)
            {
                Condition conditionFromSource = GetConditionFromSource(c);
                bool cResult = c.IsSatisfied(conditionFromSource);
                testResults.Add(cResult);
            }
            satisfied = testResults.CheckOperationOnList(testOperator);
            return satisfied;
        }
    }
}