﻿#if SHARPX_COMPILER

using NatsunekoLaboratory.SakuraShader.Avatars.Effects.Shader;

using SharpX.Library.ShaderLab.Abstractions;

namespace NatsunekoLaboratory.SakuraShader.Avatars.Effects.ShaderLab
{
    internal class Stencil : StencilDefinition
    {
        public Stencil()
        {
            Ref = $"[{nameof(GlobalProperties.StencilRef)}]";
            Compare = $"[{nameof(GlobalProperties.StencilComp)}]";
            Pass = $"[{nameof(GlobalProperties.StencilPass)}]";
            Fail = $"[{nameof(GlobalProperties.StencilFail)}]";
            ZFail = $"[{nameof(GlobalProperties.StencilZFail)}]";
            ReadMask = $"[{nameof(GlobalProperties.StencilReadMask)}]";
            WriteMask = $"[{nameof(GlobalProperties.StencilWriteMask)}]";
        }
    }
}

#endif