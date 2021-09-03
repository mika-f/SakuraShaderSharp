#if SHARPX_COMPILER
using System;

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Primitives;

namespace NatsunekoLaboratory.SakuraShader.ScreenFX.Shader
{
    [Export("utils")]
    public class Utilities
    {
        public static SlFloat2 RotateByAngle(SlFloat2 current, SlFloat angle)
        {
            var value = current;
            value.X = current.X * Cos(-angle) - current.Y * Sin(-angle);
            value.Y = current.X * Sin(-angle) + current.Y * Cos(-angle);

            return value;
        }

        [Obsolete("This is a obsolete API, use Random#WhiteNoise")]
        public static SlFloat Random(SlFloat2 value)
        {
            return Frac(Sin(Dot(value, new SlFloat2(12.9898f, 78.233f))) * 43758.5453f);
        }

        public static SlFloat GetAspectRatio()
        {
            var projectionSpaceUpperRight = new SlFloat4(1, 1, UnityInjection.NearClipValue, UnityInjection.ProjectionParams.Y);
            var viewSpaceUpperRight = Mul<SlFloat4>(UnityInjection.CameraInvProjection, projectionSpaceUpperRight);

            return viewSpaceUpperRight.X / viewSpaceUpperRight.Y;
        }

        public static SlFloat LessThanOrEquals(SlFloat x, SlFloat edge)
        {
            return Step(x, edge);
        }

        public static SlFloat GreaterThanOrEquals(SlFloat x, SlFloat edge)
        {
            return Step(edge, x);
        }

        public static SlFloat LessThan(SlFloat x, SlFloat edge)
        {
            return 1 - Step(edge, x);
        }

        public static SlFloat GreaterThan(SlFloat x, SlFloat edge)
        {
            return 1 - Step(x, edge);
        }

        public static SlFloat NotEquals(SlFloat x, SlFloat edge)
        {
            return Abs(Sign(x - edge));
        }

        public static SlFloat IsEquals(SlFloat x, SlFloat edge)
        {
            return 1 - Abs(Sign(x - edge));
        }

        public static SlFloat IsEquals31(SlFloat3 x, SlFloat3 edge)
        {
            var diff = new SlFloat3(1, 1, 1) - Abs(Sign(x - edge));
            return 1 - Saturate(diff.X + diff.Y + diff.Z);
        }
    }
}
#endif