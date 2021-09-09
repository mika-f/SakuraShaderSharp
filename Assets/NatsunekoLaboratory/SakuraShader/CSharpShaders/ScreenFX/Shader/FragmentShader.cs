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

            if (GlobalProperties.IsEnableScreenRotation)
                DistortionEffects.ApplyScreenRotation(ref normalized);

            if (GlobalProperties.IsEnableScreenMovement)
                DistortionEffects.ApplyScreenMovement(ref normalized);

            if (GlobalProperties.IsEnableScreenTransform)
                DistortionEffects.ApplyScreenTransform(ref normalized);

            if (GlobalProperties.IsEnablePixelation)
                DistortionEffects.ApplyPixelation(ref normalized);

            if (GlobalProperties.IsEnableCheckerboard)
                DistortionEffects.ApplyCheckerboard(ref normalized);

            var color = Tex2Dlod(GlobalProperties.GrabTexture, new Color(Saturate(normalized), 0, 0));


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
                OverlayEffects.ApplyNoise(ref color, uv);

            if (GlobalProperties.IsEnableGirlsCam)
                OverlayEffects.ApplyGirlsCam(ref color, uv);

            if (GlobalProperties.IsEnableGlitch)
                OverlayEffects.ApplyGlitch(ref color, uv);

            if (GlobalProperties.IsEnableBlur)
                OverlayEffects.ApplyBlur(ref color, uv);

            if (GlobalProperties.IsEnableColoredCheckerboard)
                OverlayEffects.ApplyColoredCheckerboard(ref color, uv);

            if (GlobalProperties.IsEnableImageOverlay)
                OverlayEffects.ApplyImageOverlay(ref color, uv);

            if (GlobalProperties.IsEnableStageCurtain)
                OverlayEffects.ApplyStageCurtain(ref color, uv);

            if (GlobalProperties.IsEnableCinemascope)
                OverlayEffects.ApplyCinemascope(i, ref color);

            return color;
        }
    }
}

#endif