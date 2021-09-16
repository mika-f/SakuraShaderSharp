#if SHARPX_COMPILER

using NatsunekoLaboratory.SakuraShader.MotionGraphics.Shared;

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Enums;
using SharpX.Library.ShaderLab.Primitives;

namespace NatsunekoLaboratory.SakuraShader.MotionGraphics.Shader
{
    [Export("core")]
    public static class ShaderProperties
    {
        #region Color

        [GlobalMember]
        [MainTexture]
        [DefaultValue(@"""white"" {}")]
        public static Sampler2D MainTexture { get; }

        [GlobalMember]
        [Property("MainTexture_ST")]
        [NotExportToInspector]
        public static SlFloat4 MainTextureST { get; }

        [GlobalMember]
        [MainColor]
        [Color]
        [DefaultValue("(1, 1, 1, 1)")]
        public static SlFloat4 MainColor { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(1)]
        public static SlFloat AlphaTransparency { get; }

        #endregion

        #region Base Shape

        [GlobalMember]
        [Enum(typeof(Shape))]
        [DefaultValue(Shape.Circle)]
        public static Shape BaseShape { get; }

        [GlobalMember]
        [Range(0, 10)]
        [DefaultValue(0.1)]
        public static SlFloat BaseShapeScale { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2Drawer")]
        [DefaultValue("(0, 0, 0, 0)")]
        public static SlFloat4 BaseShapeOffset { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0)]
        public static SlFloat BaseShapeRounded { get; }

        [GlobalMember]
        [Range(0, 360)]
        [DefaultValue(0)]
        public static SlFloat BaseShapeRotate { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2Drawer")]
        [DefaultValue("(0.1, 0.1, 0, 0)")]
        public static SlFloat4 BaseBoxVector { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2Drawer")]
        [DefaultValue("(0.1, 0.1, 0, 0)")]
        public static SlFloat4 BaseTrianglePoint1 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2Drawer")]
        [DefaultValue("(0.5, 0.5, 0, 0)")]
        public static SlFloat4 BaseTrianglePoint2 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2Drawer")]
        [DefaultValue("(0.3, 0.3, 0, 0)")]
        public static SlFloat4 BaseTrianglePoint3 { get; }

        #endregion

        #region Second Shape

        [GlobalMember]
        [Enum(typeof(Shape))]
        [DefaultValue(Shape.Circle)]
        public static Shape SecondShape { get; }

        [GlobalMember]
        [Range(0, 10)]
        [DefaultValue(0.01)]
        public static SlFloat SecondShapeScale { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2Drawer")]
        [DefaultValue("(0, 0, 0, 0)")]
        public static SlFloat4 SecondShapeOffset { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0)]
        public static SlFloat SecondShapeRounded { get; }

        [GlobalMember]
        [Range(0, 360)]
        [DefaultValue(0)]
        public static SlFloat SecondShapeRotate { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2Drawer")]
        [DefaultValue("(0.1, 0.1, 0, 0)")]
        public static SlFloat4 SecondBoxVector { get; }


        [GlobalMember]
        [CustomInspectorAttribute("Vector2Drawer")]
        [DefaultValue("(0.1, 0.1, 0, 0)")]
        public static SlFloat4 SecondTrianglePoint1 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2Drawer")]
        [DefaultValue("(0.5, 0.5, 0, 0)")]
        public static SlFloat4 SecondTrianglePoint2 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2Drawer")]
        [DefaultValue("(0.3, 0.3, 0, 0)")]
        public static SlFloat4 SecondTrianglePoint3 { get; }

        #endregion

        #region Stencil

        [GlobalMember(true)]
        [DefaultValue(0)]
        public static SlInt StencilRef { get; }

        [GlobalMember(true)]
        [Enum("UnityEngine.Rendering.CompareFunction")]
        [DefaultValue(CompareFunction.Disabled)]
        public static CompareFunction StencilComp { get; }

        [GlobalMember(true)]
        [Enum("UnityEngine.Rendering.StencilOp")]
        [DefaultValue(StencilOp.Keep)]
        public static StencilOp StencilPass { get; }

        [GlobalMember(true)]
        [Enum("UnityEngine.Rendering.StencilOp")]
        [DefaultValue(StencilOp.Keep)]
        public static StencilOp StencilFail { get; }

        [GlobalMember(true)]
        [Enum("UnityEngine.Rendering.StencilOp")]
        [DefaultValue(StencilOp.Keep)]
        public static StencilOp StencilZFail { get; }

        [GlobalMember(true)]
        [DefaultValue(0)]
        public static SlInt StencilReadMask { get; }

        [GlobalMember(true)]
        [DefaultValue(0)]
        public static SlInt StencilWriteMask { get; }

        #endregion

        #region Culling and ZWrite

        [GlobalMember(true)]
        [Enum("UnityEngine.Rendering.CullMode")]
        [DefaultValue(SharpX.Library.ShaderLab.Enums.Culling.Back)]
        public static SlFloat Culling { get; }

        [GlobalMember(true)]
        [Enum("UnityEngine.Rendering.CompareFunction")]
        [Property("_ZTest")]
        [DefaultValue(ZTestFunc.LEqual)]
        public static SlFloat ZTest { get; }

        [GlobalMember(true)]
        [Enum("Off", 0, "On", 1)]
        [Property("_ZWrite")]
        [DefaultValue(Switch.On)]
        public static SlFloat ZWrite { get; }

        #endregion

        #region Meta

        [GlobalMember(true)]
        [HideInInspector]
        [DefaultValue(0)]
        public static SlInt FoldoutStatus1 { get; }

        [GlobalMember(true)]
        [HideInInspector]
        [DefaultValue(0)]
        public static SlInt FoldoutStatus2 { get; }

        #endregion
    }
}

#endif