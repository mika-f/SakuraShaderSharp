﻿#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Primitives;

namespace NatsunekoLaboratory.SakuraShader.Lyrics.Shader
{
    [Export("core")]
    [Component]
    public class Vertex2Fragment
    {
        [Semantic("SV_POSITION")]
        public SlFloat4 Vertex { get; set; }

        [Semantic("TEXCOORD0")]
        public SlFloat2 TexCoord { get; set; }

        [Semantic("TEXCOORD1")]
        public SlFloat3 LocalPos { get; set; }

        [Semantic("TEXCOORD2")]
        public SlFloat4 GrabPos { get; set; }
    }
}

#endif