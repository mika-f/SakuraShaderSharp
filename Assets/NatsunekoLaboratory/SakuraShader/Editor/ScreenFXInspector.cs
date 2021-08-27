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
            OnInitializeFoldout(FoldoutStatus);

            // OnShrinkGui(me);
            OnMeltGui(me);
            OnScreenMovementGui(me);
            OnChromaticAberrationGui(me);
            OnColorInverseGui(me);
            OnGrayscaleGui(me);
            OnHueShiftGui(me);
            OnSepiaGui(me);
            OnCinemascopeGui(me);
            OnNoiseGui(me);
            OnStencilGui(me);
            OnOthersGui(me, Culling, ZWrite);
            OnStoreFoldout(FoldoutStatus);
        }

        /*
        private void OnShrinkGui(MaterialEditor me)
        {
            OnToggleGui(me, Category.Shrink, IsEnableShrink, () =>
            {
                me.ShaderProperty(ShrinkWidth, "Width");
                me.ShaderProperty(ShrinkHeight, "Height");
            });
        }
        */

        private void OnMeltGui(MaterialEditor me)
        {
            OnFoldoutAndToggleGui(Category.Melt, IsEnableMelt, () =>
            {
                me.ShaderProperty(MeltAngle, "Angle");
                me.ShaderProperty(MeltInterval, "Interval");
                me.ShaderProperty(MeltDistance, "Distance");
                me.ShaderProperty(MeltVariance, "Variance");
                me.ShaderProperty(MeltSeed, "Seed");
            });
        }

        private void OnScreenMovementGui(MaterialEditor me)
        {
            OnFoldoutAndToggleGui(Category.ScreenMovement, IsEnableScreenMovement, () =>
            {
                me.ShaderProperty(ScreenMovementX, "Movement X");
                me.ShaderProperty(ScreenMovementY, "Movement Y");
                me.ShaderProperty(ScreenMovementZ, "Movement Z");
            });
        }

        private void OnCinemascopeGui(MaterialEditor me)
        {
            OnFoldoutAndToggleGui(Category.Cinemascope, IsEnableCinemascope, () =>
            {
                me.ShaderProperty(CinemascopeColor, "Color");
                me.ShaderProperty(CinemascopeWidth, "Width");
            });
        }

        private void OnNoiseGui(MaterialEditor me)
        {
            OnFoldoutAndToggleGui(Category.Noise, IsEnableNoise, () =>
            {
                me.ShaderProperty(NoisePattern, "Noise Pattern");
                me.ShaderProperty(NoiseRandomFactor, "Noise Random Factor");

                using (new EditorGUI.DisabledGroupScope(!IsEqualsTo(NoisePattern, 2)))
                    me.ShaderProperty(BlockNoiseFactor, "Block Noise Factor");

                me.ShaderProperty(NoiseWeight, "Noise Weight");
            });
        }

        private void OnChromaticAberrationGui(MaterialEditor me)
        {
            OnFoldoutAndToggleGui(Category.ChromaticAberration, IsEnableChromaticAberration, () =>
            {
                me.ShaderProperty(ChromaticAberrationRedOffsetX, "Offset Red X");
                me.ShaderProperty(ChromaticAberrationRedOffsetY, "Offset Red Y");
                me.ShaderProperty(ChromaticAberrationGreenOffsetX, "Offset Green X");
                me.ShaderProperty(ChromaticAberrationGreenOffsetY, "Offset Green Y");
                me.ShaderProperty(ChromaticAberrationBlueOffsetX, "Offset Blue X");
                me.ShaderProperty(ChromaticAberrationBlueOffsetY, "Offset Blue Y");
                me.ShaderProperty(ChromaticAberrationWeight, "Weight");
            });
        }

        private void OnHueShiftGui(MaterialEditor me)
        {
            OnFoldoutAndToggleGui(Category.HueShift, IsEnableHueShift, () =>
            {
                me.ShaderProperty(HueShiftValue, "Hue");
                me.ShaderProperty(SaturationValue, "Saturation");
                me.ShaderProperty(BrightnessValue, "Value");
                me.ShaderProperty(HueShiftWeight, "Weight");
            });
        }

        private void OnGrayscaleGui(MaterialEditor me)
        {
            OnFoldoutAndToggleGui(Category.Grayscale, IsEnableGrayscale, () =>
            {
                //
                me.ShaderProperty(GrayscaleWeight, "Weight");
            });
        }

        private void OnSepiaGui(MaterialEditor me)
        {
            OnFoldoutAndToggleGui(Category.Sepia, IsEnableSepiaColor, () =>
            {
                //
                me.ShaderProperty(SepiaWeight, "Weight");
            });
        }

        private void OnColorInverseGui(MaterialEditor me)
        {
            OnFoldoutAndToggleGui(Category.ColorInverse, IsEnableColorInverse, () =>
            {
                //
                me.ShaderProperty(ColorInverseWeight, "Weight");
            });
        }

        private void OnStencilGui(MaterialEditor me)
        {
            OnFoldOutGui(Category.Stencil, () =>
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

        private enum Category
        {
            [EnumMember(Value = "Effects - Cinemascope")]
            Cinemascope,

            [EnumMember(Value = "Colors - Chromatic Aberration")]
            ChromaticAberration,

            [EnumMember(Value = "Effects - Noise")]
            Noise,

            [EnumMember(Value = "Colors - HUE Shift")]
            HueShift,

            [EnumMember(Value = "Colors - Grayscale")]
            Grayscale,

            [EnumMember(Value = "Colors - Sepia")]
            Sepia,

            Stencil,

            [EnumMember(Value = "Distortion - Shrink")]
            Shrink,

            [EnumMember(Value = "Colors - Color Inverse")]
            ColorInverse,

            [EnumMember(Value = "Distortion - Melt")]
            Melt,

            [EnumMember(Value = "Distortion - Screen Movement")]
            ScreenMovement,
        }

        // ReSharper disable InconsistentNaming

        private MaterialProperty MainTexture;
        private MaterialProperty IsEnableShrink;
        private MaterialProperty ShrinkWidth;
        private MaterialProperty ShrinkHeight;
        private MaterialProperty IsEnableScreenMovement;
        private MaterialProperty ScreenMovementX;
        private MaterialProperty ScreenMovementY;
        private MaterialProperty ScreenMovementZ;
        private MaterialProperty IsEnableMelt;
        private MaterialProperty MeltAngle;
        private MaterialProperty MeltInterval;
        private MaterialProperty MeltVariance;
        private MaterialProperty MeltDistance;
        private MaterialProperty MeltSeed;
        private MaterialProperty IsEnableCinemascope;
        private MaterialProperty CinemascopeColor;
        private MaterialProperty CinemascopeWidth;
        private MaterialProperty IsEnableNoise;
        private MaterialProperty NoisePattern;
        private MaterialProperty BlockNoiseFactor;
        private MaterialProperty NoiseRandomFactor;
        private MaterialProperty NoiseWeight;
        private MaterialProperty IsEnableChromaticAberration;
        private MaterialProperty ChromaticAberrationRedOffsetX;
        private MaterialProperty ChromaticAberrationRedOffsetY;
        private MaterialProperty ChromaticAberrationGreenOffsetX;
        private MaterialProperty ChromaticAberrationGreenOffsetY;
        private MaterialProperty ChromaticAberrationBlueOffsetX;
        private MaterialProperty ChromaticAberrationBlueOffsetY;
        private MaterialProperty ChromaticAberrationWeight;
        private MaterialProperty IsEnableColorInverse;
        private MaterialProperty ColorInverseWeight;
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
        private MaterialProperty FoldoutStatus;

        // ReSharper restore InconsistentNaming
    }
}