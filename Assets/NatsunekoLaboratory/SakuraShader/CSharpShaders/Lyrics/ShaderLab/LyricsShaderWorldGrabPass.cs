#if SHARPX_COMPILER

using System.Collections.Generic;
using System.Collections.Immutable;

using SharpX.Library.ShaderLab.Abstractions;
using SharpX.Library.ShaderLab.Enums;

namespace NatsunekoLaboratory.SakuraShader.Lyrics.ShaderLab
{
    internal class LyricsShaderWorldGrabPass : ShaderPassDefinition
    {
        public LyricsShaderWorldGrabPass() : base("SakuraShader_Lyrics_WorldGrab")
        {
            Tags = new Dictionary<object, string>
            {
                { ShaderTags.LightMode, "ForwardBase" }
            }.ToImmutableDictionary(w => w.Key.ToString(), w => w.Value);
        }
    }
}

#endif