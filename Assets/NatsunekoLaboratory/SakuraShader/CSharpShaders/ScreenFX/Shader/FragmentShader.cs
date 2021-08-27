#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Functions;
using SharpX.Library.ShaderLab.Primitives;
using SharpX.Library.ShaderLab.Statements;

using Color = SharpX.Library.ShaderLab.Primitives.SlFloat4;
using UV = SharpX.Library.ShaderLab.Primitives.SlFloat4;


namespace NatsunekoLaboratory.SakuraShader.ScreenFX.Shader
{
    [Export("frag.{extension}")]
    internal class FragmentShader
    {
        private static SlFloat Random(SlFloat2 value)
        {
            return Builtin.Frac(Builtin.Sin(Builtin.Dot(value, new SlFloat2(12.9898f, 78.233f))) * 43758.5453f);
        }

        private static void ApplyChromaticAberration(ref Color color, SlFloat2 uv, SlFloat3 normal)
        {
            Compiler.AnnotatedStatement("unrolled", () => { });

            for (SlInt i = 0; i < 2; i++)
            {
                SlInt sign = i == 0 ? 1 : -1;
                var rOffset = uv + new SlFloat2(GlobalProperties.ChromaticAberrationRedOffsetX * sign, GlobalProperties.ChromaticAberrationRedOffsetY * sign);
                var gOffset = uv + new SlFloat2(GlobalProperties.ChromaticAberrationGreenOffsetX * sign, GlobalProperties.ChromaticAberrationGreenOffsetY * sign);
                var bOffset = uv + new SlFloat2(GlobalProperties.ChromaticAberrationBlueOffsetX * sign, GlobalProperties.ChromaticAberrationBlueOffsetY * sign);

                var r = Builtin.Tex2Dlod(GlobalProperties.GrabTexture, new UV(rOffset, 0, 0)).R;
                var g = Builtin.Tex2Dlod(GlobalProperties.GrabTexture, new UV(gOffset, 0, 0)).G;
                var b = Builtin.Tex2Dlod(GlobalProperties.GrabTexture, new UV(bOffset, 0, 0)).B;

                color += BuiltinOverride.Lerp(new Color(0, 0, 0, 0), new Color(r, g, b, 0), GlobalProperties.ChromaticAberrationWeight) * sign;
            }
        }

        private static void ApplyNoise(ref Color color, SlFloat2 uv)
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
                    var random = Random(uv + factor);
                    color = BuiltinOverride.Lerp(color, new Color(random, random, random, color.A), GlobalProperties.NoiseWeight);
                    break;
                }

                case NoisePattern.RandomColor:
                {
                    var r = Random(uv + factor + 0);
                    var g = Random(uv + factor + 1);
                    var b = Random(uv + factor + 2);
                    color = BuiltinOverride.Lerp(color, new Color(r, g, b, color.A), GlobalProperties.NoiseWeight);
                    break;
                }

