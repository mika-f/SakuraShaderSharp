#if SHARPX_COMPILER

using NatsunekoLaboratory.SakuraShader.Avatars.Effects.Shader.Geometry;

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Interfaces;
using SharpX.Library.ShaderLab.Primitives;
using SharpX.Library.ShaderLab.Statements;

namespace NatsunekoLaboratory.SakuraShader.Avatars.Effects.Shader
{
    [Export("geom")]
    internal class GeometryShader
    {
        private static Geometry2Fragment CreateStandardVertex(Vertex2Geometry i)
        {
            return new Geometry2Fragment
            {
                Position = UnityCg.UnityWorldToClipPos(i.WorldPos.XYZ),
                UV = i.TexCoord,
                Normal = i.Normal,
                WorldPos = i.WorldPos.XYZ,
                LocalPos = Mul<SlFloat3>(WorldToObject, i.WorldPos)
            };
        }

        [GeometryShader]
        [MaxVertexCount(32)]
        public static void GeometryMain([InputPrimitive(InputPrimitiveAttribute.InputPrimitives.Triangle)] Vertex2Geometry[] i, ref ITriangleStream<Geometry2Fragment> stream)
        {
            if (GlobalProperties.IsEnableVoxelization)
            {
                Voxelization.ApplyVoxelization(i, ref stream);
                return;
            }

            Compiler.AnnotatedStatement("unroll", () => { });
            for (SlInt j = 0; j < 3; j++)
            {
                var v2g = i[j];
                stream.Append(CreateStandardVertex(v2g));
            }
            stream.RestartStrip();
        }
    }
}

#endif