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

        private SlFloat ApplyBaseShape(NormalizedUV uv)
        {
            Compiler.AnnotatedStatement("branch", () => {});
            switch (ShaderProperties.BaseShape)
            {
                case Shape.Circle:
                    return Circle(uv, 0.25f);

                case Shape.Box:
                    return Box(uv, 0.25f);
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
            var color = Tex2D(ShaderProperties.MainTexture, i.TexCoord) * ShaderProperties.MainColor;

            var position = Frac(i.TexCoord) * 2 - 1;
            var f = ApplyBaseShape(position / ShaderProperties.BaseShapeScale) * ShaderProperties.BaseShapeScale;
            var s = ApplySecondShape(position / ShaderProperties.SecondShapeScale) * ShaderProperties.SecondShapeScale;

            return BuiltinOverload.Lerp(new SlFloat4(1, 1, 1, 0), color, Step(ApplyBoolean(f, s), 0));
        }
    }
}

#endif