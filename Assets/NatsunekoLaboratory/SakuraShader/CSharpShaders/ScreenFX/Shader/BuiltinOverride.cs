#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Primitives;

namespace NatsunekoLaboratory.SakuraShader.ScreenFX.Shader
{
    [External]
    internal static class BuiltinOverride
    {
        [Function("lerp")]
        public static extern SlFloat4 Lerp(SlFloat4 a, SlFloat4 b, SlFloat c);
    }
}

#endif