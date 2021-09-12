#if SHARPX_COMPILER

using NatsunekoLaboratory.SakuraShader.ScreenFX.Shader.Fragment;

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Primitives;
using SharpX.Library.ShaderLab.Statements;

namespace NatsunekoLaboratory.SakuraShader.ScreenFX.Shader
{
    [Export("frag")]
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

            if (ShaderProperties.IsEnableScreenRotation)
                DistortionEffects.ApplyScreenRotation(ref normalized);

            if (ShaderProperties.IsEnableScreenMovement)
                DistortionEffects.ApplyScreenMovement(ref normalized);

            if (ShaderProperties.IsEnableScreenTransform)
                DistortionEffects.ApplyScreenTransform(ref normalized);

            if (ShaderProperties.IsEnablePixelation)
                DistortionEffects.ApplyPixelation(ref normalized);

            if (ShaderProperties.IsEnableCheckerboard)
                DistortionEffects.ApplyCheckerboard(ref normalized);

            var color = Tex2Dlod(ShaderProperties.GrabTexture, new Color(Saturate(normalized), 0, 0));


            if (ShaderProperties.IsEnableChromaticAberration)
                ColorEffects.ApplyChromaticAberration(ref color, normalized);

            if (ShaderProperties.IsEnableColorInverse)
                ColorEffects.ApplyColorInverse(ref color);

            if (ShaderProperties.IsEnableGrayscale)
                ColorEffects.ApplyGrayscale(ref color);

            if (ShaderProperties.IsEnableHueShift)
                ColorEffects.ApplyHueShift(ref color);

            if (ShaderProperties.IsEnableSepiaColor)
                ColorEffects.ApplySepiaColor(ref color);

            if (ShaderProperties.IsEnableColorLayer)
                ColorEffects.ApplyColorLayer(ref color, uv);

            if (ShaderProperties.IsEnableNoise)
                OverlayEffects.ApplyNoise(ref color, uv);

            if (ShaderProperties.IsEnableGirlsCam)
                OverlayEffects.ApplyGirlsCam(ref color, uv);

            if (ShaderProperties.IsEnableGlitch)
                OverlayEffects.ApplyGlitch(ref color, uv);

            if (ShaderProperties.IsEnableBlur)
                OverlayEffects.ApplyBlur(ref color, uv);

            if (ShaderProperties.IsEnableColoredCheckerboard)
                OverlayEffects.ApplyColoredCheckerboard(ref color, uv);

            if (ShaderProperties.IsEnableImageOverlay)
                OverlayEffects.ApplyImageOverlay(ref color, uv);

            if (ShaderProperties.IsEnableStageCurtain)
                OverlayEffects.ApplyStageCurtain(ref color, uv);

            if (ShaderProperties.IsEnableCinemascope)
                OverlayEffects.ApplyCinemascope(i, ref color);

            return color;
        }
    }
}

#endif