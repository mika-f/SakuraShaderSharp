#if SHARPX_COMPILER

using NatsunekoLaboratory.SakuraShader.ScreenFX.Enums;

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Primitives;
using SharpX.Library.ShaderLab.Statements;

namespace NatsunekoLaboratory.SakuraShader.ScreenFX.Shader.Fragment
{
    [Export("frag-color")]
    internal class ColorEffects
    {
        public static void ApplyChromaticAberration(ref Color color, NormalizedUV uv)
        {
            Compiler.AnnotatedStatement("unroll", () => { });
            for (var i = 0; i < 2; i++)
            {
                var sign = i == 0 ? 1 : -1;
                var rOffset = uv + new NormalizedUV(ShaderProperties.ChromaticAberrationRedOffsetX * sign, ShaderProperties.ChromaticAberrationRedOffsetY * sign);
                var gOffset = uv + new NormalizedUV(ShaderProperties.ChromaticAberrationGreenOffsetX * sign, ShaderProperties.ChromaticAberrationGreenOffsetY * sign);
                var bOffset = uv + new NormalizedUV(ShaderProperties.ChromaticAberrationBlueOffsetX * sign, ShaderProperties.ChromaticAberrationBlueOffsetY * sign);

                var r = Tex2Dlod(ShaderProperties.GrabTexture, new UV(rOffset, 0, 0)).R;
                var g = Tex2Dlod(ShaderProperties.GrabTexture, new UV(gOffset, 0, 0)).G;
                var b = Tex2Dlod(ShaderProperties.GrabTexture, new UV(bOffset, 0, 0)).B;

                color += BuiltinOverride.Lerp(new UV(0, 0, 0, 0), new UV(r, g, b, 0), ShaderProperties.ChromaticAberrationWeight) * sign;
            }
        }

        public static void ApplyColorInverse(ref Color color)
        {
            var r = 1 - color.R;
            var g = 1 - color.G;
            var b = 1 - color.B;

            var inverse = new Color(r, g, b, color.A);

            color = BuiltinOverride.Lerp(color, inverse, ShaderProperties.ColorInverseWeight);
        }

        public static void ApplyGrayscale(ref Color color)
        {
            var grayscale = Dot(color.RGB, new Color(0.2989f, 0.5870f, 0.1140f, color.A).RGB);
            color = BuiltinOverride.Lerp(color, new Color(grayscale, grayscale, grayscale, color.A), ShaderProperties.GrayscaleWeight);
        }

        // http://beesbuzz.biz/code/16-hsv-color-transforms
        public static void ApplyHueShift(ref Color color)
        {
            var v = ShaderProperties.BrightnessValue;
            var vsu = v * ShaderProperties.SaturationValue * Cos(ShaderProperties.HueShiftValue * UnityCg.PI / 180f);
            var vsw = v * ShaderProperties.SaturationValue * Sin(ShaderProperties.HueShiftValue * UnityCg.PI / 180f);

            var r = new Color(0, 0, 0, color.A);
            r.R = (0.299f * v + 0.701f * vsu + 0.168f * vsw) * color.R + (0.587f * v - 0.587f * vsu + 0.330f * vsw) * color.G + (0.114f * v - 0.114f * vsu - 0.497f * vsw) * color.B;
            r.G = (0.299f * v - 0.299f * vsu - 0.328f * vsw) * color.R + (0.587f * v + 0.413f * vsu + 0.035f * vsw) * color.G + (0.114f * v - 0.114f * vsu - 0.292f * vsw) * color.B;
            r.B = (0.299f * v - 0.300f * vsu + 1.250f * vsw) * color.R + (0.587f * v - 0.588f * vsu - 1.050f * vsw) * color.G + (0.114f * v + 0.886f * vsu - 0.203f * vsw) * color.B;

            color = BuiltinOverride.Lerp(color, r, ShaderProperties.HueShiftWeight);
        }

        public static void ApplySepiaColor(ref Color color)
        {
            var r = Saturate(0.393f * color.R + 0.796f * color.G + 0.189f * color.B);
            var g = Saturate(0.349f * color.R + 0.686f * color.G + 0.168f * color.B);
            var b = Saturate(0.272f * color.R + 0.534f * color.G + 0.131f * color.B);

            var sepia = new Color(r, g, b, color.A);
            color = BuiltinOverride.Lerp(color, sepia, ShaderProperties.SepiaWeight);
        }

