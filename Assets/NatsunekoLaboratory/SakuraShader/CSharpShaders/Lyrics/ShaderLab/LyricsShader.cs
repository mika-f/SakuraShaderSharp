#if SHARPX_COMPILER

using System.Collections.Immutable;

using NatsunekoLaboratory.SakuraShader.Lyrics.Shader;

using SharpX.Library.ShaderLab.Abstractions;
using SharpX.Library.ShaderLab.Attributes;

namespace NatsunekoLaboratory.SakuraShader.Lyrics.ShaderLab
{
    [Export("Lyrics")]
    public class LyricsShader : ShaderLabDefinition
    {
        private static readonly ImmutableArray<SubShaderDefinition> Shaders = ImmutableArray.Create<SubShaderDefinition>(new LyricsShaderLodNone());

        public LyricsShader() : base("NatsunekoLaboratory/Sakura Shader/Lyrics", typeof(ShaderProperties), Shaders)
        {
            CustomEditor = typeof(LyricsInspector);
        }
    }
}

namespace NatsunekoLaboratory.SakuraShader
{
    // ReSharper disable once InconsistentNaming
    internal class LyricsInspector { }
}

#endif