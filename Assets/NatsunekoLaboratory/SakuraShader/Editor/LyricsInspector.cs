using System.IO;
using System.Runtime.Serialization;

using UnityEditor;

using UnityEngine;

// ReSharper disable AssignNullToNotNullAttribute

#pragma warning disable 649

namespace NatsunekoLaboratory.SakuraShader
{
    public class LyricsInspector : SakuraShaderInspector
    {
        private const string LyricsShaderGuid = "ed5660f54b5c8fc4da35edf4b0049331";
        private const string ShaderFullPlaceholder = "NatsunekoLaboratory/Sakura Shader";
        private const string ShaderPlaceholder = "Lyrics";
        private const string PassPlaceholder = "SakuraShader_Lyrics_WorldGrab";
        private string _pass = PassPlaceholder;
        private string _name = ShaderPlaceholder;

        public override void OnGUI(MaterialEditor me, MaterialProperty[] properties)
        {
            var material = (Material)me.target;

            InjectMaterialProperties(properties);

            OnHeaderGui("Lyrics Shader");
            OnInitialize(material);
            OnInitializeFoldout(FoldoutStatus1, FoldoutStatus2);

            OnMainColor(me);
            OnOutlineGui(me);
            OnInverseColorGui(me);
            OnStencilGui(me);
            OnOthersGui(me, Culling, _ZWrite, _ZTest);
            OnAdvancedGui(me);
            OnStoreFoldout(FoldoutStatus1, FoldoutStatus2);

            var isEnableGrabFeature = IsEqualsTo(IsEnableOutline, true) || IsEqualsTo(IsEnableInverseColor, true);

            // NOTE: SetShaderPassEnabled is based on the LightMode, but the LightMode only recognizes some of the lights, which changes the way the light is handled, resulting in unnatural rendering. Therefore, we intentionally use the existing LightMode.
            material.SetShaderPassEnabled("Always", true);
            material.SetShaderPassEnabled("ForwardBase", isEnableGrabFeature);
        }

        private void OnMainColor(MaterialEditor me)
        {
            OnFoldOutGui(Category.Color, () =>
            {
                me.TextureProperty(MainTexture, "Texture");
                me.ShaderProperty(MainColor, "Color");
                me.ShaderProperty(AlphaTransparency, "Alpha Transparency");
            });
        }

        private void OnOutlineGui(MaterialEditor me)
        {
            OnFoldoutAndToggleGui(Category.Outline, IsEnableOutline, () =>
            {
                me.ShaderProperty(OutlineClearColor, "Clear Color");
                me.ShaderProperty(OutlineColor, "Color");
                me.ShaderProperty(OutlineWidth, "Width");
                me.ShaderProperty(IsOutlineRenderEdgeOnly, "Render Edge Only");
            });
        }

        private void OnInverseColorGui(MaterialEditor me)
        {
            OnFoldoutAndToggleGui(Category.Inverse, IsEnableInverseColor, () =>
            {
                //
                me.ShaderProperty(InverseWeight, "Weight");
            });
        }

        private void OnStencilGui(MaterialEditor me)
        {
            OnFoldOutGui(Category.Stencil, () =>
            {
                me.ShaderProperty(StencilRef, "Reference");
                me.ShaderProperty(StencilComp, "Compare Function");
                me.ShaderProperty(StencilPass, "Pass");
                me.ShaderProperty(StencilFail, "Fail");
                me.ShaderProperty(StencilZFail, "ZFail");
                me.ShaderProperty(StencilReadMask, "Read Mask");
                me.ShaderProperty(StencilWriteMask, "Write Mask");
            });
        }

        private void OnAdvancedGui(MaterialEditor me)
        {
            OnFoldOutGui(Category.Advanced, () =>
            {
                EditorGUILayout.LabelField("Duplicate Lyrics Shader", EditorStyles.boldLabel);
                EditorGUILayout.LabelField("Because the Lyrics Shader uses a named GrabPass, it is not possible to do multiple overlays. Therefore, we will create a new shader to simulate multiple overlays.");

                _name = EditorGUILayout.TextField("Shader Name", _name);
                _pass = EditorGUILayout.TextField("Grab Name (Unique)", _pass);

                using (new EditorGUI.DisabledGroupScope(string.IsNullOrWhiteSpace(_name) || _name == ShaderPlaceholder || string.IsNullOrWhiteSpace(_pass) || _pass == PassPlaceholder))
                    if (GUILayout.Button("Duplicate Lyrics Shader"))
                        OnDuplicate();
            });
        }

        private void OnDuplicate()
        {
            var root = Path.GetFullPath(Path.Combine(Application.dataPath, "..", AssetDatabase.GUIDToAssetPath(LyricsShaderGuid)));
            var parent = Path.GetDirectoryName(root);
            var shaders = Directory.GetFiles(root, "*", SearchOption.AllDirectories);

            foreach (var shader in shaders)
            {
                if (shader.EndsWith(".meta"))
                    continue;

                using (var sr = new StreamReader(shader))
                {
                    var str = sr.ReadToEnd();
                    str = str.Replace(ShaderFullPlaceholder + "/" + ShaderPlaceholder, ShaderFullPlaceholder + "/" + _name);
                    str = str.Replace(PassPlaceholder, _pass);

                    var name = shader.Replace(root, "").Substring(1);
                    var path = Path.Combine(parent, _name, name);
                    var container = Path.GetDirectoryName(path);
                    if (!Directory.Exists(container))
                        Directory.CreateDirectory(container);

                    Debug.Log($"Write Duplicated Lyrics Shader to {path}.");

                    using (var sw = new StreamWriter(path))
                        sw.WriteLine(str);
                }
            }
        }

        private enum Category
        {
            [EnumMember(Value = "Color")]
            Color = 1,

            Stencil,

            Outline,

            Inverse,

            Advanced,
        }

        // ReSharper disable InconsistentNaming

        private MaterialProperty MainTexture;
        private MaterialProperty MainColor;
        private MaterialProperty AlphaTransparency;
        private MaterialProperty IsEnableOutline;
        private MaterialProperty OutlineClearColor;
        private MaterialProperty IsOutlineRenderEdgeOnly;
        private MaterialProperty OutlineColor;
        private MaterialProperty OutlineWidth;
        private MaterialProperty IsEnableInverseColor;
        private MaterialProperty InverseWeight;
        private MaterialProperty StencilRef;
        private MaterialProperty StencilComp;
        private MaterialProperty StencilPass;
        private MaterialProperty StencilFail;
        private MaterialProperty StencilZFail;
        private MaterialProperty StencilReadMask;
        private MaterialProperty StencilWriteMask;
        private MaterialProperty Culling;
        private MaterialProperty _ZTest;
        private MaterialProperty _ZWrite;
        private MaterialProperty FoldoutStatus1;
        private MaterialProperty FoldoutStatus2;

        // ReSharper restore InconsistentNaming
    }
}