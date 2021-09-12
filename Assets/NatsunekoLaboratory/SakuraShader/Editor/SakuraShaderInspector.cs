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
        private int[] _foldoutStatuses;
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

        protected void OnInitializeFoldout(params MaterialProperty[] foldout)
        {
            _foldoutStatuses = foldout.Select(w => (int)w.floatValue).ToArray();
        }

        protected void OnStoreFoldout(params MaterialProperty[] foldout)
        {
            if (foldout.Length != _foldoutStatuses.Length)
                return;

            for (var i = 0; i < _foldoutStatuses.Length; i++)
                foldout[i].floatValue = _foldoutStatuses[i];
        }

        protected void OnOthersGui(MaterialEditor me, MaterialProperty culling, MaterialProperty zw, MaterialProperty zt = null)
        {
            OnFoldOutGui(Category.Others, () =>
            {
                if (culling != null)
                    me.ShaderProperty(culling, "Culling");
                if (zw != null)
                    me.ShaderProperty(zw, "ZWrite");
                if (zt != null)
                    me.ShaderProperty(zt, "ZTest");

                me.RenderQueueField();
                me.DoubleSidedGIField();
            });
        }

        protected void OnFoldOutGui<T>(T category, Action callback) where T : Enum
        {
            var title = typeof(T).GetMember(category.ToString()).Select(w => w.GetCustomAttribute<EnumMemberAttribute>(false)?.Value).FirstOrDefault() ?? category.ToString();
            var style = new GUIStyle("ShurikenModuleTitle")
            {
                border = new RectOffset(15, 7, 4, 4),
                fontSize = 12,
                fixedHeight = 24,
                contentOffset = new Vector2(20, -2)
            };

            var rect = GUILayoutUtility.GetRect(16.0f, 22.0f, GUIStyle.none);
            GUI.Box(rect, title, style);

            var e = Event.current;
            var foldoutState = GetFoldState((int)(object)category);
            var foldoutRect = new Rect(rect.x + 4, rect.y + 2, 16, 16);
            if (e.type == EventType.Repaint)
                EditorStyles.foldout.Draw(foldoutRect, false, false, foldoutState, false);

            if (e.type == EventType.MouseDown)
            {
                var foldoutArea = new Rect(0, rect.y + 2f, rect.width, 16);
                if (foldoutArea.Contains(e.mousePosition))
                {
                    SetFoldState((int)(object)category, !foldoutState);
                    e.Use();
                }
            }

            if (GetFoldState((int)(object)category))
            {
                using (new EditorGUI.IndentLevelScope())
                    callback.Invoke();

                EditorGUILayout.Space();
            }
        }

        protected void OnFoldoutAndToggleGui<T>(T category, MaterialProperty toggleProperty, Action callback) where T : Enum
        {
            var title = typeof(T).GetMember(category.ToString()).Select(w => w.GetCustomAttribute<EnumMemberAttribute>(false)?.Value).FirstOrDefault() ?? category.ToString();
            var style = new GUIStyle("ShurikenModuleTitle")
            {
                border = new RectOffset(15, 7, 4, 4),
                fontSize = 12,
                fixedHeight = 24,
                contentOffset = new Vector2(20, -2)
            };

            var rect = GUILayoutUtility.GetRect(16.0f, 22.0f, GUIStyle.none);
            GUI.Box(rect, title, style);

            var e = Event.current;
            var foldoutState = GetFoldState((int)(object)category);
            var foldoutRect = new Rect(rect.x + 4, rect.y + 2, 16, 16);
            if (e.type == EventType.Repaint)
                EditorStyles.foldout.Draw(foldoutRect, false, false, foldoutState, false);

            var toggleState = IsEqualsTo(toggleProperty, true);
            var toggleRect = new Rect(rect.width, rect.y + 2, 16, 16);
            if (e.type == EventType.Repaint)
                EditorStyles.toggle.Draw(toggleRect, false, false, toggleState, false);

            if (e.type == EventType.MouseDown)
            {
                var foldoutArea = new Rect(0, rect.y + 2f, rect.width, 16);
                if (foldoutArea.Contains(e.mousePosition))
                {
                    SetFoldState((int)(object)category, !foldoutState);
                    e.Use();
                }

                if (toggleRect.Contains(e.mousePosition))
                {
                    toggleProperty.floatValue = toggleState ? 0.0f : 1.0f;
                    e.Use();
                }
            }

            if (GetFoldState((int)(object)category))
            {
                using (new EditorGUI.DisabledGroupScope(IsEqualsTo(toggleProperty, false)))
                using (new EditorGUI.IndentLevelScope())
                    callback.Invoke();

                EditorGUILayout.Space();
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

        protected static bool DisabledWhen(MaterialProperty a, bool b)
        {
            return !IsEqualsTo(a, b);
        }

        protected static bool IsEqualsTo<T>(MaterialProperty a, T b) where T : Enum
        {
            return IsEqualsTo(a, (int)(object)b);
        }

        protected static void DisabledWhen<T>(MaterialProperty a, T b, Action callback) where T : Enum
        {
            using (new EditorGUI.DisabledGroupScope(IsEqualsTo(a, b)))
                callback.Invoke();

        }

        protected static void EnabledWhen(MaterialProperty a, bool b, Action callback)
        {
            using (new EditorGUI.DisabledGroupScope(!IsEqualsTo(a, b)))
                callback.Invoke();
        }

        protected static void EnabledWhen<T>(MaterialProperty a, T b, Action callback) where T : Enum
        {
            using (new EditorGUI.DisabledGroupScope(!IsEqualsTo(a, b)))
                callback.Invoke();
        }

        protected new static MaterialProperty FindProperty(string name, MaterialProperty[] properties)
        {
            return FindProperty(name, properties, false);
        }

        protected bool GetFoldState(int category)
        {
            var index = category / 32;
            return (_foldoutStatuses[index] & (1 << category)) > 0;
        }

        protected void SetFoldState(int category, bool value)
        {
            var index = category / 32;
            if (value)
                _foldoutStatuses[index] |= 1 << category;
            else
                _foldoutStatuses[index] &= ~(1 << category);
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

        private enum Category
        {
            Others = 0
        }
    }
}