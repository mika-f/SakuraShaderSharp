#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Predefined;

namespace NatsunekoLaboratory.SakuraShader.Avatars.Toon.Shader
{
    [Export("vert.{extension}")]
    internal class VertexShader
    {
        [VertexShader]
        public Vertex2Fragment Vertex(AppDataFull i)
        {
            return null;
        }
    }
}

#endif