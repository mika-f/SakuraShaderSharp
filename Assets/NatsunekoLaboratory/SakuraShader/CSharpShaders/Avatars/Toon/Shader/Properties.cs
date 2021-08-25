#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Primitives;

namespace NatsunekoLaboratory.SakuraShader.Avatars.Toon.Shader
{ 
    internal static class Properties
    {
        #region Main Properties

        [GlobalMember]
        [MainTexture]
        [DisplayName(nameof(MainTexture))]
        [DefaultValue(@"""white"" { }")]
        public static Sampler2D MainTexture { get; }


        [GlobalMember]
        [NotExportToInspector]
        [Property("_MainTex_ST")]
        public static SlFloat4 MainTextureST { get; }

        #endregion
    }
}

#endif