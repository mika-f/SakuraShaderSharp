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
    }
}
#endif