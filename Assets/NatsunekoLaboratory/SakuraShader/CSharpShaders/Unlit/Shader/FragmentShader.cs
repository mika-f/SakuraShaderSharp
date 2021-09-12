#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;

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