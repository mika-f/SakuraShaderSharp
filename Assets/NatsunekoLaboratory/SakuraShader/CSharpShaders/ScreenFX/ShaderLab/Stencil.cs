#if SHARPX_COMPILER

using NatsunekoLaboratory.SakuraShader.ScreenFX.Shader;

using SharpX.Library.ShaderLab.Abstractions;

namespace NatsunekoLaboratory.SakuraShader.ScreenFX.ShaderLab
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