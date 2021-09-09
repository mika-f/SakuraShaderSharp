#if SHARPX_COMPILER

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using SharpX.Library.ShaderLab.Abstractions;
using SharpX.Library.ShaderLab.Enums;

namespace NatsunekoLaboratory.SakuraShader.Avatars.Toon.ShaderLab
{
    internal class AvatarsToonShaderLodAll : SubShaderDefinition
    {
        private static readonly ImmutableArray<ShaderPassDefinition> Passes = ImmutableArray.Create<ShaderPassDefinition>(new AvatarsToonShaderPass());

        internal AvatarsToonShaderLodAll() : base(Passes)
        {
            Tags = new Dictionary<object, object>
            {
                { ShaderTags.RenderType, RenderType.Opaque },
                { ShaderTags.Queue, Queue.Geometry },
                { ShaderTags.DisableBatching, "True" },
            }.ToDictionary(w => w.Key.ToString()!, w => w.Value.ToString()!).ToImmutableDictionary();

        }
    }
}

#endif