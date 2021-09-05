#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Primitives;

namespace NatsunekoLaboratory.SakuraShader.Avatars.Effects.Shader
{
    [Export("core")]
    [Component]
    internal class Geometry2Fragment
    {
        [Semantic("SV_POSITION")]
        public SlFloat4 Position { get; set; }

        [Semantic("NORMAL")]
        public Normal Normal { get; set; }

        [Semantic("TEXCOORD0")]
        public NormalizedUV UV { get; set; }

        [Semantic("TEXCOORD1")]
        public SlFloat3 WorldPos { get; set; }

        [Semantic("TEXCOORD2")]
        public SlFloat3 LocalPos { get; set; }

        [Semantic("TEXCOORD3")]
        public SlFloat3 Bary { get; set; }
    }
}

#endif