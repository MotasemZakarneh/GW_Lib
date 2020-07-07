using GW_Lib;
using UnityEditor;

namespace GW_Lib.Interaction_System
{
    [CustomEditor(typeof(GlobalLevelConditions))]
    public class GlobalLevelConditionsEditor : BaseConditionsSourceEditor
    {
        GlobalLevelConditions globalLevelConditions;
        
        protected override void OnEnable()
        {
            base.OnEnable();

            globalLevelConditions = target as GlobalLevelConditions;
            if (globalLevelConditions==null||serializedObject==null)
            {
                DestroyImmediate(this);
                return;
            }
        }

        protected override void BaseGUI()
        {
            EditorExtentions.DisplayScriptableScript(globalLevelConditions);
        }
    }
}