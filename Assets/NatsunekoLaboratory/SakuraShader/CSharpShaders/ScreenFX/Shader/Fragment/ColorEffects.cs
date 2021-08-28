﻿#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Functions;
using SharpX.Library.ShaderLab.Primitives;
using SharpX.Library.ShaderLab.Statements;

using Color = SharpX.Library.ShaderLab.Primitives.SlFloat4;
using UV = SharpX.Library.ShaderLab.Primitives.SlFloat4;
using NormalizedUV = SharpX.Library.ShaderLab.Primitives.SlFloat2;

namespace NatsunekoLaboratory.SakuraShader.ScreenFX.Shader.Fragment
{
    [Export("frag-color.{extension}")]
    internal class ColorEffects
    {
        public static void ApplyChromaticAberration(ref Color color, NormalizedUV uv, SlFloat3 normal)
        {
            Compiler.AnnotatedStatement("unroll", () => { });
            for (SlInt i = 0; i < 2; i++)
            {
                SlInt sign = i == 0 ? 1 : -1;
                var rOffset = uv + new NormalizedUV(GlobalProperties.ChromaticAberrationRedOffsetX * sign, GlobalProperties.ChromaticAberrationRedOffsetY * sign);
                var gOffset = uv + new NormalizedUV(GlobalProperties.ChromaticAberrationGreenOffsetX * sign, GlobalProperties.ChromaticAberrationGreenOffsetY * sign);
                var bOffset = uv + new NormalizedUV(GlobalProperties.ChromaticAberrationBlueOffsetX * sign, GlobalProperties.ChromaticAberrationBlueOffsetY * sign);

                var r = Builtin.Tex2Dlod(GlobalProperties.GrabTexture, new UV(rOffset, 0, 0)).R;
                var g = Builtin.Tex2Dlod(GlobalProperties.GrabTexture, new UV(gOffset, 0, 0)).G;
                var b = Builtin.Tex2Dlod(GlobalProperties.GrabTexture, new UV(bOffset, 0, 0)).B;

                color += BuiltinOverride.Lerp(new UV(0, 0, 0, 0), new UV(r, g, b, 0), GlobalProperties.ChromaticAberrationWeight) * sign;
            }
        }

        public static void ApplyColorInverse(ref Color color)
        {
            var r = 1 - color.R;
            var g = 1 - color.G;
            var b = 1 - color.B;

            var inverse = new Color(r, g, b, color.A);

            color = BuiltinOverride.Lerp(color, inverse, GlobalProperties.ColorInverseWeight);
        }

        public static void ApplyGrayscale(ref Color color)
        {
            var grayscale = Builtin.Dot(color.RGB, new Color(0.2989f, 0.5870f, 0.1140f, color.A).RGB);
            color = BuiltinOverride.Lerp(color, new Color(grayscale, grayscale, grayscale, color.A), GlobalProperties.GrayscaleWeight);
        }

        // http://beesbuzz.biz/code/16-hsv-color-transforms
        public static void ApplyHueShift(ref Color color)
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

        public static void ApplySepiaColor(ref Color color)
        {
            var r = Builtin.Saturate(0.393f * color.R + 0.796f * color.G + 0.189f * color.B);
            var g = Builtin.Saturate(0.349f * color.R + 0.686f * color.G + 0.168f * color.B);
            var b = Builtin.Saturate(0.272f * color.R + 0.534f * color.G + 0.131f * color.B);

            var sepia = new Color(r, g, b, color.A);
            color = BuiltinOverride.Lerp(color, sepia, GlobalProperties.SepiaWeight);
        }

        // https://www.fbs.osaka-u.ac.jp/labs/ishijima/Photoshop-01.html
        public static void ApplyColorLayer(ref Color color, NormalizedUV uv)
        {
            var layer = GlobalProperties.LayerColor;

            Compiler.AnnotatedStatement("branch", () => { });
            switch (GlobalProperties.LayerBlendMode)
            {
                case LayerBlendMode.None:
                    break;

                case LayerBlendMode.Darken:
                {
                    var r = Builtin.Lerp(color.R, layer.R, Builtin.Step(layer.R, color.R));
                    var g = Builtin.Lerp(color.G, layer.G, Builtin.Step(layer.G, color.G));
                    var b = Builtin.Lerp(color.B, layer.B, Builtin.Step(layer.B, color.B));

                    color = Builtin.Saturate(new Color(r, g, b, color.A));
                    break;
                }

                case LayerBlendMode.Lighten:
                {
                    var r = Builtin.Lerp(color.R, layer.R, Builtin.Step(color.R, layer.R));
                    var g = Builtin.Lerp(color.G, layer.G, Builtin.Step(color.G, layer.G));
                    var b = Builtin.Lerp(color.B, layer.B, Builtin.Step(color.B, layer.B));

                    color = Builtin.Saturate(new Color(r, g, b, color.A));
                    break;
                }

                case LayerBlendMode.ColorDarken:
                    color = BuiltinOverride.Lerp(color, layer, Builtin.Step(layer.R + layer.G + layer.B, color.R + color.B + color.B));
                    break;

                case LayerBlendMode.ColorLighten:
                    color = BuiltinOverride.Lerp(layer, color, Builtin.Step(layer.R + layer.G + layer.B, color.R + color.B + color.B));
                    break;

                case LayerBlendMode.ColorBurn:
                {
                    var r = 1 - (1 - color.R) * layer.R;
                    var g = 1 - (1 - color.G) * layer.G;
                    var b = 1 - (1 - color.B) * layer.B;

                    color = Builtin.Saturate(new Color(r, g, b, color.A));
                    break;
                }

                case LayerBlendMode.LinearBurn:
                {
                    var r = Builtin.Lerp(0, color.R + layer.R - 1, Builtin.Step(1, color.R + layer.R));
                    var g = Builtin.Lerp(0, color.G + layer.G - 1, Builtin.Step(1, color.G + layer.G));
                    var b = Builtin.Lerp(0, color.B + layer.B - 1, Builtin.Step(1, color.B + layer.B));

                    color = Builtin.Saturate(new Color(r, g, b, color.A));
                    break;
                }

                case LayerBlendMode.Multiply:
                    color = Builtin.Saturate(color * layer);
                    break;

                case LayerBlendMode.Screen:
                    color = Builtin.Saturate(color + layer - color * layer);
                    break;

                case LayerBlendMode.ColorDodge:
                {
                    var r = color.R * (1 - layer.R);
                    var g = color.G * (1 - layer.G);
                    var b = color.B * (1 - layer.B);

                    color = Builtin.Saturate(new Color(r, g, b, color.A));
                    break;
                }

                case LayerBlendMode.LinearDodge:
                    color = Builtin.Saturate(color + layer);
                    break;

                case LayerBlendMode.Overlay:
                {
                    var r = Builtin.Lerp(color.R * layer.R * 2.0f, 1f - 2f * (1 - color.R) * (1 - layer.R), Builtin.Step(0.5f, color.R));
                    var g = Builtin.Lerp(color.G * layer.G * 2.0f, 1f - 2f * (1 - color.G) * (1 - layer.G), Builtin.Step(0.5f, color.G));
                    var b = Builtin.Lerp(color.B * layer.B * 2.0f, 1f - 2f * (1 - color.B) * (1 - layer.B), Builtin.Step(0.5f, color.B));

                    color = Builtin.Saturate(new Color(r, g, b, color.A));
                    break;
                }
            }
        }
    }
}
#endif