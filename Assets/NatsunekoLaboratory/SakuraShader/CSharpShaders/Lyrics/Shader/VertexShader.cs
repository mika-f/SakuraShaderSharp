#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Predefined;

namespace NatsunekoLaboratory.SakuraShader.Lyrics.Shader
{
    [Export("vert")]
    public class VertexShader
    {
        [VertexShader]
        public Vertex2Fragment VertexMain(AppDataFull i)
        {
            return new Vertex2Fragment
            {
                Vertex = UnityCg.UnityObjectToClipPos(i.Vertex),
                TexCoord = UnityCg.TransformTexture(i.TexCoord, ShaderProperties.MainTexture),
                LocalPos = i.Vertex.XYZ
            };
        }
    }
}

#endif