        // https://www.fbs.osaka-u.ac.jp/labs/ishijima/Photoshop-01.html
        public static void ApplyColorLayer(ref Color color, NormalizedUV i)
        {
            var layer = ShaderProperties.LayerColor;
            var weight = Lerp(1, ShaderProperties.ColorLayerWeight, ShaderProperties.IsEnableColorLayerPartially ? 1 : 0);
            var candidateColor = color;

            Compiler.AnnotatedStatement("branch", () => { });
            switch (ShaderProperties.LayerBlendMode)
            {
                case LayerBlendMode.None:
                {
                    candidateColor = layer;
                    break;
                }

                case LayerBlendMode.Darken:
                {
                    candidateColor = Saturate(new Color(Min(color, layer).RGB, color.A));
                    break;
                }

                case LayerBlendMode.Lighten:
                {
                    candidateColor = Saturate(new Color(Max(color, layer).RGB, color.A));
                    break;
                }

                case LayerBlendMode.ColorDarken:
                    candidateColor = BuiltinOverride.Lerp(color, layer, Step(layer.R + layer.G + layer.B, color.R + color.B + color.B));
                    break;

                case LayerBlendMode.ColorLighten:
                    candidateColor = BuiltinOverride.Lerp(layer, color, Step(layer.R + layer.G + layer.B, color.R + color.B + color.B));
                    break;

                case LayerBlendMode.ColorBurn:
                {
                    var r = 1 - (1 - color.R) * layer.R;
                    var g = 1 - (1 - color.G) * layer.G;
                    var b = 1 - (1 - color.B) * layer.B;

                    candidateColor = Saturate(new Color(r, g, b, color.A));
                    break;
                }

                case LayerBlendMode.LinearBurn:
                {
                    var r = Lerp(0, color.R + layer.R - 1, Step(1, color.R + layer.R));
                    var g = Lerp(0, color.G + layer.G - 1, Step(1, color.G + layer.G));
                    var b = Lerp(0, color.B + layer.B - 1, Step(1, color.B + layer.B));

                    candidateColor = Saturate(new Color(r, g, b, color.A));
                    break;
                }

                case LayerBlendMode.Divide:
                    candidateColor = Saturate(color / layer);
                    break;

                case LayerBlendMode.Multiply:
                    candidateColor = Saturate(color * layer);
                    break;

                case LayerBlendMode.Subtract:
                    candidateColor = Saturate(layer - color);
                    break;

                case LayerBlendMode.Difference:
                    candidateColor = Saturate(Abs(color - layer));
                    break;

                case LayerBlendMode.Screen:
                    candidateColor = Saturate(color + layer - color * layer);
                    break;

                case LayerBlendMode.ColorDodge:
                {
                    var r = color.R * (1 - layer.R);
                    var g = color.G * (1 - layer.G);
                    var b = color.B * (1 - layer.B);

                    candidateColor = Saturate(new Color(r, g, b, color.A));
                    break;
                }

                case LayerBlendMode.LinearDodge:
                    candidateColor = Saturate(color + layer);
                    break;

                case LayerBlendMode.Overlay:
                {
                    var r = Lerp(color.R * layer.R * 2.0f, 1f - 2f * (1 - color.R) * (1 - layer.R), Step(0.5f, color.R));
                    var g = Lerp(color.G * layer.G * 2.0f, 1f - 2f * (1 - color.G) * (1 - layer.G), Step(0.5f, color.G));
                    var b = Lerp(color.B * layer.B * 2.0f, 1f - 2f * (1 - color.B) * (1 - layer.B), Step(0.5f, color.B));

                    candidateColor = Saturate(new Color(r, g, b, color.A));
                    break;
                }
            }

            if (ShaderProperties.IsEnableColorLayerPartially)
            {
                var edge = new SlFloat2(1, 1);

                if (ShaderProperties.ColorLayerDirection == LayerDirection.TopToBottom)
                    edge.Y = Utilities.GreaterThanOrEquals(i.Y, 1 - weight);
                else if (ShaderProperties.ColorLayerDirection == LayerDirection.BottomToTop)
                    edge.Y = Utilities.LessThanOrEquals(i.Y, weight);
                else if (ShaderProperties.ColorLayerDirection == LayerDirection.RightToLeft)
                    edge.X = Utilities.GreaterThanOrEquals(i.X, 1 - weight);
                else if (ShaderProperties.ColorLayerDirection == LayerDirection.LeftToRight)
                    edge.X = Utilities.LessThanOrEquals(i.X, weight);

                color = BuiltinOverride.Lerp(color, candidateColor, Utilities.IsEquals21(edge, new SlFloat2(1, 1)));
            }
            else
            {
                color = BuiltinOverride.Lerp(color, candidateColor, ShaderProperties.ColorLayerWeight);
            }
        }
    }
}

#endif