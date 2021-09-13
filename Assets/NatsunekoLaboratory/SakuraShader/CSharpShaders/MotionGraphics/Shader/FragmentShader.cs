#if SHARPX_COMPILER

using NatsunekoLaboratory.SakuraShader.MotionGraphics.Shared;

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Primitives;
using SharpX.Library.ShaderLab.Statements;

namespace NatsunekoLaboratory.SakuraShader.MotionGraphics.Shader
{
    [Export("frag")]
    public class FragmentShader
    {
        // 2D SDF functions: https://www.iquilezles.org/www/articles/distfunctions2d/distfunctions2d.htm
        private SlFloat Circle(NormalizedUV uv, SlFloat r)
        {
            return Length(uv) - r;
        }

        private SlFloat Box(NormalizedUV uv, SlFloat r)
        {
            var d = Abs(uv) - new SlFloat2(r, r);
            return Length(Max(d, new SlFloat2(0.0f, 0.0f))) + Min(Max(d.X, d.Y), 0.0f);
        }

        private SlFloat Triangle(NormalizedUV uv, SlFloat2 p0, SlFloat2 p1, SlFloat2 p2)
        {
            var e0 = p1 - p0;
            var e1 = p2 - p1;
            var e2 = p0 - p2;
            var v0 = uv - p0;
            var v1 = uv - p1;
            var v2 = uv - p2;
            var pg0 = v0 - e0 * Saturate(Dot(v0, e0) / Dot(e0, e0));
            var pg1 = v1 - e1 * Saturate(Dot(v1, e1) / Dot(e1, e1));
            var pg2 = v2 - e2 * Saturate(Dot(v2, e2) / Dot(e2, e2));
            var s = Sign(e0.X * e2.Y - e0.Y * e2.X);
            var d = Min(Min(new SlFloat2(Dot(pg0, pg0), s * (v0.X * e0.Y - v0.Y * e0.X)), new SlFloat2(Dot(pg1, pg1), s * (v1.X * e1.Y - v1.Y * e1.X))), new SlFloat2(Dot(pg2, pg2), s * (v2.X * e2.Y - v2.Y * e2.X)));
            return -Sqrt(d.X) * Sign(d.Y);
        }

        private SlFloat EquilateralTriangle(NormalizedUV uv)
        {
            var k = Sqrt(3.0f);
            var p1 = new SlFloat2(Abs(uv.X) - 1.0f, uv.Y + 1.0f / k);
            var p2 = Lerp(p1, new SlFloat2(p1.X - k * p1.Y, -k * p1.X - p1.Y) / 2.0f, 1 - Step(p1.X + k * p1.Y, 0.0f));
            p2.X -= Clamp(p2.X, -2.0f, 0.0f);

            return -Length(p2) * Sign(p2.Y);
        }

        private SlFloat Star5(NormalizedUV uv, SlFloat r, SlFloat rf)
        {
            var k1 = new SlFloat2(0.809016994375f, -0.587785252292f);
            var k2 = new SlFloat2(-k1.X, k1.Y);
            var p = uv;
            p.X = Abs(p.X);
            p -= 2.0f * Max(Dot(k1, p), 0.0f) * k1;
            p -= 2.0f * Max(Dot(k2, p), 0.0f) * k2;
            p.X = Abs(p.X);
            p.Y -= r;

            var ba = new SlFloat2(rf, rf) * new SlFloat2(-k1.Y, k1.X) - new SlFloat2(0, 1);
            var h = Clamp(Dot(p, ba) / Dot(ba, ba), 0.0f, r);
            return Length(p - ba * h) * Sign(p.Y * ba.X - p.X * ba.Y);
        }

        private SlFloat ApplyBaseShape(NormalizedUV uv)
        {
            Compiler.AnnotatedStatement("branch", () => { });
            switch (ShaderProperties.BaseShape)
            {
                case Shape.Circle:
                    return Circle(uv, 0.25f);

                case Shape.Box:
                    return Box(uv, 0.25f);

                case Shape.Triangle:
                    return Triangle(uv, ShaderProperties.BaseTrianglePoint1.XY, ShaderProperties.BaseTrianglePoint2.XY, ShaderProperties.BaseTrianglePoint3.XY);

                case Shape.EquilateralTriangle:
                    return EquilateralTriangle(uv + new SlFloat2(0f, 0.25f));

                case Shape.Star:
                    return Star5(uv, 0.7f, 2f);
            }

            return new SlFloat(0);
        }

        private SlFloat ApplySecondShape(NormalizedUV uv)
        {
            Compiler.AnnotatedStatement("branch", () => { });
            switch (ShaderProperties.SecondShape)
            {
                case Shape.Circle:
                    return Circle(uv, 0.25f);

                case Shape.Box:
                    return Box(uv, 0.25f);

                case Shape.Triangle:
                    return Triangle(uv, ShaderProperties.SecondTrianglePoint1.XY, ShaderProperties.SecondTrianglePoint2.XY, ShaderProperties.SecondTrianglePoint3.XY);

                case Shape.EquilateralTriangle:
                    return EquilateralTriangle(uv + new SlFloat2(0f, 0.25f));

                case Shape.Star:
                    return Star5(uv, 0.7f, 2f);
            }

            return new SlFloat(0);
        }

        private SlFloat ApplyBoolean(SlFloat a, SlFloat b)
        {
            return Max(a, -b);
        }

        [FragmentShader]
        [return: Semantic("SV_Target")]
        public Color FragmentMain(Vertex2Fragment i)
        {
            if (ShaderProperties.BaseShapeScale == 0)
                return new SlFloat4(1, 1, 1, 0);

            var color = Tex2D(ShaderProperties.MainTexture, i.TexCoord) * ShaderProperties.MainColor;

            var position = Frac(i.TexCoord) * 2 - 1;
            var f = ApplyBaseShape((position + ShaderProperties.BaseShapeOffset) / ShaderProperties.BaseShapeScale) * ShaderProperties.BaseShapeScale;
            var s = ApplySecondShape((position + ShaderProperties.SecondShapeOffset) / ShaderProperties.SecondShapeScale) * ShaderProperties.SecondShapeScale;

            return Lerp(new SlFloat4(1, 1, 1, 0), color, Step(ApplyBoolean(f, s), 0));
        }
    }
}

#endif