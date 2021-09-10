#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;

namespace NatsunekoLaboratory.SakuraShader.Particles.Shader
{
    [Export("frag")]
    public class FragmentShader
    {
        [FragmentShader]
        [return: Semantic("SV_Target")]
        public static Color FragmentMain(Vertex2Fragment i)
        {
            var color = Tex2D(ShaderProperties.MainTexture, i.TexCoord) * ShaderProperties.MainColor * i.Color;
            color.A *= ShaderProperties.AlphaTransparency;

            if (ShaderProperties.IsEnableEmission)
            {
                var emission = Tex2D(ShaderProperties.EmissionTexture, i.TexCoord) * ShaderProperties.EmissionColor;
                color.RGB += emission.RGB;
            }

            return color;
        }
    }
}

#endif