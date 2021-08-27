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

        #endregion

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
        [DefaultValue(0)]
        public static SlInt FoldoutStatus { get; }

        #endregion
    }
}

#endif