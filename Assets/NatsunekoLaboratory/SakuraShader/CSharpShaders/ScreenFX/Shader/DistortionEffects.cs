﻿#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Functions;

using Color = SharpX.Library.ShaderLab.Primitives.SlFloat4;
using UV = SharpX.Library.ShaderLab.Primitives.SlFloat4;
using NormalizedUV = SharpX.Library.ShaderLab.Primitives.SlFloat2;

namespace NatsunekoLaboratory.SakuraShader.ScreenFX.Shader
{
    [Export("frag-distortion.{extension}")]
    internal static class DistortionEffects
    {
        public static void ApplyMelt(ref Color color, Vertex2Fragment i, NormalizedUV uv)
        {
            var r = Utilities.RotateByAngle(uv, GlobalProperties.MeltAngle);
            var s = Builtin.Sin(uv.X / GlobalProperties.MeltInterval);

            color = s > 0 ? Builtin.Tex2Dlod(GlobalProperties.GrabTexture, new Color(r, 0, 0)) : color;
        }

        public static void ApplyScreenMovement(ref Color color, NormalizedUV @base)
        {
            var x = @base.X + GlobalProperties.ScreenMovementX;
            var y = @base.Y + GlobalProperties.ScreenMovementY;

            var center = new NormalizedUV(0.5f + GlobalProperties.ScreenMovementX, 0.5f + GlobalProperties.ScreenMovementY);
            var uv = (new NormalizedUV(x, y) - center) * (1 - GlobalProperties.ScreenMovementZ) + center;

            color = Builtin.Tex2D(GlobalProperties.GrabTexture, uv);
        }
    }
}
#endif