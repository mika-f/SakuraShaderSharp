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

        #region Cinemascope

        [GlobalMember]
        [DefaultValue(false)]
        [CustomInspectorAttribute("MaterialToggle")]
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
        [CustomInspectorAttribute("MaterialToggle")]
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

        #region Hue Shift

        [GlobalMember]
        [CustomInspectorAttribute("MaterialToggle")]
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
        [CustomInspectorAttribute("MaterialToggle")]
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
        [CustomInspectorAttribute("MaterialToggle")]
        public static SlBool IsEnableSepiaColor { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(0)]
        public static SlFloat SepiaWeight { get; }

        #endregion

        #region Stencil

        [GlobalMember]
        [DefaultValue(0)]
        public static SlInt StencilRef { get; }

        [GlobalMember]
        [DefaultValue(CompareFunction.Disabled)]
        public static CompareFunction StencilComp { get; }

        [GlobalMember]
        [DefaultValue(StencilOp.Keep)]
        public static StencilOp StencilPass { get; }

        [GlobalMember]
        [DefaultValue(StencilOp.Keep)]
        public static StencilOp StencilFail { get; }

        [GlobalMember]
        [DefaultValue(StencilOp.Keep)]
        public static StencilOp StencilZFail { get; }

        [GlobalMember]
        [DefaultValue(0)]
        public static SlInt StencilReadMask { get; }

        [GlobalMember]
        [DefaultValue(0)]
        public static SlInt StencilWriteMask { get; }

        #endregion
    }
}

#endif