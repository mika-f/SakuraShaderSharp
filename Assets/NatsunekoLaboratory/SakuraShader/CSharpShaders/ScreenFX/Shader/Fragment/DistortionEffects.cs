#if SHARPX_COMPILER

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

    }
}
#endif