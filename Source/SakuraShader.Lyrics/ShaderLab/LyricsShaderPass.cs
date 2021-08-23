using System;
using System.Collections.Generic;
using System.Collections.Immutable;

using SakuraShader.Lyrics.Shader;

using SharpX.Library.ShaderLab.Abstractions;
using SharpX.Library.ShaderLab.Enums;

namespace SakuraShader.Lyrics.ShaderLab
{
    internal class LyricsShaderPass : ShaderPassDefinition
    {
        private static readonly ImmutableDictionary<string, string> OutlinePragmas = new Dictionary<string, string>
        {
            { "target", "4.5" },
            { "vertex", "vs" },
            { "fragment", "fs" }
        }.ToImmutableDictionary();

        private static readonly ImmutableArray<Type> Shaders = ImmutableArray.Create(typeof(Vertex2Fragment), typeof(VertexShader), typeof(FragmentShader));

        public LyricsShaderPass() : base(OutlinePragmas, Shaders)
        {
            Name = "Lyrics Outline 2";
            ShaderVariant = "normal";
            Blend = "SrcAlpha OneMinusSrcAlpha";
            Cull = $"[{nameof(Globals.Culling)}]";
            ZWrite = $"[{nameof(Globals.ZWrite)}]";
            Stencil = new LyricsShaderStencil();
        }
    }
}