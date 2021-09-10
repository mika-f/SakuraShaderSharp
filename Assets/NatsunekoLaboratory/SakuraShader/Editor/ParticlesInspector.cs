
using System.Runtime.Serialization;

using UnityEditor;

using UnityEngine;

#pragma warning disable 649

namespace NatsunekoLaboratory.SakuraShader
{
    public class ParticlesInspector : SakuraShaderInspector
    {
        public override void OnGUI(MaterialEditor me, MaterialProperty[] properties)
        {
            var material = (Material)me.target;

            InjectMaterialProperties(properties);

            OnHeaderGui("Particles Shader");
            OnInitialize(material);
            OnInitializeFoldout(FoldoutStatus1, FoldoutStatus2);

            OnMainGui(me);
            OnEmissionGui(me);
            OnStencilGui(me);
            OnOthersGui(me, Culling, ZWrite);
            OnStoreFoldout(FoldoutStatus1, FoldoutStatus2);
        }

        private void OnMainGui(MaterialEditor me)
        {
            OnFoldOutGui(Category.Main, () =>
            {
                me.TextureProperty(MainTexture, "Main Texture");
                me.ShaderProperty(MainColor, "Color");
                me.ShaderProperty(AlphaTransparency, "Alpha Transparency");
            });
        }

        private void OnEmissionGui(MaterialEditor me)
        {
            OnFoldoutAndToggleGui(Category.Emission, IsEnableEmission, () =>
            {
                me.TexturePropertySingleLine(new GUIContent("Texture"), EmissionTexture);
                me.ShaderProperty(EmissionColor, "Color");
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
            [EnumMember(Value = "Main Color")]
            Main = 1, 

            [EnumMember(Value = "Emission")]
            Emission,

            Stencil,
        }

        // ReSharper disable InconsistentNaming

        private MaterialProperty MainTexture;
        private MaterialProperty MainColor;
        private MaterialProperty AlphaTransparency;
        private MaterialProperty IsEnableEmission;
        private MaterialProperty EmissionTexture;
        private MaterialProperty EmissionColor;
        private MaterialProperty StencilRef;
        private MaterialProperty StencilComp;
        private MaterialProperty StencilPass;
        private MaterialProperty StencilFail;
        private MaterialProperty StencilZFail;
        private MaterialProperty StencilReadMask;
        private MaterialProperty StencilWriteMask;
        private MaterialProperty Culling;
        private MaterialProperty ZWrite;
        private MaterialProperty FoldoutStatus1;
        private MaterialProperty FoldoutStatus2;

        // ReSharper restore InconsistentNaming


    }
}
