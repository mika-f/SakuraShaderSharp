#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Primitives;

namespace NatsunekoLaboratory.SakuraShader.ScreenFX.Shader
{
    [External]
    internal static class BuiltinOverride
    {
        [Function("lerp")]
        public static extern SlFloat3 Lerp(SlFloat3 a, SlFloat3 b, SlFloat c);

        [Function("lerp")]
        public static extern SlFloat4 Lerp(SlFloat4 a, SlFloat4 b, SlFloat c);

        [Function("smoothstep")]
        public static extern SlFloat4 Smoothstep(SlFloat x, SlFloat4 min, SlFloat4 max);
    }
}

#endif