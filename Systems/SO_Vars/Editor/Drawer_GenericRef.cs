using UnityEditor;
using UnityEngine;
namespace GW_Lib.Utility
{
    [CustomPropertyDrawer(typeof(Ref_bool))]
    public class Ref_BoolDrawer : Drawer_GenericRef
    {
        public override SerializedProperty VariableProp(SerializedProperty refProp)
        {
            return refProp.FindPropertyRelative("var_Bool");
        }
    }
    [CustomPropertyDrawer(typeof(Ref_int))]
    public class Ref_IntDrawer : Drawer_GenericRef
    {
        public override SerializedProperty VariableProp(SerializedProperty refProp)
        {
            return refProp.FindPropertyRelative("var_Int");
        }
    }
    [CustomPropertyDrawer(typeof(Ref_string))]
    public class Ref_StringDrawer : Drawer_GenericRef
    {
        public override SerializedProperty VariableProp(SerializedProperty refProp)
        {
            return refProp.FindPropertyRelative("var_String");
        }
    }
    [CustomPropertyDrawer(typeof(Ref_float))]
    public class Ref_FloatDrawer : Drawer_GenericRef
    {
        public override SerializedProperty VariableProp(SerializedProperty refProp)
        {
            return refProp.FindPropertyRelative("var_Float");
        }
    }
    [CustomPropertyDrawer(typeof(Ref_LayerMask))]
    public class Ref_LayerMaskDrawer : Drawer_GenericRef
    {
        public override SerializedProperty VariableProp(SerializedProperty refProp)
        {
            return refProp.FindPropertyRelative("var_LayerMask");
        }
    }
    public abstract class Drawer_GenericRef : PropertyDrawer
    {
        readonly string[] popUpOptions = { "Use Reference", "Use Variable" };
        GUIStyle popUpStyle;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (popUpStyle == null)
            {
                popUpStyle = GUI.skin.GetStyle("PaneOptions");
                popUpStyle.imagePosition = ImagePosition.ImageOnly;
            }
            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);

            SerializedProperty useVariableProp = property.FindPropertyRelative("useVariable");
            SerializedProperty referenceValueProp = property.FindPropertyRelative("referenceValue");
            SerializedProperty variableProp = VariableProp(property);


            EditorGUI.BeginChangeCheck();

            Rect popUpRect = new Rect(position);
            popUpRect.yMin += popUpStyle.margin.top;
            popUpRect.width = popUpStyle.fixedWidth + popUpStyle.margin.right;
            position.xMin = popUpRect.xMax;

            int mode = EditorGUI.Popup(popUpRect, useVariableProp.boolValue ? 1 : 0, popUpOptions,popUpStyle);
            useVariableProp.boolValue = mode == 1;

            if (useVariableProp.boolValue)
            {
                VariableDrawing(position,variableProp);
            }
            else
            {
                ReferenceDrawing(position,referenceValueProp);
            }

            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();
            }
            EditorGUI.EndProperty();
        }
        protected virtual void ReferenceDrawing(Rect position,SerializedProperty referenceProp)
        {
            EditorGUI.PropertyField(position,referenceProp,GUIContent.none,true);
        }
        protected virtual void VariableDrawing(Rect position,SerializedProperty varProp)
        {
            EditorGUI.PropertyField(position,varProp,GUIContent.none,true);
        }

        public abstract SerializedProperty VariableProp(SerializedProperty refProp);
    }
}