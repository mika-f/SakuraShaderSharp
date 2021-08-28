﻿#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Functions;

using RawUV = SharpX.Library.ShaderLab.Primitives.SlFloat4;
using NormalizedUV = SharpX.Library.ShaderLab.Primitives.SlFloat2;

namespace NatsunekoLaboratory.SakuraShader.ScreenFX.Shader.Fragment
{
    [Export("frag-distortion.{extension}")]
    internal static class DistortionEffects
    {
        public static void ApplyScreenMovement(ref NormalizedUV uv)
        {
            var x = uv.X + GlobalProperties.ScreenMovementX;
            var y = uv.Y + GlobalProperties.ScreenMovementY;

            var center = new NormalizedUV(0.5f + GlobalProperties.ScreenMovementX, 0.5f + GlobalProperties.ScreenMovementY);
            uv = (new NormalizedUV(x, y) - center) * (1 - GlobalProperties.ScreenMovementZ) + center;
        }

        public static void ApplyScreenRotation(ref RawUV uv)
        {
            var offset = new NormalizedUV(0.5f, 0.5f) * uv.W;
            uv.XY = offset + Utilities.RotateByAngle(uv.XY - offset, Builtin.Radians(GlobalProperties.ScreenRotationRoll));
        }

        public static void ApplyScreenTransform(ref NormalizedUV uv)
        {
            var center = new NormalizedUV(0.5f , 0.5f );
            uv = (uv - center) * new NormalizedUV(1 - GlobalProperties.TransformHorizontal, 1 - GlobalProperties.TransformVertical) + center;
        }
    }
}
#endif