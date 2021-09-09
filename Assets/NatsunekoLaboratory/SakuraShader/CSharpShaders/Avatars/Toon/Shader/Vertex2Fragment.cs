#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Primitives;

namespace NatsunekoLaboratory.SakuraShader.Avatars.Toon.Shader
{
    [Component]
    [Export("core")]
    public class Vertex2Fragment
    {
        [Semantic("SV_POSITION")]
        public SlFloat4 Vertex { get; set; }

        [Semantic("NORMAL")]
        public Normal Normal { get; set; }

        [Semantic("TEXCOORD0")]
        public NormalizedUV TexCoord { get; set; }

        [Semantic("TEXCOORD1")]
        public SlFloat4 WorldPos { get; set; }

        [Semantic("TEXCOORD2")]
        public SlFloat3 LocalPos { get; set; }

    }
}

#endif