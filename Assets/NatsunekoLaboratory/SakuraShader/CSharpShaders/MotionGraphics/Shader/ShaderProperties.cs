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

        [GlobalMember]
        [CustomInspectorAttribute("Toggle(_)")]
        [DefaultValue(false)]
        public static SlBool IsOutlined { get; }

        [GlobalMember]
        [Color]
        [DefaultValue("(0, 0, 0, 1)")]
        public static SlFloat4 OutlineColor { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Toggle(_)")]
        [DefaultValue(true)]
        public static SlBool IsKeepAspectRatio { get; }

        [GlobalMember()]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(1, 1, 0, 0)")]
        public static SlFloat4 AspectRatio { get; }

        #endregion

        #region 1st Shape

        [GlobalMember]
        [Enum(typeof(Shape))]
        [DefaultValue(Shape.None)]
        public static Shape Shape1 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0, 0, 0, 0)")]
        public static SlFloat4 PositionOffset1 { get; }

        [GlobalMember]
        [Range(0, 360)]
        [DefaultValue(0)]
        public static SlFloat RotationAngle1 { get; }

        [GlobalMember]
        [Range(0, 10)]
        [DefaultValue(1)]
        public static SlFloat Scale1 { get; }

        [GlobalMember]
        [Enum(typeof(RepeatMode))]
        [DefaultValue(RepeatMode.None)]
        public static RepeatMode RepeatMode1 { get; }

        [GlobalMember]
        [Range(0.0001f, 1)]
        [DefaultValue(0.0001f)]
        public static SlFloat RepeatPeriod1 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0, 0, 0, 0)")]
        public static SlFloat4 RepeatLimitedRangeA1 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(1, 1, 0, 0)")]
        public static SlFloat4 RepeatLimitedRangeB1 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Toggle(_)")]
        [DefaultValue(false)]
        public static SlBool IsOnion1 { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0.25)]
        public static SlFloat OnionThickness1 { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0)]
        public static SlFloat Round1 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0.5, 0.5, 0, 0)")]
        public static SlFloat4 BoxSize1 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0.5, 0.5, 0, 0)")]
        public static SlFloat4 TriangleSize1 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0.1, 0.1, 0, 0)")]
        public static SlFloat4 SegmentA1 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0.9, 0.9, 0, 0)")]
        public static SlFloat4 SegmentB1 { get; }

        [GlobalMember]
        [DefaultValue(0.01)]
        public static SlFloat SegmentThickness1 { get; }

        [GlobalMember]
        [Range(0, 360)]
        [DefaultValue(0)]
        public static SlFloat PieAngle1 { get; }

        #endregion

        #region 2nd Shape

        [GlobalMember]
        [Enum(typeof(Shape))]
        [DefaultValue(Shape.None)]
        public static Shape Shape2 { get; }

        [GlobalMember]
        [Enum(typeof(CombinationFunction))]
        [DefaultValue(CombinationFunction.Union)]
        public static CombinationFunction CombinationFunction2 { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0.5)]
        public static SlFloat CombinationRate2 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0, 0, 0, 0)")]
        public static SlFloat4 PositionOffset2 { get; }

        [GlobalMember]
        [Range(0, 360)]
        [DefaultValue(0)]
        public static SlFloat RotationAngle2 { get; }

        [GlobalMember]
        [Range(0, 10)]
        [DefaultValue(1)]
        public static SlFloat Scale2 { get; }

        [GlobalMember]
        [Enum(typeof(RepeatMode))]
        [DefaultValue(RepeatMode.None)]
        public static RepeatMode RepeatMode2 { get; }

        [GlobalMember]
        [Range(0.0001f, 1)]
        [DefaultValue(0.0001f)]
        public static SlFloat RepeatPeriod2 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0, 0, 0, 0)")]
        public static SlFloat4 RepeatLimitedRangeA2 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(1, 1, 0, 0)")]
        public static SlFloat4 RepeatLimitedRangeB2 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Toggle(_)")]
        [DefaultValue(false)]
        public static SlBool IsOnion2 { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0.25)]
        public static SlFloat OnionThickness2 { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0)]
        public static SlFloat Round2 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0.5, 0.5, 0, 0)")]
        public static SlFloat4 BoxSize2 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0.5, 0.5, 0, 0)")]
        public static SlFloat4 TriangleSize2 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0.1, 0.1, 0, 0)")]
        public static SlFloat4 SegmentA2 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0.9, 0.9, 0, 0)")]
        public static SlFloat4 SegmentB2 { get; }

        [GlobalMember]
        [DefaultValue(0.01)]
        public static SlFloat SegmentThickness2 { get; }

        [GlobalMember]
        [Range(0, 360)]
        [DefaultValue(0)]
        public static SlFloat PieAngle2 { get; }

        #endregion

        #region 3rd Shape

        [GlobalMember]
        [Enum(typeof(Shape))]
        [DefaultValue(Shape.None)]
        public static Shape Shape3 { get; }

        [GlobalMember]
        [Enum(typeof(CombinationFunction))]
        [DefaultValue(CombinationFunction.Union)]
        public static CombinationFunction CombinationFunction3 { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0.5)]
        public static SlFloat CombinationRate3 { get; }


        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0, 0, 0, 0)")]
        public static SlFloat4 PositionOffset3 { get; }

        [GlobalMember]
        [Range(0, 360)]
        [DefaultValue(0)]
        public static SlFloat RotationAngle3 { get; }

        [GlobalMember]
        [Range(0, 10)]
        [DefaultValue(1)]
        public static SlFloat Scale3 { get; }

        [GlobalMember]
        [Enum(typeof(RepeatMode))]
        [DefaultValue(RepeatMode.None)]
        public static RepeatMode RepeatMode3 { get; }

        [GlobalMember]
        [Range(0.0001f, 1)]
        [DefaultValue(0.0001f)]
        public static SlFloat RepeatPeriod3 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0, 0, 0, 0)")]
        public static SlFloat4 RepeatLimitedRangeA3 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(1, 1, 0, 0)")]
        public static SlFloat4 RepeatLimitedRangeB3 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Toggle(_)")]
        [DefaultValue(false)]
        public static SlBool IsOnion3 { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0.25)]
        public static SlFloat OnionThickness3 { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0)]
        public static SlFloat Round3 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0.5, 0.5, 0, 0)")]
        public static SlFloat4 BoxSize3 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0.5, 0.5, 0, 0)")]
        public static SlFloat4 TriangleSize3 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0.1, 0.1, 0, 0)")]
        public static SlFloat4 SegmentA3 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0.9, 0.9, 0, 0)")]
        public static SlFloat4 SegmentB3 { get; }

        [GlobalMember]
        [DefaultValue(0.01)]
        public static SlFloat SegmentThickness3 { get; }

        [GlobalMember]
        [Range(0, 360)]
        [DefaultValue(0)]
        public static SlFloat PieAngle3 { get; }

        #endregion

        #region 4th Shape

        [GlobalMember]
        [Enum(typeof(Shape))]
        [DefaultValue(Shape.None)]
        public static Shape Shape4 { get; }

        [GlobalMember]
        [Enum(typeof(CombinationFunction))]
        [DefaultValue(CombinationFunction.Union)]
        public static CombinationFunction CombinationFunction4 { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0.5)]
        public static SlFloat CombinationRate4 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0, 0, 0, 0)")]
        public static SlFloat4 PositionOffset4 { get; }

        [GlobalMember]
        [Range(0, 360)]
        [DefaultValue(0)]
        public static SlFloat RotationAngle4 { get; }

        [GlobalMember]
        [Range(0, 10)]
        [DefaultValue(1)]
        public static SlFloat Scale4 { get; }

        [GlobalMember]
        [Enum(typeof(RepeatMode))]
        [DefaultValue(RepeatMode.None)]
        public static RepeatMode RepeatMode4 { get; }

        [GlobalMember]
        [Range(0.0001f, 1)]
        [DefaultValue(0.0001f)]
        public static SlFloat RepeatPeriod4 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0, 0, 0, 0)")]
        public static SlFloat4 RepeatLimitedRangeA4 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(1, 1, 0, 0)")]
        public static SlFloat4 RepeatLimitedRangeB4 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Toggle(_)")]
        [DefaultValue(false)]
        public static SlBool IsOnion4 { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0.25)]
        public static SlFloat OnionThickness4 { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0)]
        public static SlFloat Round4 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0.5, 0.5, 0, 0)")]
        public static SlFloat4 BoxSize4 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0.5, 0.5, 0, 0)")]
        public static SlFloat4 TriangleSize4 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0.1, 0.1, 0, 0)")]
        public static SlFloat4 SegmentA4 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0.9, 0.9, 0, 0)")]
        public static SlFloat4 SegmentB4 { get; }

        [GlobalMember]
        [DefaultValue(0.01)]
        public static SlFloat SegmentThickness4 { get; }

        [GlobalMember]
        [Range(0, 360)]
        [DefaultValue(0)]
        public static SlFloat PieAngle4 { get; }

        #endregion

        #region 5th Shape

        [GlobalMember]
        [Enum(typeof(Shape))]
        [DefaultValue(Shape.None)]
        public static Shape Shape5 { get; }

        [GlobalMember]
        [Enum(typeof(CombinationFunction))]
        [DefaultValue(CombinationFunction.Union)]
        public static CombinationFunction CombinationFunction5 { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0.5)]
        public static SlFloat CombinationRate5 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0, 0, 0, 0)")]
        public static SlFloat4 PositionOffset5 { get; }

        [GlobalMember]
        [Range(0, 360)]
        [DefaultValue(0)]
        public static SlFloat RotationAngle5 { get; }

        [GlobalMember]
        [Range(0, 10)]
        [DefaultValue(1)]
        public static SlFloat Scale5 { get; }

        [GlobalMember]
        [Enum(typeof(RepeatMode))]
        [DefaultValue(RepeatMode.None)]
        public static RepeatMode RepeatMode5 { get; }

        [GlobalMember]
        [Range(0.0001f, 1)]
        [DefaultValue(0.0001f)]
        public static SlFloat RepeatPeriod5 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0, 0, 0, 0)")]
        public static SlFloat4 RepeatLimitedRangeA5 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(1, 1, 0, 0)")]
        public static SlFloat4 RepeatLimitedRangeB5 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Toggle(_)")]
        [DefaultValue(false)]
        public static SlBool IsOnion5 { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0.25)]
        public static SlFloat OnionThickness5 { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0)]
        public static SlFloat Round5 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0.5, 0.5, 0, 0)")]
        public static SlFloat4 BoxSize5 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0.5, 0.5, 0, 0)")]
        public static SlFloat4 TriangleSize5 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0.1, 0.1, 0, 0)")]
        public static SlFloat4 SegmentA5 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0.9, 0.9, 0, 0)")]
        public static SlFloat4 SegmentB5 { get; }

        [GlobalMember]
        [DefaultValue(0.01)]
        public static SlFloat SegmentThickness5 { get; }

        [GlobalMember]
        [Range(0, 360)]
        [DefaultValue(0)]
        public static SlFloat PieAngle5 { get; }

        #endregion

        #region 6th Shape

        [GlobalMember]
        [Enum(typeof(Shape))]
        [DefaultValue(Shape.None)]
        public static Shape Shape6 { get; }

        [GlobalMember]
        [Enum(typeof(CombinationFunction))]
        [DefaultValue(CombinationFunction.Union)]
        public static CombinationFunction CombinationFunction6 { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0.5)]
        public static SlFloat CombinationRate6 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0, 0, 0, 0)")]
        public static SlFloat4 PositionOffset6 { get; }

        [GlobalMember]
        [Range(0, 360)]
        [DefaultValue(0)]
        public static SlFloat RotationAngle6 { get; }

        [GlobalMember]
        [Range(0, 10)]
        [DefaultValue(1)]
        public static SlFloat Scale6 { get; }

        [GlobalMember]
        [Enum(typeof(RepeatMode))]
        [DefaultValue(RepeatMode.None)]
        public static RepeatMode RepeatMode6 { get; }

        [GlobalMember]
        [Range(0.0001f, 1)]
        [DefaultValue(0.0001f)]
        public static SlFloat RepeatPeriod6 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0, 0, 0, 0)")]
        public static SlFloat4 RepeatLimitedRangeA6 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(1, 1, 0, 0)")]
        public static SlFloat4 RepeatLimitedRangeB6 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Toggle(_)")]
        [DefaultValue(false)]
        public static SlBool IsOnion6 { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0.25)]
        public static SlFloat OnionThickness6 { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0)]
        public static SlFloat Round6 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0.5, 0.5, 0, 0)")]
        public static SlFloat4 BoxSize6 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0.5, 0.5, 0, 0)")]
        public static SlFloat4 TriangleSize6 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0.1, 0.1, 0, 0)")]
        public static SlFloat4 SegmentA6 { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Vector2")]
        [DefaultValue("(0.9, 0.9, 0, 0)")]
        public static SlFloat4 SegmentB6 { get; }

        [GlobalMember]
        [DefaultValue(0.01)]
        public static SlFloat SegmentThickness6 { get; }

        [GlobalMember]
        [Range(0, 360)]
        [DefaultValue(0)]
        public static SlFloat PieAngle6 { get; }

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