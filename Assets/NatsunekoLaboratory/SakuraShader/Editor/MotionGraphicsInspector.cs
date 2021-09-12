using System.Runtime.Serialization;

using UnityEditor;

using UnityEngine;

#pragma warning disable 649

namespace NatsunekoLaboratory.SakuraShader
{
    public class MotionGraphicsInspector : SakuraShaderInspector
    {
        public override void OnGUI(MaterialEditor me, MaterialProperty[] properties)
        {
            var material = (Material)me.target;

            InjectMaterialProperties(properties);

            OnHeaderGui("Motion Graphics Shader");
            OnInitialize(material);
            OnInitializeFoldout(FoldoutStatus1, FoldoutStatus2);

            OnMainColorGui(me);
            OnBaseShapeGui(me);
            OnSecondShapeGui(me);
            OnStencilGui(me);
            OnOthersGui(me, Culling, _ZWrite, _ZTest);
            OnStoreFoldout(FoldoutStatus1, FoldoutStatus2);
        }
        private void OnMainColorGui(MaterialEditor me)
        {
            OnFoldOutGui(Category.Color, () =>
            {
                me.TextureProperty(MainTexture, "Texture");
                me.ShaderProperty(MainColor, "Color");
            });
        }

        private void OnBaseShapeGui(MaterialEditor me)
        {
            OnFoldOutGui(Category.BaseShape, () =>
            {
                me.ShaderProperty(BaseShape, "Shape");
                me.ShaderProperty(BaseShapeScale, "Scale");
                me.ShaderProperty(BaseShapeRounded, "Rounded");
                me.ShaderProperty(BaseShapeRotate, "Rotate");
            });
        }

        private void OnSecondShapeGui(MaterialEditor me)
        {
            OnFoldOutGui(Category.SecondShape, () =>
            {
                me.ShaderProperty(SecondShape, "Shape");
                me.ShaderProperty(SecondShapeScale, "Scale");
                me.ShaderProperty(SecondShapeRounded, "Rounded");
                me.ShaderProperty(SecondShapeRotate, "Rotate");
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
            Color = 1,

            Stencil,

            [EnumMember(Value = "Base Shape")]
            BaseShape,

            [EnumMember(Value = "Clipping Shape")]
            SecondShape,
        }

        // ReSharper disable InconsistentNaming

        private MaterialProperty MainTexture;
        private MaterialProperty MainColor;
        private MaterialProperty BaseShape;
        private MaterialProperty BaseShapeScale;
        private MaterialProperty BaseShapeRounded;
        private MaterialProperty BaseShapeRotate;
        private MaterialProperty SecondShape;
        private MaterialProperty SecondShapeScale;
        private MaterialProperty SecondShapeRounded;
        private MaterialProperty SecondShapeRotate;
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