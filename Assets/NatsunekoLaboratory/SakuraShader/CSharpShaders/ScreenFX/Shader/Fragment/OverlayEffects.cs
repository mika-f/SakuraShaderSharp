#if SHARPX_COMPILER

using NatsunekoLaboratory.SakuraShader.ScreenFX.Enums;

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Primitives;
using SharpX.Library.ShaderLab.Statements;

namespace NatsunekoLaboratory.SakuraShader.ScreenFX.Shader.Fragment
{
    [Export("frag-effect")]
    internal class OverlayEffects
    {
        public static void ApplyNoise(ref Color color, NormalizedUV uv)
        {
            SlFloat factor = 0;

            Compiler.AnnotatedStatement("branch", () =>
            {
                switch (ShaderProperties.NoiseRandomFactor)
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
            switch (ShaderProperties.NoisePattern)
            {
                case NoisePattern.Random:
                {
                    var random = Utilities.Random(uv + factor);
                    color = BuiltinOverride.Lerp(color, new Color(random, random, random, color.A), ShaderProperties.NoiseWeight);
                    break;
                }

                case NoisePattern.RandomColor:
                {
                    var r = Utilities.Random(uv + factor + 0);
                    var g = Utilities.Random(uv + factor + 1);
                    var b = Utilities.Random(uv + factor + 2);
                    color = BuiltinOverride.Lerp(color, new Color(r, g, b, color.A), ShaderProperties.NoiseWeight);
                    break;
                }

                case NoisePattern.Block:
                {
                    var newUV = new NormalizedUV(uv.X * ShaderProperties.BlockNoiseFactor * Utilities.GetAspectRatio(), uv.Y * ShaderProperties.BlockNoiseFactor);
                    var random = Utilities.Random(Floor(newUV + factor));
                    color = BuiltinOverride.Lerp(color, new Color(random, random, random, color.A), ShaderProperties.NoiseWeight);
                    break;
                }
            }
        }

        public static void ApplyCinemascope(Vertex2Fragment i, ref Color color)
        {
            var height = UnityInjection.ScreenParams.Y / 2.0f * ShaderProperties.CinemascopeWidth;
            var tPixels = UnityInjection.ScreenParams.Y - height;
            var bPixels = height;

            var nApplied = color;
            var bApplied = BuiltinOverride.Lerp(nApplied, ShaderProperties.CinemascopeColor, Step(i.Vertex.Y, bPixels));
            var tApplied = BuiltinOverride.Lerp(bApplied, ShaderProperties.CinemascopeColor, 1 - Step(i.Vertex.Y, tPixels));

            color = tApplied;
        }

        public static void ApplyGlitch(ref Color color, NormalizedUV uv)
        {
            Compiler.AnnotatedStatement("branch", () => { });
            switch (ShaderProperties.GlitchMode)
            {
                case GlitchMode.Block:
                {
                    var block = new NormalizedUV((100 - ShaderProperties.GlitchBlockSizeX * 100) * Utilities.GetAspectRatio(), 100 - ShaderProperties.GlitchBlockSizeY * 100);
                    var pixel = Floor(uv * block) / block * UnityInjection.Time.Y;
                    var random = Random.WhiteNoise12(pixel.X, pixel.Y);
                    var sign = Lerp(-1, 1, Utilities.GreaterThan(random, 0.5f));

                    var distortion = Lerp(0f, Random.Hash11(pixel.X) * ShaderProperties.GlitchAberrationOffset, Utilities.GreaterThan(random, ShaderProperties.GlitchThreshold));

                    var glitchColorR = Tex2D(ShaderProperties.GrabTexture, new NormalizedUV(uv.X + distortion * sign, uv.Y)).R;
                    var glitchColorG = Tex2D(ShaderProperties.GrabTexture, new NormalizedUV(uv.X + distortion * -sign, uv.Y)).G;

                    var s1 = 1 - Utilities.LessThanOrEquals(Abs(glitchColorR - color.R), 0.01f);
                    var s2 = 1 - Utilities.LessThanOrEquals(Abs(glitchColorG - color.G), 0.01f);

                    var @base = Tex2D(ShaderProperties.GrabTexture, new NormalizedUV(uv.X + distortion * sign * 0.5f, uv.Y));
                    var grayscaled = Dot(@base.RGB, new Color(0.2989f, 0.5870f, 0.1140f, color.A).RGB);

                    color = BuiltinOverride.Lerp(@base, new Color(grayscaled, grayscaled, grayscaled, color.A) * s1, Utilities.GreaterThan(random, ShaderProperties.GlitchThreshold));
                    color.R += Lerp(0, s1 * glitchColorR, Utilities.GreaterThan(random, ShaderProperties.GlitchThreshold));
                    color.G += Lerp(0, s2 * glitchColorG, Utilities.GreaterThan(random, ShaderProperties.GlitchThreshold));

                    break;
                }

                // based on https://github.com/keijiro/KinoGlitch/blob/master/Assets/Kino/Glitch/Shader/AnalogGlitch.shader
                case GlitchMode.KinoAnalog:
                {
                    var jitter = Random.WhiteNoise12(uv.Y, UnityInjection.Time.X) * 2 - 1;
                    jitter *= Step(Saturate(1.0f - ShaderProperties.GlitchScanLineJitter * 1.2f), Abs(jitter)) * (0.002f + Pow(ShaderProperties.GlitchScanLineJitter, 3) * 0.5f);

                    var jump = Lerp(uv.Y, Frac(uv.Y * UnityInjection.Delta.Y * ShaderProperties.GlitchVerticalJumpAmount * 11.3f), ShaderProperties.GlitchVerticalJumpAmount);
                    var shake = Random.WhiteNoise12(UnityInjection.Time.X, 2) * ShaderProperties.GlitchHorizontalShake * 0.2f;
                    var drift = Sin(jump + UnityInjection.Time.Y * 606.11f) * ShaderProperties.GlitchColorDriftAmount * 0.4f;

                    var src1 = Tex2D(ShaderProperties.GrabTexture, Frac(new NormalizedUV(uv.X + jitter + shake, jump)));
                    var src2 = Tex2D(ShaderProperties.GrabTexture, Frac(new NormalizedUV(uv.X + jitter + shake + drift, jump)));

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
            var width = Random.WhiteNoise12(0, uv.Y * UnityInjection.Time.X) / 10f;
            var y = Sin(uv.Y * 500);

            uv.X += Lerp(-1, 1, Step(y, 0)) * width;

            color = width < ShaderProperties.GirlsCamSize ? Tex2Dlod(ShaderProperties.GrabTexture, Saturate(new UV(uv, 0, 0))) : color;
        }

        public static void ApplyColoredCheckerboard(ref Color color, NormalizedUV uv)
        {
            var rotate = Utilities.RotateByAngle(new NormalizedUV(uv.X * Utilities.GetAspectRatio(), uv.Y), Radians(ShaderProperties.ColoredCheckerboardAngle));
            var cols = Floor(rotate.X * (100 - ShaderProperties.ColoredCheckerboardWidth * 100));
            var rows = Floor(rotate.Y * (100 - ShaderProperties.ColoredCheckerboardHeight * 100));

            var newColor = BuiltinOverride.Lerp(ShaderProperties.ColoredCheckerboardColor1, ShaderProperties.ColoredCheckerboardColor2, Utilities.IsEquals(Fmod(cols + rows, 2), 0));
            color = BuiltinOverride.Lerp(color, Saturate(color + newColor), ShaderProperties.ColoredCheckerboardWeight);
        }

        public static void ApplyImageOverlay(ref Color color, NormalizedUV uv)
        {
            var overlay = Tex2D(ShaderProperties.ImageOverlayTexture, uv);

            Compiler.AnnotatedStatement("branch", () => { });
            switch (ShaderProperties.ImageOverlayBlendMode)
            {
                case LayerBlendMode.None:
                    color.RGB = BuiltinOverride.Lerp(color.RGB, overlay.RGB, Utilities.GreaterThanOrEquals(overlay.A, .95f));
                    break;

                case LayerBlendMode.Darken:
                    color = Saturate(Min(color, overlay));
                    break;

                case LayerBlendMode.Lighten:
                    color = Saturate(Max(color, overlay));
                    break;

                case LayerBlendMode.ColorDarken:
                    color = BuiltinOverride.Lerp(color, overlay, Step(overlay.R + overlay.G + overlay.B, color.R + color.G + color.B));
                    break;

                case LayerBlendMode.ColorLighten:
                    color = BuiltinOverride.Lerp(overlay, color, Step(overlay.R + overlay.G + overlay.B, color.R + color.G + color.B));
                    break;

                case LayerBlendMode.ColorBurn:
                {
                    var r = 1 - (1 - color.R) * overlay.R;
                    var g = 1 - (1 - color.G) * overlay.G;
                    var b = 1 - (1 - color.B) * overlay.B;

                    color = Saturate(new Color(r, g, b, color.A));
                    break;
                }

                case LayerBlendMode.LinearBurn:
                {
                    var r = Lerp(0, color.R + overlay.R - 1, Step(1, color.R + overlay.R));
                    var g = Lerp(0, color.G + overlay.G - 1, Step(1, color.G + overlay.G));
                    var b = Lerp(0, color.B + overlay.B - 1, Step(1, color.B + overlay.B));

                    color = Saturate(new Color(r, g, b, color.A));
                    break;
                }

                case LayerBlendMode.Divide:
                    color = Saturate(color / overlay);
                    break;

                case LayerBlendMode.Multiply:
                    color = Saturate(color * overlay);
                    break;
                case LayerBlendMode.Subtract:
                    color = Saturate(overlay - color);
                    break;

                case LayerBlendMode.Difference:
                    color = Saturate(Abs(color - overlay));
                    break;

                case LayerBlendMode.Screen:
                    color = Saturate(color + overlay - color * overlay);
                    break;

                case LayerBlendMode.ColorDodge:
                {
                    var r = color.R * (1 - overlay.R);
                    var g = color.G * (1 - overlay.G);
                    var b = color.B * (1 - overlay.B);

                    color = Saturate(new Color(r, g, b, color.A));
                    break;
                }

                case
                    LayerBlendMode.LinearDodge:
                    color = Saturate(color + overlay);
                    break;

                case LayerBlendMode.Overlay:
                {
                    var r = Lerp(color.R * overlay.R * 2.0f, 1f - 2f * (1 - color.R) * (1 - overlay.R), Step(0.5f, color.R));
                    var g = Lerp(color.G * overlay.G * 2.0f, 1f - 2f * (1 - color.G) * (1 - overlay.G), Step(0.5f, color.G));
                    var b = Lerp(color.B * overlay.B * 2.0f, 1f - 2f * (1 - color.B) * (1 - overlay.B), Step(0.5f, color.B));

                    color = Saturate(new Color(r, g, b, color.A));
                    break;
                }
            }
        }

        public static void ApplyStageCurtain(ref Color color, NormalizedUV uv)
        {
            var width = 0.5f * ShaderProperties.StageCurtainWeight;
            var rPixels = 1.0f - width;
            var lPixels = width;


            if (ShaderProperties.IsStageCurtainFlipped)
            {
                var @override = Tex2D(ShaderProperties.StageCurtainTexture, uv) * ShaderProperties.StageCurtainColor;
                color = BuiltinOverride.Lerp(color, @override, Utilities.IsBetween(uv.X, lPixels, rPixels));
            }
            else
            {
                var leftOverride = Tex2D(ShaderProperties.StageCurtainTexture, uv - new SlFloat2(width, 0)) * ShaderProperties.StageCurtainColor;
                var rightOverride = Tex2D(ShaderProperties.StageCurtainTexture, uv + new SlFloat2(width, 0)) * ShaderProperties.StageCurtainColor;
                var nApplied = color;
                var rApplied = BuiltinOverride.Lerp(nApplied, rightOverride, Utilities.GreaterThanOrEquals(uv.X, rPixels));
                var lApplied = BuiltinOverride.Lerp(rApplied, leftOverride, Utilities.LessThanOrEquals(uv.X, lPixels));
                color = lApplied;
            }

        }

        // ReSharper disable once RedundantAssignment
        public static void ApplyBlur(ref Color color, NormalizedUV uv)
        {
            var blurredColor = new Color(0, 0, 0, 0);
            var iterations = Max(1, ShaderProperties.BlurSamplingIterations / 2);

            if (ShaderProperties.BlurAlgorithmMode == BlurAlgorithm.GaussianHorizontal)
            {
                var weights = 0.0f;

                for (var i = -iterations; i < iterations; i++)
                {
                    var weight = Exp(-0.5f * Pow(Abs(i), 2) / ShaderProperties.BlurFactor);
                    weights += weight;

                    blurredColor += Tex2D(ShaderProperties.GrabTexture, uv + ShaderProperties.BlurTexel * new SlFloat2(i, 0) / ShaderProperties.BlurSamplingIterations) * weight;
                }

                blurredColor /= weights;
            }

            if (ShaderProperties.BlurAlgorithmMode == BlurAlgorithm.GaussianVertical)
            {
                var weights = 0.0f;

                for (var i = -iterations; i < iterations; i++)
                {
                    var weight = Exp(-0.5f * Pow(Abs(i), 2) / ShaderProperties.BlurFactor);
                    weights += weight;

                    var samplingCoords = uv;

                    blurredColor += Tex2D(ShaderProperties.GrabTexture, samplingCoords + ShaderProperties.BlurTexel * new SlFloat2(0, i) / ShaderProperties.BlurSamplingIterations) * weight;
                }

                blurredColor /= weights;
            }

            color = blurredColor;
        }
    }
}

#endif