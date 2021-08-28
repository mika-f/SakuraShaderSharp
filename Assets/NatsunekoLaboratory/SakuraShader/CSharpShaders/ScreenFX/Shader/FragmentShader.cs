﻿#if SHARPX_COMPILER

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
            var @base = i.GrabScreenPos.XYZ / i.GrabScreenPos.W;

            var color = Builtin.Tex2Dlod(GlobalProperties.GrabTexture, new Color(@base, 0));
            var uv = ComputeStereoScreenUV(i);

            if (GlobalProperties.IsEnableScreenMovement)
                DistortionEffects.ApplyScreenMovement(ref color, @base.XY);

            if (GlobalProperties.IsEnableMelt) DistortionEffects.ApplyMelt(ref color, i, @base.XY);

            if (GlobalProperties.IsEnableChromaticAberration)
                ColorEffects.ApplyChromaticAberration(ref color, @base.XY, i.Normal);

            if (GlobalProperties.IsEnableColorInverse)
                ColorEffects.ApplyColorInverse(ref color);

            if (GlobalProperties.IsEnableGrayscale)
                ColorEffects.ApplyGrayscale(ref color);

            if (GlobalProperties.IsEnableHueShift)
                ColorEffects.ApplyHueShift(ref color);

            if (GlobalProperties.IsEnableSepiaColor)
                ColorEffects.ApplySepiaColor(ref color);

            if (GlobalProperties.IsEnableNoise)
                SpecialEffects.ApplyNoise(ref color, uv);

            if (GlobalProperties.IsEnableCinemascope)
                SpecialEffects.ApplyCinemascope(i, ref color);

            return color;
        }
    }
}

#endif