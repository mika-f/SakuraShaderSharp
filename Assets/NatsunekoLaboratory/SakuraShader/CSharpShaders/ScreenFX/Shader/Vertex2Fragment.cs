#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Primitives;

namespace NatsunekoLaboratory.SakuraShader.ScreenFX.Shader
{
    [Component]
    [Export("core.{extension}")]
    internal class Vertex2Fragment
    {
        [Semantic("SV_POSITION")]
        public SlFloat4 Vertex { get; set; }

        [Semantic("NORMAL")]
        public SlFloat3 Normal { get; set; }

        [Semantic("TEXCOORD0")]
        public SlFloat2 TexCoord { get; set; }

        [Semantic("TEXCOORD1")]
        public SlFloat3 WorldPos { get; set; }

        [Semantic("TEXCOORD2")]
        public SlFloat3 LocalPos { get; set; }

        [Semantic("TEXCOORD3")]
        public SlFloat4 ScreenPos { get; set; }

        [Semantic("TEXCOORD4")]
        public SlFloat4 GrabScreenPos { get; set; }
    }
}

#endif