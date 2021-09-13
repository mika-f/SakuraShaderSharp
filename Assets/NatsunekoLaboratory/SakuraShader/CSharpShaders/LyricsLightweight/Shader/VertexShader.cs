#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Predefined;

namespace NatsunekoLaboratory.SakuraShader.LyricsLightweight.Shader
{
    [Export("vert")]
    public class VertexShader
    {
        [VertexShader]
        public Vertex2Fragment VertexMain(AppDataFull i)
        {
            var vertex = UnityCg.UnityObjectToClipPos(i.Vertex);

            return new Vertex2Fragment
            {
                Vertex = vertex,
                TexCoord = UnityCg.TransformTexture(i.TexCoord, ShaderProperties.MainTexture),
                LocalPos = i.Vertex.XYZ
            };
        }
    }
}

#endif