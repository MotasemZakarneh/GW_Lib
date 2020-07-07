using UnityEditor;

namespace GW_Lib.Interaction_System
{
    [CustomEditor(typeof(GlobalConditionCollection))]
    public class ConditionCollectionEditor : BaseConditionCollectionEditor
    {
        SerializedProperty levelConditionsProp;
        const string levelConditionsName = "levelConditions";

        GlobalConditionCollection mySelf;

        protected override void OnEnable()
        {
            base.OnEnable();

            mySelf = target as GlobalConditionCollection;
            if (mySelf==null||serializedObject==null)
            {
                DestroyImmediate(this);
                return;
            }
            levelConditionsProp = serializedObject.FindProperty(levelConditionsName);
        }
        protected override bool CanDisplayCondsAndAdditionGUI()
        {
            bool noLevelConditions = mySelf.levelConditions == null;
            if (noLevelConditions)
            {
                EditorGUILayout.HelpBox("No Level Conditions Are Present Please Plug Level Conditions", MessageType.Warning);
                return false;
            }
            else if (noLevelConditions && mySelf.levelConditions.conditions.Length == 0)
            {
                EditorGUILayout.HelpBox("The conditions asset, does not contain any conditions"
                , MessageType.Warning);
                return false;
            }
            return true;
        }
        protected override void SpecialGUI()
        {
            EditorGUILayout.PropertyField(levelConditionsProp);
            EditorGUI.BeginChangeCheck();

        }
        protected override string[] GetAllCondsNames()
        {
            string[] allCondsNames = mySelf.levelConditions.GetConditionsSource().GetNamesOfCondsInSource();

            return allCondsNames;
        }
        protected override Condition GetCondFromSource(int byID)
        {
            GlobalLevelConditions targetAct = mySelf.levelConditions;
            Condition condToAdd = targetAct.GetConditionsSource().GetCondOfId(byID);
            return condToAdd;
        }
        protected override Condition GetCondFromSource(string byName)
        {
            GlobalLevelConditions targetAct = mySelf.levelConditions;
            Condition condToAdd = targetAct.GetConditionsSource().GetCondOfKey(byName);
            return condToAdd;
        }
    }
}