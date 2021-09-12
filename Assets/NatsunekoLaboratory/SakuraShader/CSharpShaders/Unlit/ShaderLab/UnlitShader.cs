#if SHARPX_COMPILER

using System.Collections.Immutable;

using NatsunekoLaboratory.SakuraShader.Unlit.Shader;

using SharpX.Library.ShaderLab.Abstractions;
using SharpX.Library.ShaderLab.Attributes;

namespace NatsunekoLaboratory.SakuraShader.Unlit.ShaderLab
{
    [Export("Unlit")]
    public class UnlitShader : ShaderLabDefinition
    {
        private static readonly ImmutableArray<SubShaderDefinition> Shaders = ImmutableArray.Create<SubShaderDefinition>(new UnlitShaderLodNone());

        public UnlitShader() : base("NatsunekoLaboratory/Sakura Shader/Unlit", typeof(ShaderProperties), Shaders)
        {
            CustomEditor = typeof(UnlitInspector);
        }
    }
}

namespace NatsunekoLaboratory.SakuraShader
{
    // ReSharper disable once InconsistentNaming
    internal class UnlitInspector { }
}

#endif