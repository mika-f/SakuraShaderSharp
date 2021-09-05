#if SHARPX_COMPILER

using System;
using System.Collections.Generic;
using System.Collections.Immutable;

using NatsunekoLaboratory.SakuraShader.Avatars.Effects.Shader;
using NatsunekoLaboratory.SakuraShader.Avatars.Effects.Shader.Geometry;

using SharpX.Library.ShaderLab.Abstractions;

using Random = NatsunekoLaboratory.SakuraShader.Avatars.Effects.Shader.Random;

namespace NatsunekoLaboratory.SakuraShader.Avatars.Effects.ShaderLab
{
    internal class AvatarsEffectsShaderVoxelPass : ShaderPassDefinition
    {
        private static readonly ImmutableDictionary<string, string> ShaderPragmas = new Dictionary<string, string>
        {
            { "target", "4.5" },
            { "require", "geometry"},
            { "vertex", Configuration.GetShaderEntryPoint(typeof(VertexShader), Configuration.EntryPoint.VertexShader) },
            { "geometry", Configuration.GetShaderEntryPoint(typeof(GeometryShader), Configuration.EntryPoint.GeometryShader) },
            { "fragment", Configuration.GetShaderEntryPoint(typeof(FragmentShader), Configuration.EntryPoint.FragmentShader) }
        }.ToImmutableDictionary();

        private static readonly ImmutableArray<Type> Shaders = ImmutableArray.Create(typeof(Random), typeof(Vertex2Geometry), typeof(Geometry2Fragment), typeof(VertexShader), typeof(Voxelization), typeof(GeometryShader), typeof(FragmentShader));

        public AvatarsEffectsShaderVoxelPass() : base(ShaderPragmas, Shaders) { }
    }
}

#endif