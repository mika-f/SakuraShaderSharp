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

        [FragmentShader]
        [return: Semantic("SV_Target")]
        public Color FragmentRenderMain(Vertex2Fragment i)
        {
            var color = Tex2D(ShaderProperties.MainTexture, i.TexCoord) * ShaderProperties.MainColor;
            color.A *= ShaderProperties.AlphaTransparency;

            if (ShaderProperties.IsEnableOutline)
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
                    return Lerp(grab, ShaderProperties.OutlineColor, isEdge > 0 ? 1f : 0f);
                }

                return Lerp(color, ShaderProperties.OutlineColor, isEdge > 0 ? 1f : 0f);
            }

            return color;
        }
    }
}

#endif