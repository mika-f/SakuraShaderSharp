#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Predefined;
using SharpX.Library.ShaderLab.Primitives;

namespace NatsunekoLaboratory.SakuraShader.Avatars.Effects.Shader
{
    [Export("vert")]
    internal class VertexShader
    {
        [VertexShader]
        public static Vertex2Geometry VertexMain(AppDataFull i)
        {
            return new Vertex2Geometry
            {
                Vertex = UnityCg.UnityObjectToClipPos(i.Vertex),
                Normal = UnityCg.UnityObjectToWorldNormal(i.Normal),
                TexCoord = UnityCg.TransformTexture(i.TexCoord, GlobalProperties.MainTexture),
                WorldPos = Mul<SlFloat4>(ObjectToWorld, i.Vertex),
                LocalPos = i.Vertex.XYZ
            };
        }
    }
}

#endif