﻿#if SHARPX_COMPILER

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using SharpX.Library.ShaderLab.Abstractions;
using SharpX.Library.ShaderLab.Enums;

namespace NatsunekoLaboratory.SakuraShader.Particles.ShaderLab
{
    internal class ParticlesShaderLodNone : SubShaderDefinition
    {
        private static readonly ImmutableArray<ShaderPassDefinition> Passes = ImmutableArray.Create<ShaderPassDefinition>(new ParticlesShaderPass());

        public ParticlesShaderLodNone() : base(Passes)
        {
            Tags = new Dictionary<object, object>
            {
                { ShaderTags.RenderType, RenderType.Transparent },
                { ShaderTags.Queue, Queue.Transparent },
                { ShaderTags.DisableBatching, "True" },
                { ShaderTags.ForceNoShadowCasting, "True" },
                { ShaderTags.IgnoreProjector, "True"},
                { ShaderTags.PreviewType, "Plane" }
            }.ToDictionary(w => w.Key.ToString()!, w => w.Value.ToString()!).ToImmutableDictionary();
        }
    }
}

#endif