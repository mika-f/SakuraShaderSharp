#if SHARPX_COMPILER

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
#else
            return Tex2D(GlobalProperties.MainTexture, i.UV) * GlobalProperties.MainColor;
#endif
        }
    }
}

#endif