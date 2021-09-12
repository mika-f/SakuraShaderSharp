using UnityEditor;

using UnityEngine;

namespace NatsunekoLaboratory.SakuraShader
{
    internal class Vector2Drawer : MaterialPropertyDrawer
    {
        public override void OnGUI(Rect position, MaterialProperty prop, GUIContent label, MaterialEditor me)
        {
            if (prop.type == MaterialProperty.PropType.Vector)
            {
                EditorGUI.BeginChangeCheck();
                var vec2 = EditorGUI.Vector2Field(position, label, prop.vectorValue);
                if (EditorGUI.EndChangeCheck())
                    prop.vectorValue = new Vector4(vec2.x, vec2.y, 0, 0);
            }
            else
            {
                me.ShaderProperty(prop, label);
            }
        }
    }
}