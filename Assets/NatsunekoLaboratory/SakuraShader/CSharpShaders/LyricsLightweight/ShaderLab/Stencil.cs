#if SHARPX_COMPILER

using NatsunekoLaboratory.SakuraShader.LyricsLightweight.Shader;

using SharpX.Library.ShaderLab.Abstractions;

namespace NatsunekoLaboratory.SakuraShader.LyricsLightweight.ShaderLab
{
    internal class Stencil : StencilDefinition
    {
        public Stencil()
        {
            Ref = $"[{nameof(ShaderProperties.StencilRef)}]";
            Compare = $"[{nameof(ShaderProperties.StencilComp)}]";
            Pass = $"[{nameof(ShaderProperties.StencilPass)}]";
            Fail = $"[{nameof(ShaderProperties.StencilFail)}]";
            ZFail = $"[{nameof(ShaderProperties.StencilZFail)}]";
            ReadMask = $"[{nameof(ShaderProperties.StencilReadMask)}]";
            WriteMask = $"[{nameof(ShaderProperties.StencilWriteMask)}]";
        }
    }
}

#endif