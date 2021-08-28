#if SHARPX_COMPILER

using System;
using System.Collections.Generic;
using System.Collections.Immutable;

using NatsunekoLaboratory.SakuraShader.ScreenFX.Shader;
using NatsunekoLaboratory.SakuraShader.ScreenFX.Shader.Fragment;

using SharpX.Library.ShaderLab.Abstractions;
using SharpX.Library.ShaderLab.Enums;

namespace NatsunekoLaboratory.SakuraShader.ScreenFX.ShaderLab
{
    internal class ScreenFXShaderFirstPass : ShaderPassDefinition
    {
        private static readonly ImmutableDictionary<string, string> ShaderPragmas = new Dictionary<string, string>
        {
            { "target", "4.5" },
            { "vertex", Configuration.GetShaderEntryPoint(typeof(VertexShader), Configuration.EntryPoint.VertexShader) },
            { "fragment", Configuration.GetShaderEntryPoint(typeof(FragmentShader), Configuration.EntryPoint.FragmentShader) }
        }.ToImmutableDictionary();

        private static readonly ImmutableArray<Type> Shaders = ImmutableArray.Create(typeof(Vertex2Fragment), typeof(VertexShader), typeof(Utilities), typeof(ColorEffects), typeof(SpecialEffects), typeof(DistortionEffects), typeof(FragmentShader));

        public ScreenFXShaderFirstPass() : base(ShaderPragmas, Shaders)
        {
            Cull = $"{Culling.Off}";
            Blend = $"{BlendFunc.SrcAlpha} {BlendFunc.OneMinusSrcAlpha}";
            Stencil = new Stencil();
            ZTest = $"{ZTestFunc.Always}";
            ZWrite = $"{Switch.Off}";
        }
    }
}

#endif