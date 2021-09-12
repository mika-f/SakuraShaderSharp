#if SHARPX_COMPILER

using System.Collections.Immutable;

using NatsunekoLaboratory.SakuraShader.MotionGraphics.Shader;
using NatsunekoLaboratory.SakuraShader.MotionGraphics.ShaderLab;

using SharpX.Library.ShaderLab.Abstractions;
using SharpX.Library.ShaderLab.Attributes;

namespace NatsunekoLaboratory.SakuraShader.Lyrics.ShaderLab
{
    [Export("MotionGraphics")]
    public class MotionGraphicsShader : ShaderLabDefinition
    {
        private static readonly ImmutableArray<SubShaderDefinition> Shaders = ImmutableArray.Create<SubShaderDefinition>(new MotionGraphicsShaderLodNone());

        public MotionGraphicsShader() : base("NatsunekoLaboratory/Sakura Shader/Motion Graphics", typeof(ShaderProperties), Shaders)
        {
            CustomEditor = typeof(MotionGraphicsInspector);
        }
    }
}

namespace NatsunekoLaboratory.SakuraShader
{
    // ReSharper disable once InconsistentNaming
    internal class MotionGraphicsInspector { }
}

#endif