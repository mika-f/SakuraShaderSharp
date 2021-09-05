using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Primitives;

#if SHARPX_COMPILER

namespace NatsunekoLaboratory.SakuraShader.Avatars.Effects.Shader
{
    [External]
    public class BuiltinOverload
    {
        [Function("distance")]
        public static extern SlFloat Distance(SlFloat a, SlFloat b);
    }
}


#endif