#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Primitives;

namespace NatsunekoLaboratory.SakuraShader.ScreenFX.Shader
{
    // https://www.shadertoy.com/view/4djSRW
    [Export("random")]
    internal class Random
    {
        public static SlFloat WhiteNoise12(SlFloat a, SlFloat b)
        {
            return Frac(Sin(Dot(new SlFloat2(a, b), new SlFloat2(12.9898f, 78.233f))) * 43758.5453f);
        }

        public static SlFloat WhiteNoise11(SlFloat2 value)
        {
            return Frac(Sin(Dot(value, new SlFloat2(12.9898f, 78.233f))) * 43758.5453f);
        }

        public static SlFloat Hash11(SlFloat a)
        {
            a = Frac(a * 0.1031f);
            a *= a + 33.33f;
            a *= a + a;

            return Frac(a);
        }

        public static SlFloat Hash12(SlFloat a, SlFloat b)
        {
            var r = Frac(new SlFloat3(a, b, a) * 0.1031f);
            r += Dot(r, r.YXZ + 33.33f);

            return Frac((r.X + r.Y) * r.Z);
        }
    }
}

#endif