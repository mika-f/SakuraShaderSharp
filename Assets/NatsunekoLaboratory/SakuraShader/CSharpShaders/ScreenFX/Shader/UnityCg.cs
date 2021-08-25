#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Primitives;

namespace NatsunekoLaboratory.SakuraShader.ScreenFX.Shader
{
    [External]
    [Include("UnityCG.cginc")]
    internal static class UnityCg
    {
        [External]
        public static extern SlFloat4 UnityObjectToClipPos(SlFloat4 a);

        [External]
        public static extern SlFloat3 UnityObjectToWorldNormal(SlFloat3 a);

        [External]
        [Function("TRANSFORM_TEX")]
        public static extern SlFloat2 TransformTexture(SlFloat4 a, Sampler2D b);
    }
}

#endif