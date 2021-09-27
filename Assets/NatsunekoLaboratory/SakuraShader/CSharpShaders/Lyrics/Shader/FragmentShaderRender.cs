#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Primitives;

namespace NatsunekoLaboratory.SakuraShader.Lyrics.Shader
{
    [Export("frag-render")]
    public class FragmentShaderRender
    {
        private SlFloat4 GetNeighborPixel(NormalizedUV uv, SlFloat x, SlFloat y)
        {
            return Tex2D(ShaderProperties.GrabTextureModel, uv + new SlFloat2(x, y));
        }

        private void ApplyInverseColor(ref Color color)
        {
            var newColor = new Color(1, 1, 1, 0) - color;
            newColor.A = Abs(color.A);

            color = Lerp(color, newColor, ShaderProperties.InverseWeight);
        }

        private void ApplyOutline(Vertex2Fragment i, ref Color color)
        {
            var center = i.GrabPos.XY / i.GrabPos.W;
            var delta = 1.0f / ScreenParams.XY * ShaderProperties.OutlineWidth;
            var isEdge = 0;

            isEdge += Distance(GetNeighborPixel(center, delta.X * -1, delta.Y * +1), ShaderProperties.OutlineClearColor) >= 256000 ? 1 : 0;
            isEdge += Distance(GetNeighborPixel(center, delta.X * +0, delta.Y * +1), ShaderProperties.OutlineClearColor) >= 256000 ? 1 : 0;
            isEdge += Distance(GetNeighborPixel(center, delta.X * +1, delta.Y * +1), ShaderProperties.OutlineClearColor) >= 256000 ? 1 : 0;
            isEdge += Distance(GetNeighborPixel(center, delta.X * -1, delta.Y * +0), ShaderProperties.OutlineClearColor) >= 256000 ? 1 : 0;
            isEdge += Distance(GetNeighborPixel(center, delta.X * +1, delta.Y * +0), ShaderProperties.OutlineClearColor) >= 256000 ? 1 : 0;
            isEdge += Distance(GetNeighborPixel(center, delta.X * -1, delta.Y * -1), ShaderProperties.OutlineClearColor) >= 256000 ? 1 : 0;
            isEdge += Distance(GetNeighborPixel(center, delta.X * +0, delta.Y * -1), ShaderProperties.OutlineClearColor) >= 256000 ? 1 : 0;
            isEdge += Distance(GetNeighborPixel(center, delta.X * +1, delta.Y * -1), ShaderProperties.OutlineClearColor) >= 256000 ? 1 : 0;

            if (ShaderProperties.IsOutlineRenderEdgeOnly)
            {
                var grab = Tex2D(ShaderProperties.GrabTextureWorld, center);
                color = Lerp(grab, ShaderProperties.OutlineColor, isEdge > 0 ? 1f : 0f);
                return;
            }

            color= Lerp(color, ShaderProperties.OutlineColor, isEdge > 0 ? 1f : 0f);
        }

        [FragmentShader]
        [return: Semantic("SV_Target")]
        public Color FragmentRenderMain(Vertex2Fragment i)
        {
            var color = Tex2D(ShaderProperties.MainTexture, i.TexCoord) * ShaderProperties.MainColor;
            color.A *= ShaderProperties.AlphaTransparency;

            if (ShaderProperties.IsEnableInverseColor)
                ApplyInverseColor(ref color);

            if (ShaderProperties.IsEnableOutline)
                ApplyOutline(i, ref color);

            return color;
        }
    }
}

#endif