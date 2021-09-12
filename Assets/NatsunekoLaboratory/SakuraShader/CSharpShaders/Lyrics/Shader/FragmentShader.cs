#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;

namespace NatsunekoLaboratory.SakuraShader.Lyrics.Shader
{
    [Export("frag")]
    public class FragmentShader
    {
        [FragmentShader]
        [return:Semantic("SV_Target")]
        public static Color FragmentMain(Vertex2Fragment i)
        {
            return Tex2D(ShaderProperties.MainTexture, i.TexCoord) * ShaderProperties.MainColor;
        }
    }
}

#endif