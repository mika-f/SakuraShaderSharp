#if SHARPX_COMPILER

using System.Collections.Immutable;

using NatsunekoLaboratory.SakuraShader.Particles.Shader;

using SharpX.Library.ShaderLab.Abstractions;
using SharpX.Library.ShaderLab.Attributes;

namespace NatsunekoLaboratory.SakuraShader.Particles.ShaderLab
{
    // ReSharper disable once InconsistentNaming
    [Export("Particles")]
    public class ParticlesShader : ShaderLabDefinition
    {
        private static readonly ImmutableArray<SubShaderDefinition> Shaders = ImmutableArray.Create<SubShaderDefinition>(new ParticlesShaderLodNone());

        public ParticlesShader() : base("NatsunekoLaboratory/Sakura Shader/Particles", typeof(ShaderProperties), Shaders)
        {
            CustomEditor = typeof(ParticlesInspector);
        }
    }
}

namespace NatsunekoLaboratory.SakuraShader
{
    // ReSharper disable once InconsistentNaming
    internal class ParticlesInspector { }
}

#endif