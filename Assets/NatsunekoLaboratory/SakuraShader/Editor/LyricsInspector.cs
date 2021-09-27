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

        private enum Category
        {
            [EnumMember(Value = "Color")]
            Color = 1,

            Stencil,

            Outline,

            Inverse,
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