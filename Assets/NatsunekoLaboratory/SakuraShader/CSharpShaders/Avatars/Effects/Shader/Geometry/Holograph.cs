#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Interfaces;
using SharpX.Library.ShaderLab.Primitives;
using SharpX.Library.ShaderLab.Statements;

namespace NatsunekoLaboratory.SakuraShader.Avatars.Effects.Shader.Geometry
{
    [Export("geom-holograph")]
    internal class Holograph
    {
        private static SlFloat3 CalcOffsetMovedVertexForHolograph(SlFloat3 vertex, Normal normal, SlFloat3 localPos)
        {
            var wave = Time.X - Floor(Time.X) - 0.5f; // sawtooth wave
            var diff = -localPos.Y - wave;
            var height = 0 <= diff && diff <= 0.1 ? Cos((diff - 0.05f) * 30f) * GlobalProperties.HolographHeight : 0;
            var multiply = height < 0 ? 0 : height;

            return new SlFloat3(vertex.X + normal.X * multiply, vertex.Y + normal.Y * multiply, vertex.Z + normal.Z * multiply);
        }

        private static Geometry2Fragment GetStreamDataForHolograph(SlFloat3 worldPos, SlFloat3 localPos, Normal normal, NormalizedUV uv)
        {
            var newVertex = CalcOffsetMovedVertexForHolograph(worldPos, normal, localPos);

            return new()
            {
                Position = UnityCg.UnityWorldToClipPos(newVertex),
                Normal = normal,
                UV = uv,
                WorldPos = newVertex,
                LocalPos = Mul<SlFloat3>(WorldToObject, new SlFloat4(newVertex, 1.0f)),
            };
        }

        public static void ApplyHolograph([ArrayInput(3)] Vertex2Geometry[] i, ref ITriangleStream<Geometry2Fragment> stream)
        {
            Compiler.AnnotatedStatement("unroll", () => {});

            var normal = (i[0].Normal + i[1].Normal + i[2].Normal) / 3;
            var center = (i[0].LocalPos + i[1].LocalPos + i[2].LocalPos) / 3;

            for (SlInt j = 0; j < 3; j++)
            {
                var worldPos = i[j].WorldPos.XYZ;
                var texCoord = i[j].TexCoord;

                stream.Append(GetStreamDataForHolograph(worldPos, center, normal, texCoord));
            }

            stream.RestartStrip();
        }
    }
}

#endif