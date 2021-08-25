#if SHARPX_COMPILER
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using SharpX.Library.ShaderLab.Abstractions;
using SharpX.Library.ShaderLab.Enums;

namespace NatsunekoLaboratory.SakuraShader.ScreenFX.ShaderLab
{
    internal class ScreenFXShaderLodNone : SubShaderDefinition
    {
        private static readonly ImmutableArray<ShaderPassDefinition> Passes = ImmutableArray.Create<ShaderPassDefinition>(new ScreenFXShaderGrabPass(), new ScreenFXShaderFirstPass());

        internal ScreenFXShaderLodNone() : base(Passes)
        {
            Tags = new Dictionary<object, object>
            {
                { ShaderTags.RenderType, RenderType.Opaque },
                { ShaderTags.Queue, $"{Queue.Transparent}+5000" },
                { ShaderTags.DisableBatching, "True" },
                { ShaderTags.ForceNoShadowCasting, "True" }
            }.ToDictionary(w => w.Key.ToString()!, w => w.Value.ToString()!).ToImmutableDictionary();
        }
    }
}

#endif