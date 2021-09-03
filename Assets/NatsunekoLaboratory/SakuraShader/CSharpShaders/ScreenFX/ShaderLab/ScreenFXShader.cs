#if SHARPX_COMPILER

using System.Collections.Immutable;

using NatsunekoLaboratory.SakuraShader.ScreenFX.Shader;

using SharpX.Library.ShaderLab.Abstractions;
using SharpX.Library.ShaderLab.Attributes;

namespace NatsunekoLaboratory.SakuraShader.ScreenFX.ShaderLab
{
    // ReSharper disable once InconsistentNaming
    [Export("ScreenFX")]
    public class ScreenFXShader : ShaderLabDefinition
    {
        private static readonly ImmutableArray<SubShaderDefinition> Shaders = ImmutableArray.Create<SubShaderDefinition>(new ScreenFXShaderLodNone());

        public ScreenFXShader() : base("NatsunekoLaboratory/Sakura Shader/ScreenFX", typeof(GlobalProperties), Shaders)
        {
            CustomEditor = typeof(ScreenFXInspector);
        }
    }
}

namespace NatsunekoLaboratory.SakuraShader
{
    // ReSharper disable once InconsistentNaming
    internal class ScreenFXInspector { }
}

#endif