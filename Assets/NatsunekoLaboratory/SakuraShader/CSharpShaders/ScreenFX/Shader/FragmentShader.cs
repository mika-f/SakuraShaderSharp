#if SHARPX_COMPILER

using NatsunekoLaboratory.SakuraShader.ScreenFX.Shader.Fragment;

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
            var raw = i.GrabScreenPos;

            var uv = ComputeStereoScreenUV(i);

            var normalized = raw.XY / raw.W;

            if (GlobalProperties.IsEnableScreenMovement)
                DistortionEffects.ApplyScreenMovement(ref normalized);

            var color = Builtin.Tex2Dlod(GlobalProperties.GrabTexture, new Color(Builtin.Saturate(normalized), 0, 0));


            if (GlobalProperties.IsEnableChromaticAberration)
                ColorEffects.ApplyChromaticAberration(ref color, normalized, i.Normal);

            if (GlobalProperties.IsEnableColorInverse)
                ColorEffects.ApplyColorInverse(ref color);

            if (GlobalProperties.IsEnableGrayscale)
                ColorEffects.ApplyGrayscale(ref color);

            if (GlobalProperties.IsEnableHueShift)
                ColorEffects.ApplyHueShift(ref color);

            if (GlobalProperties.IsEnableSepiaColor)
                ColorEffects.ApplySepiaColor(ref color);

            if (GlobalProperties.IsEnableColorLayer)
                ColorEffects.ApplyColorLayer(ref color, uv);

            if (GlobalProperties.IsEnableNoise)
                SpecialEffects.ApplyNoise(ref color, uv);

            if (GlobalProperties.IsEnableGirlsCam)
                SpecialEffects.ApplyGirlsCam(ref color, uv);

            if (GlobalProperties.IsEnableCinemascope)
                SpecialEffects.ApplyCinemascope(i, ref color);

            return color;
        }
    }
}

#endif