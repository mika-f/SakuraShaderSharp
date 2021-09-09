#if SHARPX_COMPILER

using System.Collections.Immutable;

using NatsunekoLaboratory.SakuraShader.Avatars.Toon.Shader;

using SharpX.Library.ShaderLab.Abstractions;
using SharpX.Library.ShaderLab.Attributes;

namespace NatsunekoLaboratory.SakuraShader.Avatars.Toon.ShaderLab
{
    [Export("Avatars-Toon")]
    public class AvatarsToonShader : ShaderLabDefinition
    {
        private static readonly ImmutableArray<SubShaderDefinition> Shaders = ImmutableArray.Create<SubShaderDefinition>(new AvatarsToonShaderLodAll());

        public AvatarsToonShader() : base("NatsunekoLaboratory/Sakura Shader/Avatars - Toon", typeof(GlobalProperties), Shaders)
        {
            CustomEditor = typeof(AvatarsToonInspector);
        }
    }
}

namespace NatsunekoLaboratory.SakuraShader
{
    // ReSharper disable once InconsistentNaming
    internal class AvatarsToonInspector { }
}

#endif