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

        public static void ApplyScreenRotation(ref NormalizedUV uv)
        {
            var ratio = Utilities.GetAspectRatio();

            // roll
            var offsetRoll = new NormalizedUV(0.5f * ratio, 0.5f);
            uv = offsetRoll + Utilities.RotateByAngle(new NormalizedUV(uv.X * ratio, uv.Y) - offsetRoll, Builtin.Radians(GlobalProperties.ScreenRotationRoll));
            uv.X /= ratio;
        }

        public static void ApplyScreenTransform(ref NormalizedUV uv)
        {
            var center = new NormalizedUV(0.5f , 0.5f );
            uv = (uv - center) * new NormalizedUV(1 - GlobalProperties.TransformHorizontal, 1 - GlobalProperties.TransformVertical) + center;
        }

        public static void ApplyPixelation(ref NormalizedUV uv)
        {
            var pixelation = new NormalizedUV((128 - GlobalProperties.PixelationWidth) * Utilities.GetAspectRatio(), 128 - GlobalProperties.PixelationHeight);
            uv = Builtin.Floor(uv * pixelation) / pixelation;
        }

        public static void ApplyCheckerboard(ref NormalizedUV uv)
        {
            var rotate = Utilities.RotateByAngle(new NormalizedUV(uv.X * Utilities.GetAspectRatio(), uv.Y), Builtin.Radians(GlobalProperties.CheckerboardAngle));
            var cols = Builtin.Floor(rotate.X * (100 - GlobalProperties.CheckerboardWidth * 100));
            var rows = Builtin.Floor(rotate.Y * (100 - GlobalProperties.CheckerboardHeight * 100));

            var offset = new NormalizedUV(1 / (100 - GlobalProperties.CheckerboardWidth * 100), 1 / (100 - GlobalProperties.CheckerboardHeight * 100));

            uv.X = Builtin.Lerp(uv.X, uv.X + offset.X * GlobalProperties.CheckerboardOffset, Utilities.IsEquals(Builtin.Fmod(cols + rows, 2), 0));
        }
    }
}
#endif