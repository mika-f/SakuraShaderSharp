using System.Runtime.Serialization;

using NatsunekoLaboratory.SakuraShader.MotionGraphics.Shared;

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
                me.ShaderProperty(AlphaTransparency, "Alpha Transparency");
            });
        }

        private void OnBaseShapeGui(MaterialEditor me)
        {
            OnFoldOutGui(Category.BaseShape, () =>
            {
                me.ShaderProperty(BaseShape, "Shape");
                me.ShaderProperty(BaseShapeScale, "Scale");
                me.ShaderProperty(BaseShapeOffset, "Offset");
                me.ShaderProperty(BaseShapeRounded, "Rounded");
                me.ShaderProperty(BaseShapeRotate, "Rotate");

                EnabledWhen(BaseShape, Shape.Box, () =>
                {
                    me.ShaderProperty(BaseBoxVector, "Box Vector");
                });

                EnabledWhen(BaseShape, Shape.Triangle, () =>
                {
                    me.ShaderProperty(BaseTrianglePoint1, "Triangle Vertex A");
                    me.ShaderProperty(BaseTrianglePoint2,"Triangle Vertex B");
                    me.ShaderProperty(BaseTrianglePoint3, "Triangle Vertex C");
                });
            });
        }

        private void OnSecondShapeGui(MaterialEditor me)
        {
            OnFoldOutGui(Category.SecondShape, () =>
            {
                me.ShaderProperty(SecondShape, "Shape");
                me.ShaderProperty(SecondShapeScale, "Scale");
                me.ShaderProperty(SecondShapeOffset, "Offset");
                me.ShaderProperty(SecondShapeRounded, "Rounded");
                me.ShaderProperty(SecondShapeRotate, "Rotate");


                EnabledWhen(SecondShape, Shape.Box, () =>
                {
                    me.ShaderProperty(SecondBoxVector, "Box Vector");
                });

                EnabledWhen(SecondShape, Shape.Triangle, () =>
                {
                    me.ShaderProperty(SecondTrianglePoint1, "Triangle Vertex A");
                    me.ShaderProperty(SecondTrianglePoint2, "Triangle Vertex B");
                    me.ShaderProperty(SecondTrianglePoint3, "Triangle Vertex C");
                });

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
        private MaterialProperty AlphaTransparency;
        private MaterialProperty BaseShape;
        private MaterialProperty BaseShapeScale;
        private MaterialProperty BaseShapeOffset;
        private MaterialProperty BaseShapeRounded;
        private MaterialProperty BaseShapeRotate;
        private MaterialProperty BaseBoxVector;
        private MaterialProperty BaseTrianglePoint1;
        private MaterialProperty BaseTrianglePoint2;
        private MaterialProperty BaseTrianglePoint3;
        private MaterialProperty SecondShape;
        private MaterialProperty SecondShapeScale;
        private MaterialProperty SecondShapeOffset;
        private MaterialProperty SecondShapeRounded;
        private MaterialProperty SecondShapeRotate;
        private MaterialProperty SecondBoxVector;
        private MaterialProperty SecondTrianglePoint1;
        private MaterialProperty SecondTrianglePoint2;
        private MaterialProperty SecondTrianglePoint3;
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