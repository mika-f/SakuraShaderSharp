#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Functions;

using Color = SharpX.Library.ShaderLab.Primitives.SlFloat4;

namespace NatsunekoLaboratory.SakuraShader.ScreenFX.Shader
{
    [Export("frag.{extension}")]
    internal class FragmentShader
    {
        private static Color ApplyGrayscale(Color color)
        {
            var grayscale = Builtin.Dot(color.RGB, new Color(0.2989f, 0.5870f, 0.1140f, color.A).RGB);
            return BuiltinOverride.Lerp(color, new Color(grayscale, grayscale, grayscale, color.A), GlobalProperties.GrayscaleWeight);
        }

        // http://beesbuzz.biz/code/16-hsv-color-transforms
        private static Color ApplyHueShift(Color color)
        {
            var v = GlobalProperties.BrightnessValue;
            var vsu = v * GlobalProperties.SaturationValue * Builtin.Cos(GlobalProperties.HueShiftValue * UnityCg.PI / 180f);
            var vsw = v * GlobalProperties.SaturationValue * Builtin.Sin(GlobalProperties.HueShiftValue * UnityCg.PI / 180f);

            var r = new Color(0, 0, 0, color.A);
            r.R = (0.299f * v + 0.701f * vsu + 0.168f * vsw) * color.R + (0.587f * v - 0.587f * vsu + 0.330f * vsw) * color.G + (0.114f * v - 0.114f * vsu - 0.497f * vsw) * color.B;
            r.G = (0.299f * v - 0.299f * vsu - 0.328f * vsw) * color.R + (0.587f * v + 0.413f * vsu + 0.035f * vsw) * color.G + (0.114f * v - 0.114f * vsu - 0.292f * vsw) * color.B;
            r.B = (0.299f * v - 0.300f * vsu + 1.250f * vsw) * color.R + (0.587f * v - 0.588f * vsu - 1.050f * vsw) * color.G + (0.114f * v + 0.886f * vsu - 0.203f * vsw) * color.B;

            return BuiltinOverride.Lerp(color, r, GlobalProperties.HueShiftWeight);
        }

        private static Color ApplySepiaColor(Color color)
        {
            var r = Builtin.Saturate(0.393f * color.R + 0.796f * color.G + 0.189f * color.B);
            var g = Builtin.Saturate(0.349f * color.R + 0.686f * color.G + 0.168f * color.B);
            var b = Builtin.Saturate(0.272f * color.R + 0.534f * color.G + 0.131f * color.B);

            var sepia = new Color(r, g, b, color.A);
            return BuiltinOverride.Lerp(color, sepia, GlobalProperties.SepiaWeight);
        }

        private static Color ApplyCinemascope(Vertex2Fragment i, Color color)
        {
            var height = UnityInjection.ScreenParams.Y / 2.0f * GlobalProperties.CinemascopeWidth;
            var tPixels = UnityInjection.ScreenParams.Y - height;
            var bPixels = height;

            var nApplied = color;
            var bApplied = BuiltinOverride.Lerp(nApplied, GlobalProperties.CinemascopeColor, Builtin.Step(i.Vertex.Y, bPixels));
            var tApplied = BuiltinOverride.Lerp(bApplied, GlobalProperties.CinemascopeColor, 1 - Builtin.Step(i.Vertex.Y, tPixels));

            return tApplied;
        }


        [FragmentShader]
        [return: Semantic("SV_TARGET")]
        public static Color Fragment(Vertex2Fragment i)
        {
            var color = Builtin.Tex2D(GlobalProperties.GrabTexture, i.GrabScreenPos.XY);

            if (GlobalProperties.IsEnableGrayscale)
                color = ApplyGrayscale(color);

            if (GlobalProperties.IsEnableHueShift)
                color = ApplyHueShift(color);

            if (GlobalProperties.IsEnableSepiaColor)
                color = ApplySepiaColor(color);

            if (GlobalProperties.IsEnableCinemascope)
                color = ApplyCinemascope(i, color);

            return color;
        }
    }
}

#endif