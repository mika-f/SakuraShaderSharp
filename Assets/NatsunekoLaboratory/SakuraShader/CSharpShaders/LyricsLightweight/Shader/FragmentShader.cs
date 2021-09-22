#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;

namespace NatsunekoLaboratory.SakuraShader.LyricsLightweight.Shader
{
    [Export("frag")]
    public class FragmentShader
    {
        [FragmentShader]
        [return: Semantic("SV_Target")]
        public Color FragmentRenderMain(Vertex2Fragment i)
        {
            var color = Tex2D(ShaderProperties.MainTexture, i.TexCoord) * ShaderProperties.MainColor;
            color.A *= ShaderProperties.AlphaTransparency;

            return color;
        }
    }
}

#endif