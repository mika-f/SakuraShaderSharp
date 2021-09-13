#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Primitives;

namespace NatsunekoLaboratory.SakuraShader.LyricsLightweight.Shader
{
    [External]
    public static class BuiltinOverload
    {
        [Function("lerp")]
        public static extern SlFloat4 Lerp(SlFloat4 a, SlFloat4 b, SlFloat x);
    }
}

#endif