#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;

namespace NatsunekoLaboratory.SakuraShader.Particles.Shader
{
    [Export("vert")]
    public class VertexShader
    {
        [VertexShader]
        public static Vertex2Fragment VertexMain(AppData i)
        {
            return new Vertex2Fragment
            {
                Vertex = UnityCg.UnityObjectToClipPos(i.Position),
                TexCoord = UnityCg.TransformTexture(i.TexCoord, ShaderProperties.MainTexture),
                Color = i.Color
            };
        }
    }
}

#endif