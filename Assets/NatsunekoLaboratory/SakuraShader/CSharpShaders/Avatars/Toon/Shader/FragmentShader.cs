#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Primitives;

namespace NatsunekoLaboratory.SakuraShader.Avatars.Toon.Shader
{
    [Export("frag")]
    internal static class FragmentShader
    {
        private static void ApplyRimLighting(ref Color color, Normal normal, SlFloat3 viewDirection)
        {
            var rim = 1.0f - Abs(Dot(viewDirection, normal));
            var rimColor = GlobalProperties.RimLightingColor * Pow(rim, GlobalProperties.RimLightingPower) * GlobalProperties.RimLightingPower;

            color.RGB += rimColor;
        }

        [FragmentShader]
        [return: Semantic("SV_Target")]
        public static SlFloat4 Fragment(Vertex2Fragment i)
        {
            var baseColor = Tex2D(GlobalProperties.MainTexture, i.TexCoord);
            var viewDirection = Normalize(WorldSpaceCameraPos - Mul<SlFloat3>(ObjectToWorld, i.LocalPos).XYZ);

            if (GlobalProperties.IsEnableRimLighting)
                ApplyRimLighting(ref baseColor, i.Normal, viewDirection);

            return baseColor;
        }
    }
}

#endif