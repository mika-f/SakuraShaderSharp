#if SHARPX_COMPILER

using NatsunekoLaboratory.SakuraShader.Avatars.Effects.Common;

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Enums;
using SharpX.Library.ShaderLab.Primitives;

namespace NatsunekoLaboratory.SakuraShader.Avatars.Effects.Shader
{
    [Export("core")]
    public static class GlobalProperties
    {
        #region Unity Injection

        [GlobalMember]
        [NotExportToInspector]
        public static Sampler2D GrabTexture { get; }

        #endregion

        #region Main

        [GlobalMember]
        [MainTexture]
        [DisplayName(nameof(MainTexture))]
        [DefaultValue(@"""white"" { }")]
        public static Sampler2D MainTexture { get; }


        [GlobalMember]
        [NotExportToInspector]
        [Property("MainTexture_ST")]
        public static SlFloat4 MainTextureST { get; }

        [GlobalMember]
        [MainColor]
        [Color]
        [DefaultValue("(0, 0, 0, 1)")]
        public static Color MainColor { get; }

        #endregion

        #region Toon - Rim Lighting

        [GlobalMember]
        [CustomInspectorAttribute("Toggle(_)")]
        [DefaultValue(false)]
        public static SlBool IsEnableRimLighting { get; }

        [GlobalMember]
        [Color]
        [DefaultValue("(1, 1, 1, 1)")]
        public static Color RimLightingColor { get; }

        [GlobalMember]
        [Range(0, 2)]
        [DefaultValue(1)]
        public static SlFloat RimLightingPower { get; }

        #endregion

        #region Effects - Voxelation

        [GlobalMember]
        [CustomInspectorAttribute("Toggle(_)")]
        [DefaultValue(false)]
        public static SlBool IsEnableVoxelization { get; }

        [GlobalMember]
        [Enum(typeof(VoxelSource))]
        [DefaultValue(VoxelSource.ShaderProperty)]
        public static VoxelSource VoxelSource { get; }

        [GlobalMember]
        [DefaultValue(0.00125)]
        public static SlFloat VoxelSize { get; }

        [GlobalMember]
        [DefaultValue("(1, 1, 1, 1)")]
        public static SlFloat4 VoxelSizeRatio { get; }

        [GlobalMember]
        [Enum(typeof(UvSamplingSource))]
        [DefaultValue(UvSamplingSource.Center)]
        public static UvSamplingSource VoxelUvSamplingSource { get; }

        [GlobalMember]
        [Color]
        [DefaultValue("(0, 0, 0, 1)")]
        public static Color VoxelColor { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Toggle(_)")]
        [DefaultValue(false)]
        public static SlBool IsEnableVoxelBoundary { get; }

        [GlobalMember]
        [Range(-0.5f, 1.5f)]
        [DefaultValue(1)]
        public static SlFloat VoxelBoundaryX { get; }

        [GlobalMember]
        [Range(-0.5f, 1.5f)]
        [DefaultValue(1)]
        public static SlFloat VoxelBoundaryY { get; }

        [GlobalMember]
        [Range(-0.5f, 1.5f)]
        [DefaultValue(1)]
        public static SlFloat VoxelBoundaryZ { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0.0025)]
        public static SlFloat VoxelBoundaryRange { get; }

        [GlobalMember]
        [DefaultValue(7.5)]
        [Range(0, 10)]
        public static SlFloat VoxelBoundaryFactor { get; }

        [GlobalMember]
        [Enum(typeof(BoundaryOperator))]
        [DefaultValue(BoundaryOperator.LessThan)]
        public static BoundaryOperator VoxelBoundaryOperator { get; }

        [GlobalMember]
        [DefaultValue("(0, 0, 0, 0)")]
        public static SlFloat4 VoxelOffset { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Toggle(_)")]
        [DefaultValue(false)]
        public static SlBool IsEnableVoxelAnimation { get; }

        #endregion

        #region Effects - Holograph

        [GlobalMember]
        [CustomInspectorAttribute("Toggle(_)")]
        [DefaultValue(false)]
        public static SlBool IsEnableTriangleHolograph { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(1)]
        public static SlFloat HolographAlphaTransparency { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0.0125)]
        public static SlFloat HolographHeight { get; }

        #endregion

        #region Stencil

        [GlobalMember]
        [DefaultValue(0)]
        public static SlInt StencilRef { get; }

        [GlobalMember]
        [Enum("UnityEngine.Rendering.CompareFunction")]
        [DefaultValue(CompareFunction.Disabled)]
        public static CompareFunction StencilComp { get; }

        [GlobalMember]
        [Enum("UnityEngine.Rendering.StencilOp")]
        [DefaultValue(StencilOp.Keep)]
        public static StencilOp StencilPass { get; }

        [GlobalMember]
        [Enum("UnityEngine.Rendering.StencilOp")]
        [DefaultValue(StencilOp.Keep)]
        public static StencilOp StencilFail { get; }

        [GlobalMember]
        [Enum("UnityEngine.Rendering.StencilOp")]
        [DefaultValue(StencilOp.Keep)]
        public static StencilOp StencilZFail { get; }

        [GlobalMember]
        [DefaultValue(0)]
        public static SlInt StencilReadMask { get; }

        [GlobalMember]
        [DefaultValue(0)]
        public static SlInt StencilWriteMask { get; }

        #endregion

        #region Meta

        [GlobalMember]
        [HideInInspector]
        [DefaultValue(0)]
        public static SlInt FoldoutStatus1 { get; }

        [GlobalMember]
        [HideInInspector]
        [DefaultValue(0)]
        public static SlInt FoldoutStatus2 { get; }

        #endregion
    }
}

#endif