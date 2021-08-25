
#if SHARPX_COMPILER
using NatsunekoLaboratory.SakuraShader.ScreenFX.Shader;

using SharpX.Library.ShaderLab.Abstractions;

namespace NatsunekoLaboratory.SakuraShader.ScreenFX.ShaderLab
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