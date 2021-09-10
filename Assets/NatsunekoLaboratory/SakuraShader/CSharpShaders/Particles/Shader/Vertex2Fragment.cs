#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Primitives;

namespace NatsunekoLaboratory.SakuraShader.Particles.Shader
{
    [Export("core")]
    [Component]
    public class Vertex2Fragment
    {
        [Semantic("SV_POSITION")]
        public SlFloat4 Vertex { get; set; }

        [Semantic("COLOR")]
        public Color Color { get; set; }

        [Semantic("TEXCOORD0")]
        public SlFloat2 TexCoord { get; set; }
    }
}

#endif