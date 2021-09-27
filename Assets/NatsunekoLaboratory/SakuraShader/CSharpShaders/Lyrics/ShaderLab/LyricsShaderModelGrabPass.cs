#if SHARPX_COMPILER

using System.Collections.Generic;
using System.Collections.Immutable;

using SharpX.Library.ShaderLab.Abstractions;
using SharpX.Library.ShaderLab.Enums;

namespace NatsunekoLaboratory.SakuraShader.Lyrics.ShaderLab
{
    class LyricsShaderModelGrabPass : ShaderPassDefinition
    {
        public LyricsShaderModelGrabPass() : base("")
        {
            Tags = new Dictionary<object, string>
            {
                { ShaderTags.LightMode, "ForwardBase" }
            }.ToImmutableDictionary(w => w.Key.ToString(), w => w.Value);
        }
    }
}


#endif