#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;

namespace NatsunekoLaboratory.SakuraShader.ScreenFX.Shader.Fragment
{
    [Export("frag-distortion")]
    internal static class DistortionEffects
    {
        public static void ApplyScreenMovement(ref NormalizedUV uv)
        {
            var x = uv.X + ShaderProperties.ScreenMovementX;
            var y = uv.Y + ShaderProperties.ScreenMovementY;

            var center = new NormalizedUV(0.5f + ShaderProperties.ScreenMovementX, 0.5f + ShaderProperties.ScreenMovementY);
            uv = (new NormalizedUV(x, y) - center) * (1 - ShaderProperties.ScreenMovementZ) + center;
        }

        public static void ApplyScreenRotation(ref NormalizedUV uv)
        {
            var ratio = Utilities.GetAspectRatio();

            // roll
            var offsetRoll = new NormalizedUV(0.5f * ratio, 0.5f);
            uv = offsetRoll + Utilities.RotateByAngle(new NormalizedUV(uv.X * ratio, uv.Y) - offsetRoll, Radians(ShaderProperties.ScreenRotationRoll));
            uv.X /= ratio;
        }

        public static void ApplyScreenTransform(ref NormalizedUV uv)
        {
            var center = new NormalizedUV(0.5f, 0.5f);
            uv = (uv - center) * new NormalizedUV(1 - ShaderProperties.TransformHorizontal, 1 - ShaderProperties.TransformVertical) + center;
        }

        public static void ApplyPixelation(ref NormalizedUV uv)
        {
            var pixelation = new NormalizedUV((128 - ShaderProperties.PixelationWidth) * Utilities.GetAspectRatio(), 128 - ShaderProperties.PixelationHeight);
            uv = Floor(uv * pixelation) / pixelation;
        }

        public static void ApplyCheckerboard(ref NormalizedUV uv)
        {
            var rotate = Utilities.RotateByAngle(new NormalizedUV(uv.X * Utilities.GetAspectRatio(), uv.Y), Radians(ShaderProperties.CheckerboardAngle));
            var cols = Floor(rotate.X * (100 - ShaderProperties.CheckerboardWidth * 100));
            var rows = Floor(rotate.Y * (100 - ShaderProperties.CheckerboardHeight * 100));

            var offset = new NormalizedUV(1 / (100 - ShaderProperties.CheckerboardWidth * 100), 1 / (100 - ShaderProperties.CheckerboardHeight * 100));

            uv.X = Lerp(uv.X, uv.X + offset.X * ShaderProperties.CheckerboardOffset, Utilities.IsEquals(Fmod(cols + rows, 2), 0));
        }
    }
}
#endif