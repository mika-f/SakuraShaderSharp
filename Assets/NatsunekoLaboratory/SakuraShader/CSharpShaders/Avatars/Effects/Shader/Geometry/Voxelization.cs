#if SHARPX_COMPILER

using NatsunekoLaboratory.SakuraShader.Avatars.Effects.Common;

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Interfaces;
using SharpX.Library.ShaderLab.Primitives;

namespace NatsunekoLaboratory.SakuraShader.Avatars.Effects.Shader.Geometry
{
    [Export("geom-voxel")]
    internal class Voxelization
    {
        private static NormalizedUV GetTexcoordFromVertexes([ArrayInput(3)] Vertex2Geometry[] i)
        {
            if (GlobalProperties.VoxelUvSamplingSource == UvSamplingSource.ShaderProperty)
                return i[0].TexCoord;
            if (GlobalProperties.VoxelUvSamplingSource == UvSamplingSource.Center)
                return (i[0].TexCoord + i[1].TexCoord + i[2].TexCoord) / 3f;
            if (GlobalProperties.VoxelUvSamplingSource == UvSamplingSource.First)
                return i[0].TexCoord;
            if (GlobalProperties.VoxelUvSamplingSource == UvSamplingSource.Second)
                return i[1].TexCoord;
            if (GlobalProperties.VoxelUvSamplingSource == UvSamplingSource.Last)
                return i[2].TexCoord;

            return new(0, 0);
        }

        private static SlFloat CalcMaxDistanceBetween(SlFloat a, SlFloat b, SlFloat c)
        {
            if (GlobalProperties.VoxelSource == VoxelSource.VertexShader)
                return Max(Max(Distance(a, b), Distance(b, c)), Distance(a, c));

            return GlobalProperties.VoxelSize;
        }

        private static SlFloat3 CalcNormal(SlFloat3 a, SlFloat3 b, SlFloat3 c)
        {
            return Normalize(Cross(b - a, c - a));
        }

        private static SlFloat3 CalcVertex(SlFloat3 center, SlFloat x, SlFloat y, SlFloat z)
        {
            return center + Mul<SlFloat3>(ObjectToWorld, new SlFloat4(x, y, z, 0)).XYZ;
        }

        private static SlFloat3 CalcOffsetMovedVertex(SlFloat3 vertex, Normal normal)
        {
            var offset = Mul<SlFloat3>(ObjectToWorld, new SlFloat4(GlobalProperties.VoxelOffset.XYZ, 0)).XYZ;
            var x = vertex.X + offset.X + normal.X * GlobalProperties.VoxelOffset.W;
            var y = vertex.Y + offset.Y + normal.Y * GlobalProperties.VoxelOffset.W;
            var z = vertex.Z + offset.Z + normal.Z * GlobalProperties.VoxelOffset.W;

            return new SlFloat3(x, y, z);
        }

        private static Geometry2Fragment GetStreamDataForVoxelization(SlFloat3 vertex, Normal normal, NormalizedUV uv, Normal originalNormal)
        {
            var newVertex = CalcOffsetMovedVertex(vertex, originalNormal);

            return new Geometry2Fragment
            {
                Position = UnityCg.UnityWorldToClipPos(newVertex),
                Normal = normal,
                UV = uv,
                WorldPos = newVertex,
                LocalPos = Mul<SlFloat3>(WorldToObject, new SlFloat4(newVertex, 1))
            };
        }

