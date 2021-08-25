#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Primitives;

namespace NatsunekoLaboratory.SakuraShader.Avatars.Toon.Shader
{ 
    internal static class Globals
    {
        #region Unity Global Fields

        [External]
        [GlobalMember]
        [Property("_Time")]
        public static SlFloat4 Time { get; }

        [External]
        [GlobalMember]
        [Property("_SinTime")]
        public static SlFloat4 SinTime { get; }

        [External]
        [GlobalMember]
        [Property("_CosTime")]
        public static SlFloat4 CosTime { get; }

        #endregion
    }
}

#endif