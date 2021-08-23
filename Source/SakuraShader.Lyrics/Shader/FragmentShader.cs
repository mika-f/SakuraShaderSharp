using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Functions;
using SharpX.Library.ShaderLab.Primitives;

namespace SakuraShader.Lyrics.Shader
{
    [Export("frag.{extension}")]
    internal class FragmentShader
    {
        public SlFloat4 SampleTexture(SlFloat2 uv)
        {
            if (Globals.IsAnimationEnabled)
            {
                SlBool a = Builtin.Floor(Globals.Time.Y / Globals.AnimationUpdateRate) % 2 == 0;
                return Globals.Color * (a ? Builtin.Tex2D(Globals.MainTexture, uv) : Builtin.Tex2D(Globals.AnimationSpriteTexture, uv));
            }

            return Globals.Color * Builtin.Tex2D(Globals.MainTexture, uv);
        }

        [Function("vs")]
        [return: Semantic("SV_TARGET")]
        public SlFloat4 FragmentMain(Vertex2Fragment i)
        {
#if SHADER_OUTLINE
            var color = Globals.OutlineColor * Builtin.Tex2D(Globals.OutlineTexture, i.TexCoord);
#else
            var color = SampleTexture(i.TexCoord);
#endif

            if (Globals.IsSlideInvisibleEnabled)
            {
                if (Globals.SlideVisibleFrom == ClippingMode.Left)
                    Builtin.Clip(Builtin.Lerp(-1, 1, Builtin.Step(i.LocalPos.X + 0.5f - Globals.SlideVisibleWidth, 0f)));
                else if (Globals.SlideVisibleFrom == ClippingMode.Center)
                    Builtin.Clip(Builtin.Lerp(-1, 1, Builtin.Step(Builtin.Abs(i.LocalPos.X) - Globals.SlideVisibleWidth * 0.5f, 0f)));
                else if (Globals.SlideVisibleFrom == ClippingMode.Right)
                    Builtin.Clip(i.LocalPos.X - 0.5f + Globals.SlideVisibleWidth);
                else if (Globals.SlideVisibleFrom == ClippingMode.Top)
                    Builtin.Clip(i.LocalPos.Y - 0.5f * Globals.SlideVisibleWidth);
                else if (Globals.SlideVisibleFrom == ClippingMode.Bottom)
                    Builtin.Clip(Builtin.Lerp(-1, 1, Builtin.Step(i.LocalPos.Y + 0.5f - Globals.SlideVisibleWidth, 0f)));
            }

            return color;
        }
    }
}