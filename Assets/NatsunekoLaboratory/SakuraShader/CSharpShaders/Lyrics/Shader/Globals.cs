#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Primitives;

#pragma warning disable 8618

namespace NatsunekoLaboratory.SakuraShader.Lyrics.Shader
{
    [Export("core.{extension}")]
    public static class Globals
    {
        #region Unity Global Fields

        [External]
        [GlobalMember]
        [Property("_Time")]
        public static SlFloat4 Time { get; }

        [External]
        [GlobalMember]
        [Property("_SinTime")]
        public static SlFloat4 SinTime { get; }

        [External]
        [GlobalMember]
        [Property("_CosTime")]
        public static SlFloat4 CosTime { get; }

        #endregion

        #region MainData

        [GlobalMember]
        [DisplayName("Texture")]
        [DefaultValue("\"white\" {}")]
        public static Sampler2D MainTexture { get; }

        [GlobalMember]
        [NotExportToInspector]
        [Property("MainTexture_ST")]
        public static SlFloat4 MainTextureST { get; }

        [GlobalMember]
        [DisplayName("Color")]
        [Color]
        public static SlFloat4 Color { get; }

        #endregion
    }
}

#endif