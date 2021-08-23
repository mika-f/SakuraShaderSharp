using SakuraShader.Lyrics.Shader;

using SharpX.Library.ShaderLab.Abstractions;

namespace SakuraShader.Lyrics.ShaderLab
{
    internal class LyricsShaderStencil : StencilDefinition
    {
        public LyricsShaderStencil()
        {
            Ref = $"[{nameof(Globals.StencilRef)}]";
            Compare = $"[{nameof(Globals.StencilCompare)}]";
            Pass = $"[{nameof(Globals.StencilPass)}]";
        }
    }
}