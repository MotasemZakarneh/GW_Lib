using System.Collections.Generic;
using System.Linq;
using GW_Lib;
using UnityEditor;
using UnityEngine;

namespace GW_Lib.Interaction_System
{
    [CustomEditor(typeof(ConditionsSource))]
    public abstract class BaseConditionsSourceEditor : Editor
    {
        string nameOfCondToAdd;
        IConditionsSource iMySelf;

        const string conditionsName = "conditions";
        const string conditionsSourceName = "conditionsSource";
        SerializedProperty conditionsSourceProp;
        SerializedProperty conditionsProp;
        protected abstract void BaseGUI();

        protected virtual void OnEnable()
        {
            iMySelf = target as IConditionsSource;
            if (iMySelf==null||serializedObject==null)
            {
                DestroyImmediate(this);
                return;
            }
            EditorUtility.SetDirty(iMySelf.GetHolderObject());
            EditorUtility.SetDirty(this);

            
            conditionsSourceProp = serializedObject.FindProperty(conditionsSourceName);
            
            conditionsProp = conditionsSourceProp.FindPropertyRelative(conditionsName);
            nameOfCondToAdd = "New Condition " + conditionsProp.arraySize;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            BaseGUI();
            conditionsProp.isExpanded = EditorGUILayout.Foldout(conditionsProp.isExpanded, "Conditions");
            if (conditionsProp.isExpanded)
            {
                DisplayConditionsGUI();
                AdditionGUI();
            }
            serializedObject.ApplyModifiedProperties();
        }

        private void AdditionGUI()
        {
            EditorGUILayout.BeginHorizontal();
            nameOfCondToAdd = EditorGUILayout.TextField(nameOfCondToAdd);
            bool added = GUILayout.Button("Add Condition");
            EditorGUILayout.EndHorizontal();
            if (added)
            {
                ConditionsSource conditionsSource = iMySelf.GetConditionsSource();

                int condID = GetID(conditionsSource.conditions.Length);

                Condition condToAdd = new Condition(condID, nameOfCondToAdd, false);
                iMySelf.GetConditionsSource().conditions = conditionsSource.conditions.ReSizeArray(conditionsSource.conditions.Length + 1);
                conditionsSource.conditions[conditionsSource.conditions.Length - 1] = condToAdd;

                conditionsProp = conditionsSourceProp.FindPropertyRelative(conditionsName);
                nameOfCondToAdd = "New Condition " + conditionsSource.conditions.Length;

                conditionsSource.initialStates.Insert(conditionsSource.conditions.Length - 1, false);
            }
        }

        private int GetID(int length)
        {
            int condID = length;
            for (int i = 0; i < length; i++)
            {
                Condition testCond = iMySelf.GetConditionsSource().GetCondOfId(i);
                if (testCond==null)
                {
                    condID = i;
                    break;
                }
            }
            return condID;
        }

        private void DisplayConditionsGUI()
        {
            GUILayoutOption o = GUILayout.Width(EditorExtentions.GetCurrViewWidth(2));
            float space = EditorExtentions.GetCurrViewWidth(4);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Description", o);
            o = GUILayout.Width(EditorExtentions.GetCurrViewWidth(8));
            EditorGUILayout.LabelField("ID", o);
            EditorGUILayout.LabelField("Curr", o);
            EditorGUILayout.LabelField("ReSet", o);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            ConditionsSource conditionsSource = iMySelf.GetConditionsSource();
            for (int i = 0; i < conditionsSource.conditions.Length; i++)
            {
                EditorGUILayout.BeginVertical(GUI.skin.box);

                Condition cond = conditionsSource.conditions[i];
                EditorGUILayout.BeginHorizontal();
                o = GUILayout.Width(EditorExtentions.GetCurrViewWidth(2));
                cond.description = EditorGUILayout.TextField(cond.description, o);
                o = GUILayout.Width(EditorExtentions.GetCurrViewWidth(8));
                EditorGUILayout.LabelField(cond.iD.ToString(), o);
                cond.satisfied = EditorGUILayout.Toggle(cond.satisfied, o);
                conditionsSource.initialStates[i] = EditorGUILayout.Toggle(conditionsSource.initialStates[i], o);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                space = EditorExtentions.GetCurrViewWidth(4);
                GUILayout.Space(space * 1.5f);
                o = GUILayout.Width(EditorExtentions.GetCurrViewWidth(10));
                if (GUILayout.Button("-", o))
                {
                    List<Condition> condsL = conditionsSource.conditions.ToList();
                    int index = condsL.IndexOf(cond);
                    condsL.Remove(cond);
                    iMySelf.GetConditionsSource().conditions = condsL.ToArray();
                    conditionsSource.initialStates.Remove(conditionsSource.initialStates[index]);
                    break;
                }
                o = GUILayout.Width(EditorExtentions.GetCurrViewWidth(20));
                bool up = GUILayout.Button(Constants.UpArrow_KEY, o);
                bool down = GUILayout.Button(Constants.DownArrow_KEY, o);
                if (up)
                {
                    iMySelf.GetConditionsSource().conditions = conditionsSource.conditions.SwapInArray(i, i - 1);
                }
                else if (down)
                {
                    iMySelf.GetConditionsSource().conditions = conditionsSource.conditions.SwapInArray(i, i + 1);
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
            }
        }


    }
}