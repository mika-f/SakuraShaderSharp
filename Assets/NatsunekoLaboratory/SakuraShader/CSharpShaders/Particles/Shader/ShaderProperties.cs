#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Enums;
using SharpX.Library.ShaderLab.Primitives;

namespace NatsunekoLaboratory.SakuraShader.Particles.Shader
{
    [Export("core")]
    public static class ShaderProperties
    {
        #region Main

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
        [HDR]
        [DefaultValue("(1, 1, 1, 1)")]
        public static Color MainColor { get; }

        [GlobalMember()]
        [Range(0, 1)]
        [DefaultValue(1)]
        public static SlFloat AlphaTransparency { get; }

        #endregion

        #region Emission

        [GlobalMember]
        [CustomInspectorAttribute("Toggle(_)")]
        [DefaultValue(false)]
        public static SlBool IsEnableEmission { get; }

        [GlobalMember]
        [Color]
        [HDR]
        [DefaultValue("(0, 0, 0, 1)")]
        public static Color EmissionColor { get; }


        [GlobalMember]
        [DefaultValue(@"""black"" {}")]
        public static Sampler2D EmissionTexture { get; }

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