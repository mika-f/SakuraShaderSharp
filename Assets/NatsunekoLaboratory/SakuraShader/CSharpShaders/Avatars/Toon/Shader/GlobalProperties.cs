#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Enums;
using SharpX.Library.ShaderLab.Primitives;

namespace NatsunekoLaboratory.SakuraShader.Avatars.Toon.Shader
{ 
    [Export("core")]
    internal static class GlobalProperties
    {
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