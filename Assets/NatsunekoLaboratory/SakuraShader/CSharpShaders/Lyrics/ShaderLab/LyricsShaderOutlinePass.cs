﻿#if SHARPX_COMPILER

using System;
using System.Collections.Generic;
using System.Collections.Immutable;

using NatsunekoLaboratory.SakuraShader.Lyrics.Shader;

using SharpX.Library.ShaderLab.Abstractions;
using SharpX.Library.ShaderLab.Enums;

namespace NatsunekoLaboratory.SakuraShader.Lyrics.ShaderLab
{
    public class LyricsShaderOutlinePass : ShaderPassDefinition
    {
        private static readonly ImmutableDictionary<string, string> ShaderPragmas = new Dictionary<string, string>
        {
            { "target", "4.5" },
            { "vertex", Configuration.GetShaderEntryPoint(typeof(VertexShader), Configuration.EntryPoint.VertexShader) },
            { "fragment", Configuration.GetShaderEntryPoint(typeof(FragmentShaderRender), Configuration.EntryPoint.FragmentShader) }
        }.ToImmutableDictionary();

        private static readonly ImmutableArray<Type> Shaders = ImmutableArray.Create(typeof(Vertex2Fragment), typeof(VertexShader), typeof(FragmentShaderRender));

        public LyricsShaderOutlinePass() : base(ShaderPragmas, Shaders)
        {
            Cull = $"[{nameof(ShaderProperties.Culling)}]";
            Blend = $"{BlendFunc.SrcAlpha} {BlendFunc.OneMinusSrcAlpha}";
            Stencil = new Stencil();
            ZTest = "[_ZTest]";
            ZWrite = "[_ZWrite]";
            ShaderVariant = "outline";
        }
    }
}

#endif