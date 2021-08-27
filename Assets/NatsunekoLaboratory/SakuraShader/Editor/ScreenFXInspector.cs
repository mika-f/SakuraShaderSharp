using System.Runtime.Serialization;

using UnityEditor;

using UnityEngine;

#pragma warning disable 649


namespace NatsunekoLaboratory.SakuraShader
{
    // ReSharper disable once InconsistentNaming
    public class ScreenFXInspector : SakuraShaderInspector
    {
        public override void OnGUI(MaterialEditor me, MaterialProperty[] properties)
        {
            var material = (Material)me.target;

            InjectMaterialProperties(properties);

            OnHeaderGui("ScreenFX Shader");
            OnInitialize(material);

            OnCinemascopeGui(me);
            OnNoiseGui(me);
            OnHueShiftGui(me);
            OnGrayscaleGui(me);
            OnSepiaGui(me);
            OnStencilGui(me);
            OnOthersGui(me, Culling, ZWrite);
        }

        private void OnCinemascopeGui(MaterialEditor me)
        {
            OnToggleGui(me, Category.Cinemascope, IsEnableCinemascope, () =>
            {
                me.ShaderProperty(CinemascopeColor, "Color");
                me.ShaderProperty(CinemascopeWidth, "Width");
            });
        }

        private void OnNoiseGui(MaterialEditor me)
        {
            OnToggleGui(me, Category.Noise, IsEnableNoise, () =>
            {
                me.ShaderProperty(NoisePattern, "Noise Pattern");
                me.ShaderProperty(NoiseRandomFactor, "Noise Random Factor");

                using (new EditorGUI.DisabledGroupScope(!IsEqualsTo(NoisePattern, 2)))
                    me.ShaderProperty(BlockNoiseFactor, "Block Noise Factor");

                me.ShaderProperty(NoiseWeight, "Noise Weight");
            });
        }

        private void OnHueShiftGui(MaterialEditor me)
        {
            OnToggleGui(me, Category.HueShift, IsEnableHueShift, () =>
            {
                me.ShaderProperty(HueShiftValue, "Hue");
                me.ShaderProperty(SaturationValue, "Saturation");
                me.ShaderProperty(BrightnessValue, "Value");
                me.ShaderProperty(HueShiftWeight, "Weight");
            });
        }

        private void OnGrayscaleGui(MaterialEditor me)
        {
            OnToggleGui(me, Category.Grayscale, IsEnableGrayscale,  () =>
            {
                //
                me.ShaderProperty(GrayscaleWeight, "Weight");
            });
        }

        private void OnSepiaGui(MaterialEditor me)
        {
            OnToggleGui(me, Category.Sepia, IsEnableSepiaColor, () =>
            {
                //
                me.ShaderProperty(SepiaWeight, "Weight");
            });
        }

        private void OnStencilGui(MaterialEditor me)
        {
            using (new Section("Stencil"))
            {
                OnFoldoutGui(me, Category.Stencil, () =>
                {
                    me.ShaderProperty(StencilRef, "Reference");
                    me.ShaderProperty(StencilComp, "Compare Function");
                    me.ShaderProperty(StencilPass, "Pass");
                    me.ShaderProperty(StencilFail, "Fail");
                    me.ShaderProperty(StencilZFail, "ZFail");
                    me.ShaderProperty(StencilReadMask, "Read Mask");
                    me.ShaderProperty(StencilWriteMask, "Write Mask");
                });
            }
        }

        private enum Category
        {
            Cinemascope,

            Noise,

            [EnumMember(Value = "HUE Shift")]
            HueShift,

            Grayscale,

            Sepia,

            Stencil
        }

        // ReSharper disable InconsistentNaming

        private MaterialProperty MainTexture;
        private MaterialProperty IsEnableCinemascope;
        private MaterialProperty CinemascopeColor;
        private MaterialProperty CinemascopeWidth;
        private MaterialProperty IsEnableNoise;
        private MaterialProperty NoisePattern;
        private MaterialProperty BlockNoiseFactor;
        private MaterialProperty NoiseRandomFactor;
        private MaterialProperty NoiseWeight;
        private MaterialProperty IsEnableHueShift;
        private MaterialProperty HueShiftValue;
        private MaterialProperty SaturationValue;
        private MaterialProperty BrightnessValue;
        private MaterialProperty HueShiftWeight;
        private MaterialProperty IsEnableGrayscale;
        private MaterialProperty GrayscaleWeight;
        private MaterialProperty IsEnableSepiaColor;
        private MaterialProperty SepiaWeight;
        private MaterialProperty StencilRef;
        private MaterialProperty StencilComp;
        private MaterialProperty StencilPass;
        private MaterialProperty StencilFail;
        private MaterialProperty StencilZFail;
        private MaterialProperty StencilReadMask;
        private MaterialProperty StencilWriteMask;
        private MaterialProperty Culling;
        private MaterialProperty ZWrite;

        // ReSharper restore InconsistentNaming
    }
}