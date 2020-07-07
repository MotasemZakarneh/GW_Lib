using GW_Lib;
using UnityEditor;

namespace GW_Lib.Interaction_System
{
    [CustomEditor(typeof(InstanceConditionsSource))]
    public class InstanceConditionsSourceEditor : BaseConditionsSourceEditor
    {
        InstanceConditionsSource instanceCondsSource;
        protected override void OnEnable()
        {
            base.OnEnable();

            instanceCondsSource = target as InstanceConditionsSource;
            if (instanceCondsSource==null||serializedObject==null)
            {
                DestroyImmediate(this);
                return;
            }
        }
        protected override void BaseGUI()
        {
            EditorExtentions.DisplayMonoScript(instanceCondsSource);
        }
    }
}