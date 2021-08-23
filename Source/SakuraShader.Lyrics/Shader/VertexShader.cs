using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Functions;
using SharpX.Library.ShaderLab.Predefined;
using SharpX.Library.ShaderLab.Primitives;

namespace SakuraShader.Lyrics.Shader
{

    [Export("vert.{extension}")]
    internal class VertexShader
    {
        public SlFloat4 RotateByDegree(SlFloat4 vertex, SlFloat degree)
        {
            var rot = degree * UnityCG.PI / 180.0f;
            var s = Builtin.Sin(rot);
            var c = Builtin.Cos(rot);
            var m = new SlFloat2x2(c, -s, s, c);

            return new SlFloat4(Builtin.Mul<SlFloat2>(m, vertex.XZ), vertex.YZ).XYZW;
        }

        [VertexShader]
        [Function("vs")]
        public Vertex2Fragment VertexMain(AppDataFull v)
        {
            var o = new Vertex2Fragment { };

            var normal = Builtin.Normalize(Builtin.Mul<SlFloat3>((SlFloat3x3)UnityCG.UnityMatrixITMV, v.Vertex.XYZ));
            var offset = UnityCG.TransformViewToProjection(normal.XY);

            if (Globals.IsRotationZEnabled)
            {
                o.Vertex = RotateByDegree(v.Vertex, Globals.RotationZAngle);
                o.Vertex = UnityCG.UnityObjectToClipPos(o.Vertex);
            }
            else
            {
                o.Vertex = UnityCG.UnityObjectToClipPos(v.Vertex);
            }

#if SHADER_OUTLINE
            if (Globals.IsOutlineEnabled)
            {
#if SHADER_OUTLINE_REVERSE
            o.Vertex.XZ -= offset * Globals.OutlineWidth;
#else
                o.Vertex.XY -= offset * Globals.OutlineWidth;
#endif
            }
#endif

            o.TexCoord = UnityCG.TransformTexture(v.TexCoord, Globals.MainTexture);
            o.LocalPos = v.Vertex.XYZ;

            return o;
        }
    }
}