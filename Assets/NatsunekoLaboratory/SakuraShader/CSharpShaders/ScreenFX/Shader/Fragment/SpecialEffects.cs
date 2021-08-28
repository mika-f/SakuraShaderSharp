#if SHARPX_COMPILER

using System;

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Functions;
using SharpX.Library.ShaderLab.Primitives;
using SharpX.Library.ShaderLab.Statements;

using Color = SharpX.Library.ShaderLab.Primitives.SlFloat4;
using UV = SharpX.Library.ShaderLab.Primitives.SlFloat4;
using NormalizedUV = SharpX.Library.ShaderLab.Primitives.SlFloat2;


namespace NatsunekoLaboratory.SakuraShader.ScreenFX.Shader.Fragment
{
    [Export("frag-special.{extension}")]
    internal class SpecialEffects
    {
        public static void ApplyNoise(ref Color color, NormalizedUV uv)
        {
            SlFloat factor = 0;

            Compiler.AnnotatedStatement("branch", () =>
            {
                switch (GlobalProperties.NoiseRandomFactor)
                {
                    case NoiseRandomFactor.Time:
                        factor = UnityInjection.Time.X;
                        break;

                    case NoiseRandomFactor.SinTime:
                        factor = UnityInjection.SinTime.X;
                        break;

                    case NoiseRandomFactor.CosTime:
                        factor = UnityInjection.CosTime.X;
                        break;
                }
            });

            Compiler.AnnotatedStatement("branch", () => { });
            switch (GlobalProperties.NoisePattern)
            {
                case NoisePattern.Random:
                {
                    var random = Utilities.Random(uv + factor);
                    color = BuiltinOverride.Lerp(color, new Color(random, random, random, color.A), GlobalProperties.NoiseWeight);
                    break;
                }

                case NoisePattern.RandomColor:
                {
                    var r = Utilities.Random(uv + factor + 0);
                    var g = Utilities.Random(uv + factor + 1);
                    var b = Utilities.Random(uv + factor + 2);
                    color = BuiltinOverride.Lerp(color, new Color(r, g, b, color.A), GlobalProperties.NoiseWeight);
                    break;
                }

                case NoisePattern.Block:
                {
                    var newUV = new SlFloat2(uv.X * GlobalProperties.BlockNoiseFactor, uv.Y * GlobalProperties.BlockNoiseFactor * (UnityInjection.ScreenParams.Y / UnityInjection.ScreenParams.X));
                    var random = Utilities.Random(Builtin.Floor(newUV + factor));
                    color = BuiltinOverride.Lerp(color, new Color(random, random, random, color.A), GlobalProperties.NoiseWeight);
                    break;
                }
            }
        }

        public static void ApplyCinemascope(Vertex2Fragment i, ref Color color)
        {
            var height = UnityInjection.ScreenParams.Y / 2.0f * GlobalProperties.CinemascopeWidth;
            var tPixels = UnityInjection.ScreenParams.Y - height;
            var bPixels = height;

            var nApplied = color;
            var bApplied = BuiltinOverride.Lerp(nApplied, GlobalProperties.CinemascopeColor, Builtin.Step(i.Vertex.Y, bPixels));
            var tApplied = BuiltinOverride.Lerp(bApplied, GlobalProperties.CinemascopeColor, 1 - Builtin.Step(i.Vertex.Y, tPixels));

            color = tApplied;
        }

        public static void ApplyGirlsCam(ref Color color, NormalizedUV uv)
        {
            var width = Utilities.Random(new SlFloat2(0, uv.Y) * UnityInjection.Time.X) / 10f;
            var y = Builtin.Sin(uv.Y * 500);

            uv.X += Builtin.Lerp(-1, 1, Builtin.Step(y, 0)) * width;

            color = width < GlobalProperties.GirlsCamSize ? Builtin.Tex2Dlod(GlobalProperties.GrabTexture, new UV(uv, 0, 0)) : color;
        }
    }
}

#endif