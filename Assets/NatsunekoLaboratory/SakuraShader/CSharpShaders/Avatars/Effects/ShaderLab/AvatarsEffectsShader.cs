#if SHARPX_COMPILER

using System.Collections.Immutable;

using NatsunekoLaboratory.SakuraShader.Avatars.Effects.Shader;

using SharpX.Library.ShaderLab.Abstractions;
using SharpX.Library.ShaderLab.Attributes;

namespace NatsunekoLaboratory.SakuraShader.Avatars.Effects.ShaderLab
{
    [Export("Avatars-Effects")]
    public class AvatarsEffectsShader : ShaderLabDefinition
    {
        private static readonly ImmutableArray<SubShaderDefinition> Shaders = ImmutableArray.Create<SubShaderDefinition>(new AvatarsEffectsShaderLodNone());

        public AvatarsEffectsShader() : base("NatsunekoLaboratory/Sakura Shader/Avatars - Effects", typeof(GlobalProperties), Shaders)
        {
            CustomEditor = typeof(AvatarsEffectsInspector);
        }
    }
}

namespace NatsunekoLaboratory.SakuraShader
{
    // ReSharper disable once InconsistentNaming
    internal class AvatarsEffectsInspector { }
}

#endif