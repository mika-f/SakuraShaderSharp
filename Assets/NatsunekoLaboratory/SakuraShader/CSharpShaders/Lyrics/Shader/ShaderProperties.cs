#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Enums;
using SharpX.Library.ShaderLab.Primitives;

namespace NatsunekoLaboratory.SakuraShader.Lyrics.Shader
{
    [Export("core")]
    public static class ShaderProperties
    {
        #region Clipping

        [GlobalMember]
        [CustomInspectorAttribute("Toggle(_)")]
        [DefaultValue(false)]
        public static SlBool IsEnableClipping { get; }

        #endregion

        #region Unity Injection

        [GlobalMember]
        [Property("_GrabTexture")]
        [NotExportToInspector]
        public static Sampler2D GrabTextureModel { get; }

        [GlobalMember]
        [Property("SakuraShader_Lyrics_WorldGrab")]
        [NotExportToInspector]
        public static Sampler2D GrabTextureWorld { get; }

        #endregion

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
        public static Color MainColor { get; }


        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(1)]
        public static SlFloat AlphaTransparency { get; }

        #endregion

        #region Outline

        [GlobalMember]
        [CustomInspectorAttribute("Toggle(_)")]
        [DefaultValue(false)]
        public static SlBool IsEnableOutline { get; }

        [GlobalMember]
        [Color]
        [DefaultValue("(255, 0, 255, 1)")]
        public static Color OutlineClearColor { get; }

        [GlobalMember]
        [CustomInspectorAttribute("Toggle(_)")]
        [DefaultValue(false)]
        public static SlBool IsOutlineRenderEdgeOnly { get; }

        [GlobalMember]
        [Color]
        [DefaultValue("(0, 0, 0, 1)")]
        public static Color OutlineColor { get; }

        [GlobalMember]
        [Range(0, 20)]
        [DefaultValue(0)]
        public static SlFloat OutlineWidth { get; }

        #endregion

        #region IsEnableInverseColor

        [GlobalMember]
        [CustomInspectorAttribute("Toggle(_)")]
        [DefaultValue(false)]
        public static SlBool IsEnableInverseColor { get; }

        [GlobalMember]
        [Range(0, 1)]
        [DefaultValue(1)]
        public static SlFloat InverseWeight { get; }

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