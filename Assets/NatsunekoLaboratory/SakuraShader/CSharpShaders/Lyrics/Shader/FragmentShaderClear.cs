#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;

namespace NatsunekoLaboratory.SakuraShader.Lyrics.Shader
{
    [Export("frag-clear")]
    public class FragmentShaderClear
    {
        // outline is based on https://qiita.com/yoship1639/items/ec323111ade3a77671e0
        [FragmentShader]
        [return:Semantic("SV_Target")]
        public static Color FragmentClearMain(Vertex2Fragment i)
        {
            return ShaderProperties.OutlineClearColor;
        }
    }
}

#endif