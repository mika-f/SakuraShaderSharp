#if SHARPX_COMPILER

using SharpX.Library.ShaderLab.Abstractions;

namespace NatsunekoLaboratory.SakuraShader.ScreenFX.ShaderLab
{
    internal class ScreenFXShaderGrabPass : ShaderPassDefinition
    {
        public ScreenFXShaderGrabPass() : base("GrabPassTexture_SakuraShaderScreenFX") { }
    }
}


#endif