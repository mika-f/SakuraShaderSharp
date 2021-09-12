#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Primitives;

namespace NatsunekoLaboratory.SakuraShader.MotionGraphics.Shader
{
    [External]
    public class BuiltinOverload
    {
        [Function("distance")]
        public static extern SlFloat Distance(SlFloat a, SlFloat b);

        [Function("lerp")]
        public static extern SlFloat4 Lerp(SlFloat4 a, SlFloat4 b, SlFloat x);

        [Function("lerp")]
        public static extern SlFloat2 Lerp(SlFloat2 a, SlFloat2 b, SlFloat x);

    }
}

#endif