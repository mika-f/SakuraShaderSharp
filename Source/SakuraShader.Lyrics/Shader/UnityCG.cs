using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Primitives;

namespace SakuraShader.Lyrics.Shader
{
    [Include("UnityCG.cginc")]
    public static class UnityCG
    {
        [External]
        [GlobalMember]
        [Property("UNITY_MATRIX_IT_MV")]
        public static object UnityMatrixITMV { get; }

        [External]
        [GlobalMember]
        [Property("UNITY_PI")]
        public static SlFloat PI { get; }

        [External]
        public static extern SlFloat2 TransformViewToProjection(SlFloat2 _);

        [External]
        public static extern SlFloat4 UnityObjectToClipPos(SlFloat4 _);

        [External]
        [Function("TRANSFORM_TEX")]
        public static extern SlFloat2 TransformTexture(SlFloat4 a, Sampler2D b);

    }
}