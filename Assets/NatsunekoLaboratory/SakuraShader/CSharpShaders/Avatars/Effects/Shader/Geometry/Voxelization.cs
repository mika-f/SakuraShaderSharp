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

        private static SlBool CalcIsInHiddenArea(SlFloat3 localPos)
        {
            var range = GlobalProperties.VoxelBoundaryRange / 2;
            var boundary = new SlFloat3(GlobalProperties.VoxelBoundaryX, GlobalProperties.VoxelBoundaryY, GlobalProperties.VoxelBoundaryZ);
            if (GlobalProperties.VoxelBoundaryOperator == BoundaryOperator.GreaterThan)
            {
                SlBool isInX = (boundary.X - range) < localPos.X;
                SlBool isInY = (boundary.Y - range) < localPos.Y;
                SlBool isInZ = (boundary.Z - range) < localPos.Z;

                return isInX || isInY || isInZ;
            }

            if (GlobalProperties.VoxelBoundaryOperator == BoundaryOperator.LessThan)
            {
                SlBool isInX = (boundary.X + range) < localPos.X;
                SlBool isInY = (boundary.Y + range) < localPos.Y;
                SlBool isInZ = (boundary.Z + range) < localPos.Z;

                return isInX || isInY || isInZ;
            }

            if (GlobalProperties.VoxelBoundaryOperator == BoundaryOperator.Between)
            {
                SlBool isInX = (boundary.X - range) < localPos.X && (boundary.X + range) < localPos.X;
                SlBool isInY = (boundary.Y - range) < localPos.Y && (boundary.Y + range) < localPos.Y;
                SlBool isInZ = (boundary.Z - range) < localPos.Z && (boundary.Z + range) < localPos.Z;

                return isInX || isInY || isInZ;
            }

            if (GlobalProperties.VoxelBoundaryOperator == BoundaryOperator.OutOfBetween)
            {
                SlBool isInX = (boundary.X - range) > localPos.X && (boundary.X + range) > localPos.X;
                SlBool isInY = (boundary.Y - range) > localPos.Y && (boundary.Y + range) > localPos.Y;
                SlBool isInZ = (boundary.Z - range) > localPos.Z && (boundary.Z + range) > localPos.Z;

                return isInX || isInY || isInZ;
            }

            return 1 == 1;

        }

        private static SlBool CalcIsInBoundaryRange(SlFloat3 localPos)
        {
            var range = GlobalProperties.VoxelBoundaryRange / 2;
            var boundary = new SlFloat3(GlobalProperties.VoxelBoundaryX, GlobalProperties.VoxelBoundaryY, GlobalProperties.VoxelBoundaryZ);
            SlBool isInX = (boundary.X - range) < localPos.X && localPos.X < (boundary.X + range);
            SlBool isInY = (boundary.Y - range) < localPos.Y && localPos.Y < (boundary.Y + range);
            SlBool isInZ = (boundary.Z - range) < localPos.Z && localPos.Z < (boundary.Z + range);

            return isInX || isInY || isInZ;
        }

        private static SlFloat CalcBoundaryFixed(SlFloat3 localPos)
        {
            var range = GlobalProperties.VoxelBoundaryRange / 2;
            var boundary = new SlFloat3(GlobalProperties.VoxelBoundaryX, GlobalProperties.VoxelBoundaryY, GlobalProperties.VoxelBoundaryZ);
            var x = Abs(boundary.X - range - localPos.X);
            var y = Abs(boundary.Y - range - localPos.Z);
            var z = Abs(boundary.Z - range - localPos.Y);

            return Min(Min(x, y), z) * GlobalProperties.VoxelBoundaryFactor;
        }

        private static SlFloat3 CalcOffsetMovedVertexForVoxelization(SlFloat3 vertex, Normal normal)
        {
            var offset = Mul<SlFloat3>(ObjectToWorld, new SlFloat4(GlobalProperties.VoxelOffset.XYZ, 0)).XYZ;
            var x = vertex.X + offset.X + normal.X * GlobalProperties.VoxelOffset.W;
            var y = vertex.Y + offset.Y + normal.Y * GlobalProperties.VoxelOffset.W;
            var z = vertex.Z + offset.Z + normal.Z * GlobalProperties.VoxelOffset.W;

            return new SlFloat3(x, y, z);
        }

        private static Geometry2Fragment GetStreamDataForVoxelization(SlFloat3 vertex, Normal normal, NormalizedUV uv, Normal originalNormal)
        {
            var newVertex = CalcOffsetMovedVertexForVoxelization(vertex, originalNormal);

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

            if (GlobalProperties.IsEnableVoxelBoundary)
            {
                var localPosCenter = (i[0].LocalPos + i[1].LocalPos + i[2].LocalPos) / 3;
                var isInBoundaryRange = CalcIsInBoundaryRange(localPosCenter);
                var isInBoundaryHidden = CalcIsInHiddenArea(localPosCenter);
                var boundaryFixedValue = Lerp(1, CalcBoundaryFixed(localPosCenter), isInBoundaryRange ? 1 : 0) * (isInBoundaryHidden ? 0 : 1);

                sizeX *= boundaryFixedValue;
                sizeY *= boundaryFixedValue;
                sizeZ *= boundaryFixedValue;
            }

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