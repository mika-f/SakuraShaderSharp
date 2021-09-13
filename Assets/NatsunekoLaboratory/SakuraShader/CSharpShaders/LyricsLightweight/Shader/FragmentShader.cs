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
            return Tex2D(ShaderProperties.MainTexture, i.TexCoord) * ShaderProperties.MainColor;
        }
    }
}

#endif