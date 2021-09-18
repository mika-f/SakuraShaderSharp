#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Primitives;

namespace NatsunekoLaboratory.SakuraShader.MotionGraphics.Shader
{
    [Export("operator")]
    public static class Operator
    {
        public static SlFloat GreaterThanOrEquals(SlFloat x, SlFloat edge)
        {
            return Step(edge, x);
        }

        public static SlFloat LessThanOrEquals(SlFloat x, SlFloat edge)
        {
            return Step(x, edge);
        }

        public static SlFloat GreaterThan(SlFloat x, SlFloat edge)
        {
            return 1.0f - Step(x, edge);
        }

        public static SlFloat LessThan(SlFloat x, SlFloat edge)
        {
            return 1.0f - Step(edge, x);
        }

        public static SlFloat NotEquals(SlFloat x, SlFloat edge)
        {
            return Abs(Sign(x - edge));
        }

        public static SlFloat Equals(SlFloat x, SlFloat edge)
        {
            return 1.0f - Abs(Sign(x - edge));
        }
    }
}

#endif