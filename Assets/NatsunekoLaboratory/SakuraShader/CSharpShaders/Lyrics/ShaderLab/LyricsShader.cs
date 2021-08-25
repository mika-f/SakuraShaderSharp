#if SHARPX_COMPILER

using System;
using System.Collections.Immutable;

using SharpX.Library.ShaderLab.Abstractions;

namespace NatsunekoLaboratory.SakuraShader.Lyrics.ShaderLab
{
    public class LyricsShader : ShaderLabDefinition
    {
        public LyricsShader(Type properties, ImmutableArray<SubShaderDefinition> subShaders) : base("NatsunekoLaboratory/Sakura Shader/Lyrics", properties, subShaders) { }
    }
}

#endif