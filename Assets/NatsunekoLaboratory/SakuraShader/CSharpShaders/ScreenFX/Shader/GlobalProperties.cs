#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Enums;
using SharpX.Library.ShaderLab.Primitives;

namespace NatsunekoLaboratory.SakuraShader.ScreenFX.Shader
{
    [Export("core.{extension}")]
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

        #endregion

        #region Colors

        #region Chromatic Aberration

        [GlobalMember]
        [CustomInspectorAttribute("Toggle(_)")]
        [DefaultValue(false)]
        public static SlBool IsEnableChromaticAberration { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0)]
        public static SlFloat ChromaticAberrationRedOffsetX { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0)]
        public static SlFloat ChromaticAberrationRedOffsetY { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0)]
        public static SlFloat ChromaticAberrationGreenOffsetX { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0)]
        public static SlFloat ChromaticAberrationGreenOffsetY { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0)]
        public static SlFloat ChromaticAberrationBlueOffsetX { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0)]
        public static SlFloat ChromaticAberrationBlueOffsetY { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0)]
        public static SlFloat ChromaticAberrationWeight { get; }

        #endregion

        #region Color Reverse

        [GlobalMember]
        [CustomInspectorAttribute("Toggle(_)")]
        [DefaultValue(false)]
        public static SlBool IsEnableColorInverse { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0)]
        public static SlFloat ColorInverseWeight { get; }

        #endregion

        #region Hue Shift

        [GlobalMember]
        [CustomInspectorAttribute("Toggle(_)")]
        [DefaultValue(false)]
        public static SlBool IsEnableHueShift { get; }

        [GlobalMember]
        [Range(0, 360)]
        [DefaultValue(180)]
        public static SlFloat HueShiftValue { get; }

        [GlobalMember]
        [DefaultValue(0)]
        public static SlFloat SaturationValue { get; }

        [GlobalMember]
        [DefaultValue(0)]
        public static SlFloat BrightnessValue { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0)]
        public static SlFloat HueShiftWeight { get; }

        #endregion

        #region Grayscale

        [GlobalMember]
        [CustomInspectorAttribute("Toggle(_)")]
        [DefaultValue(false)]
        public static SlBool IsEnableGrayscale { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0)]
        public static SlFloat GrayscaleWeight { get; }

        #endregion

        #region Sepia Color

        [GlobalMember]
        [DefaultValue(false)]
        [CustomInspectorAttribute("Toggle(_)")]
        public static SlBool IsEnableSepiaColor { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0)]
        public static SlFloat SepiaWeight { get; }

        #endregion

        #endregion

        #region Distortion

        #region Melt

        [GlobalMember]
        [CustomInspectorAttribute("Toggle(_)")]
        [DefaultValue(false)]
        public static SlBool IsEnableMelt { get; }

        [GlobalMember]
        [Range(0, 360)]
        [DefaultValue(0)]
        public static SlFloat MeltAngle { get; }

        [GlobalMember]
        [Range(0, 0.4f)]
        [DefaultValue(0)]
        public static SlFloat MeltInterval { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0)]
        public static SlFloat MeltVariance { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0)]
        public static SlFloat MeltDistance { get; }

        [GlobalMember]
        [DefaultValue(0)]
        public static SlFloat MeltSeed { get; }

        #endregion

        #region Screen Movement

        [GlobalMember]
        [CustomInspectorAttribute("Toggle(_)")]
        [DefaultValue(false)]

        public static SlBool IsEnableScreenMovement { get; }

        [GlobalMember]
        [DefaultValue(0)]
        public static SlFloat ScreenMovementX { get; }

        [GlobalMember]
        [DefaultValue(0)]
        public static SlFloat ScreenMovementY { get; }

        [GlobalMember]
        [DefaultValue(0)]
        public static SlFloat ScreenMovementZ { get; }

        #endregion

        #region Screen Transform

        [GlobalMember]
        [DefaultValue(false)]
        [CustomInspectorAttribute("Toggle(_)")]
        public static SlBool IsEnableScreenTransform { get; }

        [GlobalMember]
        [Range(0, 2)]
        [DefaultValue(0)]
        public static SlFloat TransformHorizontal { get; }

        [GlobalMember]
        [Range(0, 2)]
        [DefaultValue(0)]
        public static SlFloat TransformVertical { get; }

        #endregion

        #region Pixelation

        [GlobalMember]
        [DefaultValue(false)]
        [CustomInspectorAttribute("Toggle(_)")]
        public static SlBool IsEnablePixelation { get; }

        [GlobalMember]
        [Range(1, 128)]
        [DefaultValue(0)]
        public static SlFloat PixelationHeight { get; }

        [GlobalMember]
        [Range(1, 128)]
        [DefaultValue(0)]
        public static SlFloat PixelationWidth { get; }

        #endregion

        #endregion

        #region Effects

        #region Cinemascope

        [GlobalMember]
        [DefaultValue(false)]
        [CustomInspectorAttribute("Toggle(_)")]
        public static SlBool IsEnableCinemascope { get; }

        [GlobalMember]
        [DefaultValue("(0, 0, 0, 1)")]
        [Color]
        public static SlFloat4 CinemascopeColor { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0)]
        public static SlFloat CinemascopeWidth { get; }

        #endregion

        #region Girlscam

        [GlobalMember]
        [CustomInspectorAttribute("Toggle(_)")]
        [DefaultValue(false)]
        public static SlBool IsEnableGirlsCam { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0.05)]
        public static SlFloat GirlsCamSize { get; }

        #endregion

        #region Color Layer

        [GlobalMember]
        [CustomInspectorAttribute("Toggle(_)")]
        [DefaultValue(false)]
        public static SlBool IsEnableColorLayer { get; }

        [GlobalMember]
        [Enum(typeof(LayerBlendMode))]
        [DefaultValue(LayerBlendMode.None)]
        public static LayerBlendMode LayerBlendMode { get; }

        [GlobalMember]
        [Color]
        [DefaultValue("(1, 1, 1, 1)")]
        public static SlFloat4 LayerColor { get; }


        #endregion

        #region Noise

        [GlobalMember]
        [CustomInspectorAttribute("Toggle(_)")]
        [DefaultValue(false)]
        public static SlBool IsEnableNoise { get; }

        [GlobalMember]
        [Enum(typeof(NoisePattern))]
        [DefaultValue(NoisePattern.Random)]
        public static NoisePattern NoisePattern { get; }

        [GlobalMember]
        [Range(0, 100)]
        [DefaultValue(0)]
        public static SlFloat BlockNoiseFactor { get; }

        [GlobalMember]
        [Enum(typeof(NoiseRandomFactor))]
        [DefaultValue(NoiseRandomFactor.Constant)]
        public static NoiseRandomFactor NoiseRandomFactor { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0)]
        public static SlFloat NoiseWeight { get; }

        #endregion

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