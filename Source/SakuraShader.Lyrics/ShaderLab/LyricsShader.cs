using System;
using System.Collections.Immutable;

using SakuraShader.Lyrics.Shader;

using SharpX.Library.ShaderLab.Abstractions;
using SharpX.Library.ShaderLab.Attributes;

namespace SakuraShader.Lyrics.ShaderLab
{
    [Export("Lyrics.{extension}")]
    internal class LyricsShader : ShaderLabDefinition
    {
        private static readonly ImmutableArray<SubShaderDefinition> Shaders = ImmutableArray.Create<SubShaderDefinition>(new LyricsSubShaderLodAll());

        public LyricsShader() : base("NatsunekoLaboratory/Sakura Shader/Lyrics", typeof(Globals), Shaders) { }
    }
}
