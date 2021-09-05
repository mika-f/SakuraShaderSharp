#if SHARPX_COMPILER

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using SharpX.Library.ShaderLab.Abstractions;
using SharpX.Library.ShaderLab.Enums;

namespace NatsunekoLaboratory.SakuraShader.Avatars.Effects.ShaderLab
{
    internal class AvatarsEffectsShaderLodNone : SubShaderDefinition
    {
        private static readonly ImmutableArray<ShaderPassDefinition> Passes = ImmutableArray.Create<ShaderPassDefinition>(new AvatarsEffectsShaderVoxelPass(), new AvatarsEffectsShaderHolographPass());

        public AvatarsEffectsShaderLodNone() : base(Passes)
        {
            Tags = new Dictionary<object, object>
            {
                { ShaderTags.RenderType, RenderType.Opaque },
                { ShaderTags.Queue, Queue.Transparent },
                { ShaderTags.DisableBatching, "True" },
            }.ToDictionary(w => w.Key.ToString()!, w => w.Value.ToString()!).ToImmutableDictionary();

        }
    }
}


#endif