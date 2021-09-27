#if SHARPX_COMPILER

using System;
using System.Collections.Generic;
using System.Collections.Immutable;

using NatsunekoLaboratory.SakuraShader.Lyrics.Shader;

using SharpX.Library.ShaderLab.Abstractions;
using SharpX.Library.ShaderLab.Enums;

namespace NatsunekoLaboratory.SakuraShader.Lyrics.ShaderLab
{
    public class LyricsShaderClearPass : ShaderPassDefinition
    {
        private static readonly ImmutableDictionary<string, string> ShaderPragmas = new Dictionary<string, string>
        {
            { "target", "4.5" },
            { "vertex", Configuration.GetShaderEntryPoint(typeof(VertexShader), Configuration.EntryPoint.VertexShader) },
            { "fragment", Configuration.GetShaderEntryPoint(typeof(FragmentShaderClear), Configuration.EntryPoint.FragmentShader) }
        }.ToImmutableDictionary();

        private static readonly ImmutableArray<Type> Shaders = ImmutableArray.Create(typeof(Vertex2Fragment), typeof(VertexShader), typeof(FragmentShaderClear));

        public LyricsShaderClearPass() : base(ShaderPragmas, Shaders)
        {
            Cull = $"[{nameof(ShaderProperties.Culling)}]";
            Blend = $"{BlendFunc.SrcAlpha} {BlendFunc.OneMinusSrcAlpha}";
            Stencil = new Stencil();
            Tags = new Dictionary<object, string>
            {
                { ShaderTags.LightMode, "ForwardBase" }
            }.ToImmutableDictionary(w => w.Key.ToString(), w => w.Value);
            ZTest = "[_ZTest]";
            ZWrite = "[_ZWrite]";
            ShaderVariant = "normal";
        }
    }
}

#endif