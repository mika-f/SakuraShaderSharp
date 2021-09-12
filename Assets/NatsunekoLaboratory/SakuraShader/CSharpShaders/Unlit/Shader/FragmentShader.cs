using SharpX.Library.ShaderLab.Attributes;

#if SHARPX_COMPILER

namespace NatsunekoLaboratory.SakuraShader.Unlit.Shader
{
    [Export("frag")]
    public class FragmentShader
    {
        [FragmentShader]
        [return: Semantic("SV_Target")]
        public Color FragmentMain(Vertex2Fragment i)
        {
            return Tex2D(ShaderProperties.MainTexture, i.TexCoord) * ShaderProperties.MainColor;
        }
    }
}

#endif