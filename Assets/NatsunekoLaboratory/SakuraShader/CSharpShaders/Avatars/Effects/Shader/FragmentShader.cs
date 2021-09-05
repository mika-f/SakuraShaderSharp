#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;

namespace NatsunekoLaboratory.SakuraShader.Avatars.Effects.Shader
{
    [Export("frag")]
    internal class FragmentShader
    {
        [FragmentShader]
        [return: Semantic("SV_Target")]
        public static Color FragmentMain(Geometry2Fragment i)
        {
            var color = Tex2D(GlobalProperties.MainTexture, i.UV) * GlobalProperties.MainColor;
            return color;
        }
    }
}

#endif