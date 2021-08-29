#if SHARPX_COMPILER
using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Functions;
using SharpX.Library.ShaderLab.Primitives;

namespace NatsunekoLaboratory.SakuraShader.ScreenFX.Shader
{
    [Export("utils.{extension}")]
    public class Utilities
    {
        public static SlFloat2 RotateByAngle(SlFloat2 current, SlFloat angle)
        {
            var value = current;
            value.X = current.X * Builtin.Cos(-angle) - current.Y * Builtin.Sin(-angle);
            value.Y = current.X * Builtin.Sin(-angle) + current.Y * Builtin.Cos(-angle);

            return value;
        }

        public static SlFloat Random(SlFloat2 value)
        {
            return Builtin.Frac(Builtin.Sin(Builtin.Dot(value, new SlFloat2(12.9898f, 78.233f))) * 43758.5453f);
        }

        public static SlFloat GetAspectRatio()
        {
            var projectionSpaceUpperRight = new SlFloat4(1, 1, UnityInjection.NearClipValue, UnityInjection.ProjectionParams.Y);
            var viewSpaceUpperRight = Builtin.Mul<SlFloat4>(UnityInjection.CameraInvProjection, projectionSpaceUpperRight);

            return viewSpaceUpperRight.X / viewSpaceUpperRight.Y;
        }

        public static SlFloat LessThanOrEquals(SlFloat x, SlFloat edge)
        {
            return Builtin.Step(x, edge);
        }

        public static SlFloat GreaterThanOrEquals(SlFloat x, SlFloat edge)
        {
            return Builtin.Step(edge, x);
        }

        public static SlFloat LessThan(SlFloat x, SlFloat edge)
        {
            return 1 - Builtin.Step(edge, x);
        }

        public static SlFloat GreaterThan(SlFloat x, SlFloat edge)
        {
            return 1 - Builtin.Step(x, edge);
        }

        public static SlFloat NotEquals(SlFloat x, SlFloat edge)
        {
            return Builtin.Abs(Builtin.Sign(x - edge));
        }

        public static SlFloat Equals(SlFloat x, SlFloat edge)
        {
            return 1 - Builtin.Abs(Builtin.Sign(x - edge));
        }
    }
}
#endif