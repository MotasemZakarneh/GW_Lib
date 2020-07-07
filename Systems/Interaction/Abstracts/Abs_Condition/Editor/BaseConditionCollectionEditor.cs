using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using StealthGame;
using GW_Lib;
using GW_Lib.Utility;

namespace GW_Lib.Interaction_System
{
    [CustomEditor(typeof(BaseConditionCollection))]
    public abstract class BaseConditionCollectionEditor : Editor
    {
        int condToAddIndex;

        const string decisionOperatorName= "testOperator";
        SerializedProperty decisionOperator;

        SerializedProperty reactionCollectionProp;
        const string reactionCollectionName = "reactionCollection";

        BaseConditionCollection baseMySelf;
        protected abstract string[] GetAllCondsNames();
        protected abstract Condition GetCondFromSource(int byID);
        protected abstract Condition GetCondFromSource(string byName);
        protected abstract bool CanDisplayCondsAndAdditionGUI();
        protected abstract void SpecialGUI();

        protected virtual void OnEnable()
        {
            baseMySelf = target as BaseConditionCollection;
            if (baseMySelf==null||serializedObject==null)
            {
                DestroyImmediate(this);
                return;
            }
            reactionCollectionProp = serializedObject.FindProperty(reactionCollectionName);
            decisionOperator = serializedObject.FindProperty(decisionOperatorName);
            EditorUtility.SetDirty(baseMySelf);
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            BaseGUI();
            EditorGUILayout.Space();
            if (CanDisplayCondsAndAdditionGUI())
            {
                reactionCollectionProp.isExpanded = EditorGUILayout.Foldout(reactionCollectionProp.isExpanded, "Show Conditions GUIS");
                if (reactionCollectionProp.isExpanded)
                {
                    EditorGUI.indentLevel++;
                    DisplayCurrentConditionsGUI();

                    CondAdditionGUI(GetAllCondsNames());
                    EditorGUI.indentLevel--;
                }
            }

            SpecialGUI();
            
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(reactionCollectionProp);

            serializedObject.ApplyModifiedProperties();
        }
   
        private void BaseGUI()
        {
            EditorExtentions.DisplayMonoScript(baseMySelf);
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Description");
            baseMySelf.description = EditorGUILayout.TextArea(baseMySelf.description);
            EditorGUILayout.EndHorizontal();
            if (baseMySelf.description == null || baseMySelf.description.Trim() == "")
            {
                baseMySelf.description = "Collection Description";
            }
            EditorGUILayout.PropertyField(decisionOperator);
        }

        private void CondAdditionGUI(string[] allCondsNames)
        {
            EditorGUILayout.BeginHorizontal();
            condToAddIndex = EditorGUILayout.Popup(condToAddIndex, allCondsNames);
            bool added = GUILayout.Button("+");
            EditorGUILayout.EndHorizontal();
            if (added)
            {
                baseMySelf.instanceConditions = baseMySelf.instanceConditions.ReSizeArray(baseMySelf.instanceConditions.Length + 1);
                int newlyBornCondId = baseMySelf.instanceConditions.Length - 1;

                string condName = allCondsNames[condToAddIndex];
                Condition condToAdd = GetCondFromSource(condName);
                if (condToAdd == null)
                {
                    condToAdd = GetCondFromSource(condToAddIndex);
                }

                baseMySelf.instanceConditions[newlyBornCondId] = condToAdd.Clone();
            }
        }
        private void CheckAndDeleteWrongConds()
        {
            List<Condition> condsToRemove = new List<Condition>();

            for (int i = 0; i < baseMySelf.instanceConditions.Length; i++)
            {
                Condition instanceCond = baseMySelf.instanceConditions[i];
                Condition c = GetCondFromSource(instanceCond.iD);
                Condition sourceCondition = c;

                if (sourceCondition == null)
                {
                    condsToRemove.Add(instanceCond);
                }
                else if (instanceCond.Hash != sourceCondition.Hash)
                {
                    instanceCond.description = sourceCondition.description;
                }
            }

            List<Condition> aviliableConds = baseMySelf.instanceConditions.ToList();
            foreach (Condition condToRemove in condsToRemove)
            {
                if (aviliableConds.Contains(condToRemove))
                {
                    Debug.Log(condToRemove.ToString());
                    aviliableConds.Remove(condToRemove);
                }
            }
            baseMySelf.instanceConditions = aviliableConds.ToArray();
            if (condsToRemove.Count != 0)
            {
                Debug.Log("Removed A total Of " + condsToRemove.Count + " Conditions From " + baseMySelf.name);
            }
        }
        private void DisplayCurrentConditionsGUI()
        {
            CheckAndDeleteWrongConds();

            GUILayoutOption o = GUILayout.Width(EditorExtentions.GetCurrViewWidth(3));
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Description", o);
            EditorGUILayout.LabelField("Test With State", o);
            EditorGUILayout.LabelField("Remove", o);
            EditorGUILayout.EndHorizontal();
            for (int i = 0; i < baseMySelf.instanceConditions.Length; i++)
            {
                o = GUILayout.Width(EditorExtentions.GetCurrViewWidth(3f));
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(baseMySelf.instanceConditions[i].description, o);
                baseMySelf.instanceConditions[i].satisfied =
                    EditorGUILayout.Toggle(baseMySelf.instanceConditions[i].satisfied, o);

                if (GUILayout.Button("-"))
                {
                    List<Condition> conds = baseMySelf.instanceConditions.ToList();
                    conds.Remove(baseMySelf.instanceConditions[i]);
                    baseMySelf.instanceConditions = conds.ToArray();
                }

                EditorGUILayout.EndHorizontal();
            }
        }

    }
}