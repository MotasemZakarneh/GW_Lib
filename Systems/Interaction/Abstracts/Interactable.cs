using System;
using System.Collections;
using System.Collections.Generic;
using GW_Lib.Utility.Events;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    public enum InteractType { External, Tag, Player }
    public class Interactable : Interactivity
    {
        [SerializeField] Transform condCollectionsObj = null;
        [SerializeField] bool lockPlayerTillDone = false;
        [SerializeField] ReactionCollection defaultReactionCollection = null;
        [SerializeField] DecisionOperator usageOperator = DecisionOperator.And;

        public CompoundEvent onInteractionStart = null;
        public CompoundEvent onInteractionEnd = null;

        BaseConditionCollection[] conditionCollections = new BaseConditionCollection[0];
        List<Func<bool>> usageChecks = new List<Func<bool>>();
        public Action LockPlayer = null;
        public Action UnlockPlayer = null;

        protected override void Reset()
        {
            base.Reset();
            //Create and Set, ConditionCollectionsObj
            if (condCollectionsObj == null)
            {
                GameObject conditionCollectionsObj = new GameObject("Condition Collections Obj");

                conditionCollectionsObj.transform.SetParent(transform);
                conditionCollectionsObj.transform.localPosition = Vector3.zero;
                conditionCollectionsObj.transform.localRotation = Quaternion.identity;

                condCollectionsObj = conditionCollectionsObj.transform;
            }

            //Create and set default reaction collection
            if (defaultReactionCollection == null)
            {
                GameObject defaultReactionCollection = new GameObject("Default Reaction Collection");
                ReactionCollection drc = defaultReactionCollection.AddComponent<ReactionCollection>();
                defaultReactionCollection.transform.SetParent(transform);
                defaultReactionCollection.transform.localPosition = Vector3.zero;
                defaultReactionCollection.transform.localRotation = Quaternion.identity;

                this.defaultReactionCollection = drc;
            }
        }
        public void SubscribeToUsageChecks(Func<bool> check)
        {
            if (usageChecks.Contains(check) == false)
            {
                usageChecks.Add(check);
            }
        }
        public void UnSubscribeFromUsageChecks(Func<bool> check)
        {
            if (usageChecks.Contains(check))
            {
                usageChecks.Remove(check);
            }
        }
        private bool CanInteract()
        {
            List<bool> checkResults = new List<bool>();

            foreach (Func<bool> check in usageChecks)
            {
                checkResults.Add(check());
            }

            return checkResults.CheckOperationOnList(usageOperator);
        }
        public override IEnumerator Interact(MonoBehaviour caller)
        {
            if (caller == null)
            {
                caller = this;
            }
            if (CanInteract() == false)
            {
                yield break;
            }

            TryLockPlayer();

            //            bool didSwitch = false;
            //            if (blocksRayCast)
            //            {
            //                blocksRayCast = false;
            //                didSwitch = true;
            //            }

            conditionCollections = condCollectionsObj.GetComponents<BaseConditionCollection>();
            onInteractionStart?.CallEvent();

            foreach (BaseConditionCollection cc in conditionCollections)
            {
                if (cc.CollectionSatisfied())
                {
                    yield return caller.StartCoroutine(cc.reactionCollection.BeginReacting());
                    //                    if (didSwitch)
                    //                    {
                    //                        blocksRayCast = true;
                    //                    }
                    yield return 0;
                    TryUnLockPlayer();
                    yield break;
                }
            }

            yield return caller.StartCoroutine(defaultReactionCollection.BeginReacting());
            //            if (didSwitch)
            //            {
            //                blocksRayCast = true;
            //            }
            
            onInteractionEnd?.CallEvent();
            TryUnLockPlayer();
        }
        private void TryLockPlayer()
        {
            if (lockPlayerTillDone)
            {
                LockPlayer?.Invoke();
            }
        }
        private void TryUnLockPlayer()
        {
            if (lockPlayerTillDone)
            {
                UnlockPlayer?.Invoke();
            }
        }

    }
}