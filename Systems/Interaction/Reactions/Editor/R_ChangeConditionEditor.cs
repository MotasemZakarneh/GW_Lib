using GW_Lib;
using UnityEditor;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    [CustomEditor(typeof(R_ChangeCondition))]
    public class R_ChangeConditionEditor : Editor
    {
        R_ChangeCondition mySelf;
        const string timeBeforeStartName = "timeBeforeStart", reactionDurationName= "reactionDuration"
            , levelConditionsName="levelConditions";
        const string waitEndTypeName="waitEndType";
        const string instanceConditionsSourceName = "instanceConditionsSource";
        const string changeGlobalCondName = "changeGlobalCond";

        SerializedProperty waitEndTypeProp;
        SerializedProperty timeBeforeStartProp, reactionDurationProp;
        SerializedProperty changeGlobalCondProp;

        SerializedProperty levelConditionsProp;
        SerializedProperty instanceConditionsSourceProp;

        string[] chosenActCondsNames = new string[0];

        private void OnEnable()
        {
            mySelf = target as R_ChangeCondition;
            if (mySelf==null||serializedObject==null)
            {
                DestroyImmediate(this);
                return;
            }

            timeBeforeStartProp = serializedObject.FindProperty(timeBeforeStartName);
            reactionDurationProp = serializedObject.FindProperty(reactionDurationName);
            levelConditionsProp = serializedObject.FindProperty(levelConditionsName);

            instanceConditionsSourceProp = serializedObject.FindProperty(instanceConditionsSourceName);
            changeGlobalCondProp = serializedObject.FindProperty(changeGlobalCondName);

            waitEndTypeProp = serializedObject.FindProperty(waitEndTypeName);
            EditorUtility.SetDirty(mySelf);
            EditorUtility.SetDirty(this);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            BasicReactionGUI();

            if (CanShowCondsGUI())
            {
                ChooseConditionGUI();
                SetSatisfactionToGUI();
            }
            
            serializedObject.ApplyModifiedProperties();
        }

        private bool CanShowCondsGUI()
        {
            if (mySelf.changeGlobalCond)
            {
                if (mySelf.levelConditions== null)
                {
                    EditorGUILayout.HelpBox("Please Plug A LevelConditions Asset", MessageType.Warning);
                    return false;
                }
                else if (mySelf.levelConditions.conditions.Length == 0)
                {
                    EditorGUILayout.HelpBox("The conditions asset, does not contain any conditions"
                    , MessageType.Warning);
                    return false;
                }
            }
            else if (mySelf.changeGlobalCond==false)
            {
                if (mySelf.instanceConditionsSource == null)
                {
                    EditorGUILayout.HelpBox("Please Plug An InstanceConditionsSource", MessageType.Warning);
                    return false;
                }
                else if (mySelf.instanceConditionsSource.GetConditionsSource().conditions.Length == 0)
                {
                    EditorGUILayout.HelpBox("The Current InstanceConditionsSource Contains No Conditions", MessageType.Warning);
                    return false;
                }
            }

            return true;
        }
        private void FixUpCondition()
        {
            ConditionsSource conditionsSource = GetActiveConditionsSource();

            Condition condByDesc = conditionsSource.GetCondOfKey(mySelf.newInstanceCondState.description);
            Condition condByID = conditionsSource.GetCondOfId(mySelf.newInstanceCondState.iD);

            
            if (condByDesc != null && condByDesc==condByID)
            {
                return;
            }
            if (condByDesc != null && condByDesc!=condByID)
            {
                mySelf.newInstanceCondState = condByDesc;
            }
            else if (condByDesc== null && condByID!= null)
            {
                mySelf.newInstanceCondState = condByID;
            }
        }

        private void SetSatisfactionToGUI()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Set Satisfied To");
            mySelf.newInstanceCondState.satisfied = EditorGUILayout.Toggle(mySelf.newInstanceCondState.satisfied);
            EditorGUILayout.EndHorizontal();
        }

        private void ChooseConditionGUI()
        {
            chosenActCondsNames = GetActiveConditionsSource().GetNamesOfCondsInSource();
            EditorGUI.BeginChangeCheck();
            FixUpCondition();

            int nameSelector = GetNameIndexFromInstanceCond();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Pick Condition");
            nameSelector = EditorGUILayout.Popup(nameSelector, chosenActCondsNames);
            EditorGUILayout.EndHorizontal();
            if (EditorGUI.EndChangeCheck())
            {
                ConditionsSource activeSource = GetActiveConditionsSource();

                Condition targetCond = activeSource.GetCondOfKey(chosenActCondsNames[nameSelector]);
                
                mySelf.newInstanceCondState.iD = targetCond.iD;
                mySelf.newInstanceCondState.description = targetCond.description;
                mySelf.newInstanceCondState.satisfied = false;
            }
        }

        private int GetNameIndexFromInstanceCond()
        {
            string desc = mySelf.newInstanceCondState.description;
            int targetHash = Animator.StringToHash(desc);
            for (int i = 0; i < chosenActCondsNames.Length; i++)
            {
                string possibleMatch = chosenActCondsNames[i];
                int possibleHash = Animator.StringToHash(possibleMatch);
                if (possibleHash == targetHash)
                {
                    return i;
                }
            }
            return -1; 
        }

        private void BasicReactionGUI()
        {
            EditorExtentions.DisplayMonoScript(mySelf);

            EditorGUILayout.PropertyField(timeBeforeStartProp);
            EditorGUILayout.PropertyField(reactionDurationProp);
            EditorGUILayout.PropertyField(waitEndTypeProp);

            EditorGUILayout.PropertyField(changeGlobalCondProp);
            
            if (mySelf.changeGlobalCond)
            {
                EditorGUILayout.PropertyField(levelConditionsProp);
            }
            else
            {
                EditorGUILayout.PropertyField(instanceConditionsSourceProp);
            }
        }
        //Helper Function, Was Moved From R_ChangeCondition
        public ConditionsSource GetActiveConditionsSource()
        {
            ConditionsSource conditionsSource;
            if (mySelf.changeGlobalCond)
            {
                conditionsSource = mySelf.levelConditions.GetConditionsSource();
            }
            else
            {
                conditionsSource = mySelf.instanceConditionsSource.GetConditionsSource();
            }
            return conditionsSource;
        }

    }
}