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

            if (GlobalProperties.IsEnableCinemascope)
                color = ApplyCinemascope(i, color);

            return color;
        }
    }
}

#endif