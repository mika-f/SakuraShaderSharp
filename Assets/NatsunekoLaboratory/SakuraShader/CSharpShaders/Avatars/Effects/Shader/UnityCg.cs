#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Primitives;

namespace NatsunekoLaboratory.SakuraShader.Avatars.Effects.Shader
{
    [External]
    [Include("UnityCG.cginc")]
    internal static class UnityCg
    {
        [External]
        [Property("UNITY_PI")]
        public static SlFloat PI { get; }

        [External]
        [Property("unity_StereoScaleOffset")]
        public static SlFloat4[] StereoScaleOffset { get; }

        [External]
        [Property("unity_StereoEyeIndex")]
        public static SlInt StereoEyeIndex { get; }

        [External]
        public static extern SlFloat4 UnityObjectToClipPos(SlFloat4 a);

        [External]
        public static extern SlFloat4 UnityWorldToClipPos(SlFloat3 a);

        [External]
        public static extern SlFloat3 UnityObjectToWorldNormal(SlFloat3 a);

        [External]
        [Function("TRANSFORM_TEX")]
        public static extern SlFloat2 TransformTexture(SlFloat4 a, Sampler2D b);

        [External]
        public static extern SlFloat4 ComputeScreenPos(SlFloat4 a);

        [External]
        public static extern SlFloat4 ComputeGrabScreenPos(SlFloat4 a);
    }
}

#endif