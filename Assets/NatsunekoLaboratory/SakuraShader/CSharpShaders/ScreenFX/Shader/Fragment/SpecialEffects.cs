#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Primitives;
using SharpX.Library.ShaderLab.Statements;

using Color = SharpX.Library.ShaderLab.Primitives.SlFloat4;
using UV = SharpX.Library.ShaderLab.Primitives.SlFloat4;
using NormalizedUV = SharpX.Library.ShaderLab.Primitives.SlFloat2;

using static SharpX.Library.ShaderLab.Functions.Builtin;


namespace NatsunekoLaboratory.SakuraShader.ScreenFX.Shader.Fragment
{
    [Export("frag-effect.{extension}")]
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
                    var newUV = new NormalizedUV(uv.X * GlobalProperties.BlockNoiseFactor * Utilities.GetAspectRatio(), uv.Y * GlobalProperties.BlockNoiseFactor);
                    var random = Utilities.Random(Floor(newUV + factor));
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
            var bApplied = BuiltinOverride.Lerp(nApplied, GlobalProperties.CinemascopeColor, Step(i.Vertex.Y, bPixels));
            var tApplied = BuiltinOverride.Lerp(bApplied, GlobalProperties.CinemascopeColor, 1 - Step(i.Vertex.Y, tPixels));

            color = tApplied;
        }

        public static void ApplyGlitch(ref Color color, NormalizedUV uv)
        {
            switch (GlobalProperties.GlitchMode)
            {
                case GlitchMode.Block:
                {
                    var block = new NormalizedUV((100 - GlobalProperties.GlitchBlockSizeX * 100) * Utilities.GetAspectRatio(), 100 - GlobalProperties.GlitchBlockSizeY * 100);
                    var pixel = Floor(uv * block) / block * UnityInjection.Time.Y;
                    var random = Random.WhiteNoise12(pixel.X, pixel.Y);
                    var sign = Lerp(-1, 1, Utilities.GreaterThan(random, 0.5f));

                    var distortion = Lerp(0f, Random.Hash11(pixel.X) * GlobalProperties.GlitchAberrationOffset, Utilities.GreaterThan(random, GlobalProperties.GlitchThreshold));

                    var glitchColorR = Tex2D(GlobalProperties.GrabTexture, new NormalizedUV(uv.X + distortion * sign, uv.Y)).R;
                    var glitchColorG = Tex2D(GlobalProperties.GrabTexture, new NormalizedUV(uv.X + distortion * -sign, uv.Y)).G;

                    var s1 = 1 - Utilities.LessThanOrEquals(Abs(glitchColorR - color.R), 0.01f);
                    var s2 = 1 - Utilities.LessThanOrEquals(Abs(glitchColorG - color.G), 0.01f);

                    var base1 = Tex2D(GlobalProperties.GrabTexture, new NormalizedUV(uv.X + distortion * sign * 0.5f, uv.Y));
                    var base2 = Tex2D(GlobalProperties.GrabTexture, new NormalizedUV(uv.X + distortion * -sign * 0.5f, uv.Y));
                    var grayscaled1 = Dot(base1.RGB, new Color(0.2989f, 0.5870f, 0.1140f, color.A).RGB);
                    var grayscaled2 = Dot(base2.RGB, new Color(0.2989f, 0.5870f, 0.1140f, color.A).RGB);
                    ;
                    color = BuiltinOverride.Lerp(base1, new Color(grayscaled1, grayscaled1, grayscaled1, color.A) * s1, Utilities.GreaterThan(random, GlobalProperties.GlitchThreshold));
                    color = BuiltinOverride.Lerp(base1, new Color(grayscaled1, grayscaled2, grayscaled2, color.A) * s2, Utilities.GreaterThan(random, GlobalProperties.GlitchThreshold));
                    color.R += Lerp(0, s1 * glitchColorR, Utilities.GreaterThan(random, GlobalProperties.GlitchThreshold));
                    color.G += Lerp(0, s2 * glitchColorG, Utilities.GreaterThan(random, GlobalProperties.GlitchThreshold));

                    break;
                }

                // based on https://github.com/keijiro/KinoGlitch/blob/master/Assets/Kino/Glitch/Shader/AnalogGlitch.shader
                case GlitchMode.KinoAnalog:
                {
                    var jitter = Random.WhiteNoise12(uv.Y, UnityInjection.Time.X) * 2 - 1;
                    jitter *= Step(Saturate(1.0f - GlobalProperties.GlitchScanLineJitter * 1.2f), Abs(jitter)) * (0.002f + Pow(GlobalProperties.GlitchScanLineJitter, 3) * 0.5f);

                    var jump = Lerp(uv.Y, Frac(uv.Y * UnityInjection.Delta.Y * GlobalProperties.GlitchVerticalJumpAmount * 11.3f), GlobalProperties.GlitchVerticalJumpAmount);
                    var shake = Random.WhiteNoise12(UnityInjection.Time.X, 2) * GlobalProperties.GlitchHorizontalShake * 0.2f;
                    var drift = Sin(jump + UnityInjection.Time.Y * 606.11f) * GlobalProperties.GlitchColorDriftAmount * 0.4f;

                    var src1 = Tex2D(GlobalProperties.GrabTexture, Frac(new NormalizedUV(uv.X + jitter + shake, jump)));
                    var src2 = Tex2D(GlobalProperties.GrabTexture, Frac(new NormalizedUV(uv.X + jitter + shake + drift, jump)));

                    var sign1 = Lerp(0, 1, Utilities.GreaterThan(Abs(color.R - src1.R), 0.01f));
                    var sign2 = Lerp(0, 1, Utilities.GreaterThan(Abs(color.G - src2.G), 0.01f));
                    var sign3 = Lerp(0, 1, Utilities.GreaterThan(Abs(color.B - src1.B), 0.01f));

                    color.R = Lerp(color.R, Saturate(0.65f * color.R + 0.35f * src1.R), sign1);
                    color.G = Lerp(color.G, Saturate(0.65f * color.G + 0.35f * src2.G), sign2);
                    color.B = Lerp(color.B, Saturate(0.65f * color.B + 0.35f * src1.B), sign3);

                    break;
                }
            }
        }

        public static void ApplyGirlsCam(ref Color color, NormalizedUV uv)
        {
            var width = Utilities.Random(new SlFloat2(0, uv.Y) * UnityInjection.Time.X) / 10f;
            var y = Sin(uv.Y * 500);

            uv.X += Lerp(-1, 1, Step(y, 0)) * width;

            color = width < GlobalProperties.GirlsCamSize ? Tex2Dlod(GlobalProperties.GrabTexture, Saturate(new UV(uv, 0, 0))) : color;
        }

        public static void ApplyColoredCheckerboard(ref Color color, NormalizedUV uv)
        {
            var rotate = Utilities.RotateByAngle(new NormalizedUV(uv.X * Utilities.GetAspectRatio(), uv.Y), Radians(GlobalProperties.ColoredCheckerboardAngle));
            var cols = Floor(rotate.X * (100 - GlobalProperties.ColoredCheckerboardWidth * 100));
            var rows = Floor(rotate.Y * (100 - GlobalProperties.ColoredCheckerboardHeight * 100));

            var newColor = BuiltinOverride.Lerp(GlobalProperties.ColoredCheckerboardColor1, GlobalProperties.ColoredCheckerboardColor2, Utilities.IsEquals(Fmod(cols + rows, 2), 0));
            color = BuiltinOverride.Lerp(color, Saturate(color + newColor), GlobalProperties.ColoredCheckerboardWeight);
        }
    }
}

#endif