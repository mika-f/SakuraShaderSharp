#if SHARPX_COMPILER

using NatsunekoLaboratory.SakuraShader.Avatars.Effects.Common;

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Primitives;

namespace NatsunekoLaboratory.SakuraShader.Avatars.Effects.Shader
{
    [Export("frag")]
    internal class FragmentShader
    {
        [FragmentShader]
        [return: Semantic("SV_Target")]
        public static Color FragmentMain(Geometry2Fragment i)
        {
#if SHADER_GEOMETRY_HOLOGRAPH
            return new SlFloat4((Tex2D(GlobalProperties.MainTexture, i.UV) * GlobalProperties.MainColor).XYZ, GlobalProperties.HolographAlphaTransparency);
#elif SHADER_GEOMETRY_VOXEL
            var viewDirection = Normalize(WorldSpaceCameraPos - Mul<SlFloat3>(ObjectToWorld, i.LocalPos).XYZ);
            var rim = 1.0f - Abs(Dot(viewDirection, i.Normal));
            var rimColor = GlobalProperties.RimLightingColor * Pow(rim, GlobalProperties.RimLightingPower) * GlobalProperties.RimLightingPower;
            var color = GlobalProperties.VoxelUvSamplingSource == UvSamplingSource.ShaderProperty ? GlobalProperties.VoxelColor : Tex2D(GlobalProperties.MainTexture, i.UV) * GlobalProperties.MainColor;
            color.RGB += rimColor * (GlobalProperties.IsEnableRimLighting ? 1 : 0);

            return color;

#else
            var viewDirection = Normalize(WorldSpaceCameraPos - Mul<SlFloat3>(ObjectToWorld, i.LocalPos).XYZ);
            var rim = 1.0f - Abs(Dot(viewDirection, i.Normal));
            var rimColor = GlobalProperties.RimLightingColor * Pow(rim, GlobalProperties.RimLightingPower) * GlobalProperties.RimLightingPower;
            var color = Tex2D(GlobalProperties.MainTexture, i.UV) * GlobalProperties.MainColor;
            color.RGB += rimColor * (GlobalProperties.IsEnableRimLighting ? 1 : 0);

            return color;
#endif
        }
    }
}

#endif