                case NoisePattern.Block:
                {
                    var newUV = new SlFloat2(uv.X * GlobalProperties.BlockNoiseFactor, uv.Y * GlobalProperties.BlockNoiseFactor * (UnityInjection.ScreenParams.Y / UnityInjection.ScreenParams.X));
                    var random = Random(Builtin.Floor(newUV + factor));
                    color = BuiltinOverride.Lerp(color, new Color(random, random, random, color.A), GlobalProperties.NoiseWeight);
                    break;
                }
            }
        }

        private static void ApplyColorInverse(ref Color color)
        {
            var r = 1 - color.R;
            var g = 1 - color.G;
            var b = 1 - color.B;

            var inverse = new Color(r, g, b, color.A);

            color = BuiltinOverride.Lerp(color, inverse, GlobalProperties.ColorInverseWeight);
        }

        private static void ApplyGrayscale(ref Color color)
        {
            var grayscale = Builtin.Dot(color.RGB, new Color(0.2989f, 0.5870f, 0.1140f, color.A).RGB);
            color = BuiltinOverride.Lerp(color, new Color(grayscale, grayscale, grayscale, color.A), GlobalProperties.GrayscaleWeight);
        }

        // http://beesbuzz.biz/code/16-hsv-color-transforms
        private static void ApplyHueShift(ref Color color)
        {
            var v = GlobalProperties.BrightnessValue;
            var vsu = v * GlobalProperties.SaturationValue * Builtin.Cos(GlobalProperties.HueShiftValue * UnityCg.PI / 180f);
            var vsw = v * GlobalProperties.SaturationValue * Builtin.Sin(GlobalProperties.HueShiftValue * UnityCg.PI / 180f);

            var r = new Color(0, 0, 0, color.A);
            r.R = (0.299f * v + 0.701f * vsu + 0.168f * vsw) * color.R + (0.587f * v - 0.587f * vsu + 0.330f * vsw) * color.G + (0.114f * v - 0.114f * vsu - 0.497f * vsw) * color.B;
            r.G = (0.299f * v - 0.299f * vsu - 0.328f * vsw) * color.R + (0.587f * v + 0.413f * vsu + 0.035f * vsw) * color.G + (0.114f * v - 0.114f * vsu - 0.292f * vsw) * color.B;
            r.B = (0.299f * v - 0.300f * vsu + 1.250f * vsw) * color.R + (0.587f * v - 0.588f * vsu - 1.050f * vsw) * color.G + (0.114f * v + 0.886f * vsu - 0.203f * vsw) * color.B;

            color = BuiltinOverride.Lerp(color, r, GlobalProperties.HueShiftWeight);
        }

        private static void ApplySepiaColor(ref Color color)
        {
            var r = Builtin.Saturate(0.393f * color.R + 0.796f * color.G + 0.189f * color.B);
            var g = Builtin.Saturate(0.349f * color.R + 0.686f * color.G + 0.168f * color.B);
            var b = Builtin.Saturate(0.272f * color.R + 0.534f * color.G + 0.131f * color.B);

            var sepia = new Color(r, g, b, color.A);
            color = BuiltinOverride.Lerp(color, sepia, GlobalProperties.SepiaWeight);
        }

        private static void ApplyCinemascope(Vertex2Fragment i,ref  Color color)
        {
            var height = UnityInjection.ScreenParams.Y / 2.0f * GlobalProperties.CinemascopeWidth;
            var tPixels = UnityInjection.ScreenParams.Y - height;
            var bPixels = height;

            var nApplied = color;
            var bApplied = BuiltinOverride.Lerp(nApplied, GlobalProperties.CinemascopeColor, Builtin.Step(i.Vertex.Y, bPixels));
            var tApplied = BuiltinOverride.Lerp(bApplied, GlobalProperties.CinemascopeColor, 1 - Builtin.Step(i.Vertex.Y, tPixels));

            color = tApplied;
        }

        private static SlFloat2 ComputeStereoScreenUV(Vertex2Fragment i)
        {
            var screen = i.ScreenPos.XY / i.ScreenPos.W;

            Compiler.Raw("#if defined(UNITY_SINGLE_PASS_STEREO)");

            var scaleOffset = UnityCg.StereoScaleOffset[UnityCg.StereoEyeIndex];
            screen = (screen - scaleOffset.ZW) / scaleOffset.XY;

            Compiler.Raw("#endif");

            return screen;
        }


        [FragmentShader]
        [return: Semantic("SV_TARGET")]
        public Color Fragment(Vertex2Fragment i)
        {
            var @base = i.GrabScreenPos.XY / i.GrabScreenPos.W;

            var color = Builtin.Tex2Dlod(GlobalProperties.GrabTexture, new Color(@base, 0, 0));
            var uv = ComputeStereoScreenUV(i);

            if (GlobalProperties.IsEnableChromaticAberration)
                ApplyChromaticAberration(ref color, @base, i.Normal);

            if (GlobalProperties.IsEnableNoise)
                ApplyNoise(ref color, uv);

            if (GlobalProperties.IsEnableColorInverse)
                ApplyColorInverse(ref color);

            if (GlobalProperties.IsEnableGrayscale)
                ApplyGrayscale(ref color);

            if (GlobalProperties.IsEnableHueShift)
                ApplyHueShift(ref color);

            if (GlobalProperties.IsEnableSepiaColor)
                ApplySepiaColor(ref color);

            if (GlobalProperties.IsEnableCinemascope)
                ApplyCinemascope(i, ref color);

            return color;
        }
    }
}

#endif