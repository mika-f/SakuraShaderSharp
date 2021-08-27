using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

using UnityEditor;

using UnityEngine;

namespace NatsunekoLaboratory.SakuraShader
{
    public class SakuraShaderInspector : ShaderGUI
    {
        private readonly List<FieldInfo> _properties;
        private int _foldout;
        private bool _isInitialized;

        protected SakuraShaderInspector()
        {
            _isInitialized = false;
            _properties = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic).Where(w => w.FieldType == typeof(MaterialProperty)).ToList();
        }

        protected void InjectMaterialProperties(MaterialProperty[] properties)
        {
            foreach (var property in _properties)
            {
                var value = FindProperty(property.Name, properties);
                property.SetValue(this, value);
            }
        }

        protected void OnHeaderGui(string title)
        {
            EditorStyles.label.wordWrap = true;

            using (new Section(title))
                EditorGUILayout.LabelField($"{title} - Part of Sakura Shader by Natsuneko");

            EditorStyles.label.wordWrap = false;
        }

        protected void OnInitialize(Material material)
        {
            if (_isInitialized)
                return;
            _isInitialized = true;

            foreach (var keyword in material.shaderKeywords)
                material.DisableKeyword(keyword);
        }

        protected void OnInitializeFoldout(MaterialProperty foldout)
        {
            _foldout = (int) foldout.floatValue;
        }

        protected void OnStoreFoldout(MaterialProperty foldout)
        {
            foldout.floatValue = _foldout;
        }

        protected void OnOthersGui(MaterialEditor me, MaterialProperty culling, MaterialProperty zw)
        {
            using (new Section("Others"))
            {
                if (culling != null)
                    me.ShaderProperty(culling, "Culling");
                if (zw != null)
                    me.ShaderProperty(zw, "ZWrite");

                me.RenderQueueField();
                me.DoubleSidedGIField();
            }
        }

        protected void OnToggleGui<T>(MaterialEditor me, T category, MaterialProperty toggleProperty, Action callback) where T : Enum
        {
            var title = typeof(T).GetMember(category.ToString()).Select(w => w.GetCustomAttribute<EnumMemberAttribute>(false)?.Value).FirstOrDefault() ?? category.ToString();
            using (new Section(title))
            {
                me.ShaderProperty(toggleProperty, $"Enable {title}");

                using (var foldout = new Foldout("Parameters", ref _foldout, (int)(object)category))
                {
                    if (foldout.IsDisplayed)
                        using (new EditorGUI.DisabledGroupScope(IsEqualsTo(toggleProperty, false)))
                        using (new EditorGUI.IndentLevelScope())
                            callback.Invoke();
                }
            }
        }

        protected void OnFoldoutGui<T>(MaterialEditor me, T category, Action callback) where T : Enum
        {
            using (var foldout = new Foldout(category.ToString(), ref _foldout, (int)(object)category))
            {
                if (foldout.IsDisplayed)
                    using (new EditorGUI.IndentLevelScope())
                        callback.Invoke();
            }
        }

        protected static bool IsEqualsTo(MaterialProperty a, int b)
        {
            return b - 0.5 < a.floatValue && a.floatValue <= b + 0.5;
        }

        protected static bool IsEqualsTo(MaterialProperty a, bool b)
        {
            return IsEqualsTo(a, b ? 1 : 0);
        }

        protected new static MaterialProperty FindProperty(string name, MaterialProperty[] properties)
        {
            return FindProperty(name, properties, false);
        }

        protected class Foldout : IDisposable
        {
            public bool IsDisplayed { get; }

            public Foldout(string title, ref int foldout, int category)
            {
                EditorGUI.indentLevel++;

                var isActive = EditorGUILayout.Foldout(GetHoldState(foldout, category), new GUIContent(title));

                IsDisplayed = isActive;
                if (isActive)
                    foldout |= 1 << category;
                else
                    foldout &= ~(1 << category);
            }

            public void Dispose()
            {
                EditorGUI.indentLevel--;
            }

            private bool GetHoldState(int foldout, int category)
            {
                return (foldout & (1 << category)) > 0;
            }
        }

        protected class Section : IDisposable
        {
            private readonly IDisposable _disposable;

            public Section(string title)
            {
                GUILayout.Label(title, EditorStyles.boldLabel);
                _disposable = new EditorGUILayout.VerticalScope(GUI.skin.box);
            }

            public void Dispose()
            {
                _disposable.Dispose();
            }
        }
    }
}