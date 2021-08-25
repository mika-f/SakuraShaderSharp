#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Functions;
using SharpX.Library.ShaderLab.Predefined;
using SharpX.Library.ShaderLab.Primitives;

namespace NatsunekoLaboratory.SakuraShader.ScreenFX.Shader
{
    [Export("vert.{extension}")]
    internal class VertexShader
    {
        [VertexShader]
        public static Vertex2Fragment Vertex(AppDataFull i)
        {
            return new Vertex2Fragment
            {
                Vertex = UnityCg.UnityObjectToClipPos(i.Vertex),
                Normal = UnityCg.UnityObjectToWorldNormal(i.Normal),
                TexCoord = UnityCg.TransformTexture(i.TexCoord, GlobalProperties.MainTexture),
                WorldPos = Builtin.Mul<SlFloat3>(UnityInjection.ObjectToWorld, i.Vertex),
                LocalPos = i.Vertex.XYZ
            };
        }
    }
}

#endif