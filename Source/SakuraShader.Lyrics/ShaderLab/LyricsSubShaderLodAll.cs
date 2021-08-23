using System.Collections.Generic;
using System.Collections.Immutable;

using SharpX.Library.ShaderLab.Abstractions;

namespace SakuraShader.Lyrics.ShaderLab
{
    internal class LyricsSubShaderLodAll : SubShaderDefinition
    {
        private static readonly ImmutableArray<ShaderPassDefinition> Passes = ImmutableArray.Create<ShaderPassDefinition>(new LyricsShaderOutlinePass(), new LyricsShaderOutlineReversePass(), new LyricsShaderPass());

        public LyricsSubShaderLodAll() : base(Passes)
        {
            Tags = new Dictionary<string, string>
            {
                { "RenderType", "Opaque" },
                { "Queue", "Geometry" },
                { "IgnoreProjector", "True" },
                { "DisableBatching", "True" }
            }.ToImmutableDictionary();
        }
    }
}