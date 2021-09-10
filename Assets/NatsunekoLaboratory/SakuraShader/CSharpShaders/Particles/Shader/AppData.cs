#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Primitives;

namespace NatsunekoLaboratory.SakuraShader.Particles.Shader
{
    [Export("core")]
    [Component]
    public class AppData
    {
        [Semantic("POSITION")]
        public extern SlFloat4 Position { get; }

        [Semantic("COLOR")]
        public extern Color Color { get; }

        [Semantic("TEXCOORD0")]
        public extern SlFloat4 TexCoord { get; }
    }
}

#endif