using UnityEditor;

namespace GW_Lib.Interaction_System
{
    [CustomEditor(typeof(InstanceConditionCollection))]
    public class InstanceConditionCollectionEditor : BaseConditionCollectionEditor
    {
        const string instanceConditionsSourceName = "instanceConditionsSource";
        SerializedProperty instanceConditionsSource;
        InstanceConditionCollection mySelf;
        protected override void OnEnable()
        {
            base.OnEnable();
            mySelf = target as InstanceConditionCollection;
            if (mySelf==null||serializedObject==null)
            {
                DestroyImmediate(this);
                return;
            }
            EditorUtility.SetDirty(mySelf);
            EditorUtility.SetDirty(this);
            instanceConditionsSource = serializedObject.FindProperty(instanceConditionsSourceName);
        }
        protected override bool CanDisplayCondsAndAdditionGUI()
        {
            if (mySelf.InstanceConditionsSource == null)
            {
                EditorGUILayout.HelpBox("Please Plug A InstanceConditionsSource", MessageType.Warning);
                return false;
            }
            else if (mySelf.InstanceConditionsSource.GetConditionsSource()==null)
            {
                EditorGUILayout.HelpBox("The plugged InstanceConditionsSource \n"
                + "doesn't have A Conditions Source", MessageType.Warning);
                return false;
            }
            else if (mySelf.InstanceConditionsSource.GetConditionsSource().conditions == null)
            {
                EditorGUILayout.HelpBox("The plugged InstanceConditionsSource \n"
                + "Doesnt Have Conditions", MessageType.Warning);
                return false;
            }
            else if (mySelf.InstanceConditionsSource.GetConditionsSource().conditions.Length == 0)
            {
                EditorGUILayout.HelpBox("The plugged InstanceConditionsSource have conditions\n" +
                    "of zero length"
                , MessageType.Warning);
                return false;
            }
            return true;
        }
        protected override string[] GetAllCondsNames()
        {
            return mySelf.InstanceConditionsSource.GetConditionsSource().GetNamesOfCondsInSource();
        }
        protected override Condition GetCondFromSource(int byID)
        {
            return mySelf.InstanceConditionsSource.GetConditionsSource().GetCondOfId(byID);
        }
        protected override Condition GetCondFromSource(string byName)
        {
            return mySelf.InstanceConditionsSource.GetConditionsSource().GetCondOfKey(byName);
        }
        protected override void SpecialGUI()
        {
            EditorGUILayout.PropertyField(instanceConditionsSource);
        }
    }
}