#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Primitives;

namespace NatsunekoLaboratory.SakuraShader.Avatars.Toon.Shader
{
    [Export("frag")]
    internal static class FragmentShader
    {
        [FragmentShader]
        [return: Semantic("SV_TARGET")]
        public static SlFloat4 Fragment(Vertex2Fragment i)
        {
            return new(0, 0, 0, 1);
        }
    }
}

#endif