        public static void ApplyVoxelization([ArrayInput(3)] Vertex2Geometry[] i, ref ITriangleStream<Geometry2Fragment> stream)
        {
            var worldPos1 = i[0].WorldPos.XYZ;
            var worldPos2 = i[1].WorldPos.XYZ;
            var worldPos3 = i[2].WorldPos.XYZ;
            var worldPosCenter = (worldPos1 + worldPos2 + worldPos3) / 3;

            var texCoord = GetTexcoordFromVertexes(i);
            var normal = CalcNormal(worldPos1, worldPos2, worldPos3);

            var sizeX = CalcMaxDistanceBetween(worldPos1.X, worldPos2.X, worldPos3.X) / 2;
            var sizeY = CalcMaxDistanceBetween(worldPos1.Y, worldPos2.Y, worldPos3.Y) / 2;
            var sizeZ = CalcMaxDistanceBetween(worldPos1.Z, worldPos2.Z, worldPos3.Z) / 2;

            // top
            {
                var a = CalcVertex(worldPosCenter, sizeX, sizeY, sizeZ);
                var b = CalcVertex(worldPosCenter, sizeX, sizeY, -sizeZ);
                var c = CalcVertex(worldPosCenter, -sizeX, sizeY, sizeZ);
                var d = CalcVertex(worldPosCenter, -sizeX, sizeY, -sizeZ);

                var n = CalcNormal(a, b, c);

                stream.Append(GetStreamDataForVoxelization(a, n, texCoord, normal));
                stream.Append(GetStreamDataForVoxelization(b, n, texCoord, normal));
                stream.Append(GetStreamDataForVoxelization(c, n, texCoord, normal));
                stream.Append(GetStreamDataForVoxelization(d, n, texCoord, normal));
                stream.RestartStrip();
            }

            // bottom
            {
                var a = CalcVertex(worldPosCenter, sizeX, -sizeY, sizeZ);
                var b = CalcVertex(worldPosCenter, -sizeX, -sizeY, sizeZ);
                var c = CalcVertex(worldPosCenter, sizeX, -sizeY, -sizeZ);
                var d = CalcVertex(worldPosCenter, -sizeX, -sizeY, -sizeZ);

                var n = CalcNormal(a, b, c);

                stream.Append(GetStreamDataForVoxelization(a, n, texCoord, normal));
                stream.Append(GetStreamDataForVoxelization(b, n, texCoord, normal));
                stream.Append(GetStreamDataForVoxelization(c, n, texCoord, normal));
                stream.Append(GetStreamDataForVoxelization(d, n, texCoord, normal));
                stream.RestartStrip();
            }

            // left
            {
                var a = CalcVertex(worldPosCenter, sizeX, sizeY, sizeZ);
                var b = CalcVertex(worldPosCenter, sizeX, -sizeY, sizeZ);
                var c = CalcVertex(worldPosCenter, sizeX, sizeY, -sizeZ);
                var d = CalcVertex(worldPosCenter, sizeX, -sizeY, -sizeZ);

                var n = CalcNormal(a, b, c);

                stream.Append(GetStreamDataForVoxelization(a, n, texCoord, normal));
                stream.Append(GetStreamDataForVoxelization(b, n, texCoord, normal));
                stream.Append(GetStreamDataForVoxelization(c, n, texCoord, normal));
                stream.Append(GetStreamDataForVoxelization(d, n, texCoord, normal));
                stream.RestartStrip();
            }

            // right
            {
                var a = CalcVertex(worldPosCenter, -sizeX, sizeY, sizeZ);
                var b = CalcVertex(worldPosCenter, -sizeX, sizeY, -sizeZ);
                var c = CalcVertex(worldPosCenter, -sizeX, -sizeY, sizeZ);
                var d = CalcVertex(worldPosCenter, -sizeX, -sizeY, -sizeZ);

                var n = CalcNormal(a, b, c);

                stream.Append(GetStreamDataForVoxelization(a, n, texCoord, normal));
                stream.Append(GetStreamDataForVoxelization(b, n, texCoord, normal));
                stream.Append(GetStreamDataForVoxelization(c, n, texCoord, normal));
                stream.Append(GetStreamDataForVoxelization(d, n, texCoord, normal));
                stream.RestartStrip();
            }

            // foreground
            {
                var a = CalcVertex(worldPosCenter, sizeX, sizeY, sizeZ);
                var b = CalcVertex(worldPosCenter, -sizeX, sizeY, sizeZ);
                var c = CalcVertex(worldPosCenter, sizeX, -sizeY, sizeZ);
                var d = CalcVertex(worldPosCenter, -sizeX, -sizeY, sizeZ);

                var n = CalcNormal(a, b, c);

                stream.Append(GetStreamDataForVoxelization(a, n, texCoord, normal));
                stream.Append(GetStreamDataForVoxelization(b, n, texCoord, normal));
                stream.Append(GetStreamDataForVoxelization(c, n, texCoord, normal));
                stream.Append(GetStreamDataForVoxelization(d, n, texCoord, normal));
                stream.RestartStrip();
            }

            // background
            {
                var a = CalcVertex(worldPosCenter, sizeX, sizeY, -sizeZ);
                var b = CalcVertex(worldPosCenter, sizeX, -sizeY, -sizeZ);
                var c = CalcVertex(worldPosCenter, -sizeX, sizeY, -sizeZ);
                var d = CalcVertex(worldPosCenter, -sizeX, -sizeY, -sizeZ);

                var n = CalcNormal(a, b, c);

                stream.Append(GetStreamDataForVoxelization(a, n, texCoord, normal));
                stream.Append(GetStreamDataForVoxelization(b, n, texCoord, normal));
                stream.Append(GetStreamDataForVoxelization(c, n, texCoord, normal));
                stream.Append(GetStreamDataForVoxelization(d, n, texCoord, normal));
                stream.RestartStrip();
            }
        }
    }
}

#endif