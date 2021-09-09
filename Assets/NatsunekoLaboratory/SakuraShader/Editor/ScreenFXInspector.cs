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
            OnInitializeFoldout(FoldoutStatus1, FoldoutStatus2);

            // OnShrinkGui(me);
            OnMeltGui(me);
            OnScreenMovementGui(me);
            OnScreenRotationGui(me);
            OnScreenTransformGui(me);
            OnPixelationGui(me);
            OnCheckerboardGui(me);
            OnChromaticAberrationGui(me);
            OnColorInverseGui(me);
            OnGrayscaleGui(me);
            OnHueShiftGui(me);
            OnSepiaGui(me);
            OnColorLayerGui(me);
            OnCinemascopeGui(me);
            OnGlitchGui(me);
            OnNoiseGui(me);
            OnGirlsCamGui(me);
            OnColoredCheckerboardGui(me);
            OnBlurGui(me);
            OnImageOverlayGui(me);
            OnStageCurtainGui(me);
            OnStencilGui(me);
            OnOthersGui(me, Culling, ZWrite);
            OnStoreFoldout(FoldoutStatus1, FoldoutStatus2);
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

                EnabledWhen(NoisePattern, ScreenFX.Enums.NoisePattern.Block, () =>
                {
                    //
                    me.ShaderProperty(BlockNoiseFactor, "Block Noise Factor");
                });

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

        private void OnScreenRotationGui(MaterialEditor me)
        {
            OnFoldoutAndToggleGui(Category.ScreenRotation, IsEnableScreenRotation, () =>
            {
                using (new EditorGUI.DisabledGroupScope(true))
                {
                    me.ShaderProperty(ScreenRotationPitch, "Pitch");
                    me.ShaderProperty(ScreenRotationYaw, "Yaw");
                }

                me.ShaderProperty(ScreenRotationRoll, "Roll");
            });
        }

        private void OnGlitchGui(MaterialEditor me)
        {
            OnFoldoutAndToggleGui(Category.Glitch, IsEnableGlitch, () =>
            {
                me.ShaderProperty(GlitchMode, "Mode");

                EnabledWhen(GlitchMode, ScreenFX.Enums.GlitchMode.Block, () =>
                {
                    me.ShaderProperty(GlitchBlockSizeX, "Block Size X");
                    me.ShaderProperty(GlitchBlockSizeY, "Block Size Y");
                    me.ShaderProperty(GlitchThreshold, "Threshold");
                    me.ShaderProperty(GlitchAberrationOffset, "Aberration Offset");
                });

                EnabledWhen(GlitchMode, ScreenFX.Enums.GlitchMode.KinoAnalog, () =>
                {
                    me.ShaderProperty(GlitchScanLineJitter, "Jitter Displacement");
                    me.ShaderProperty(GlitchVerticalJumpAmount, "Vertical Jump Amount");
                    me.ShaderProperty(GlitchHorizontalShake, "Horizontal Shake");
                    me.ShaderProperty(GlitchColorDriftAmount, "Color Drift Amount");
                });
            });
        }

        private void OnGirlsCamGui(MaterialEditor me)
        {
            OnFoldoutAndToggleGui(Category.GirlsCam, IsEnableGirlsCam, () =>
            {
                //
                me.ShaderProperty(GirlsCamSize, "Size");
            });
        }

        private void OnScreenTransformGui(MaterialEditor me)
        {
            OnFoldoutAndToggleGui(Category.ScreenTransform, IsEnableScreenTransform, () =>
            {
                me.ShaderProperty(TransformHorizontal, "Horizontal");
                me.ShaderProperty(TransformVertical, "Vertical");
            });
        }

        private void OnColorLayerGui(MaterialEditor me)
        {
            OnFoldoutAndToggleGui(Category.ColorLayer, IsEnableColorLayer, () =>
            {
                me.ShaderProperty(LayerColor, "Color");
                me.ShaderProperty(LayerBlendMode, "Blend Mode");
                me.ShaderProperty(IsEnableColorLayerPartially, "Enable Partially Layer");

                EnabledWhen(IsEnableColorLayerPartially, true, () =>
                {
                    me.ShaderProperty(ColorLayerDirection, "Partially Directional");
                    me.ShaderProperty(ColorLayerWeight, "Weight");
                });
            });
        }

        private void OnPixelationGui(MaterialEditor me)
        {
            OnFoldoutAndToggleGui(Category.Pixelation, IsEnablePixelation, () =>
            {
                me.ShaderProperty(PixelationHeight, "Height");
                me.ShaderProperty(PixelationWidth, "Width");
            });
        }

        private void OnCheckerboardGui(MaterialEditor me)
        {
            OnFoldoutAndToggleGui(Category.Checkerboard, IsEnableCheckerboard, () =>
            {
                me.ShaderProperty(CheckerboardAngle, "Angle");
                me.ShaderProperty(CheckerboardHeight, "Height");
                me.ShaderProperty(CheckerboardWidth, "Width");
                me.ShaderProperty(CheckerboardOffset, "Offset");
            });
        }

        private void OnColoredCheckerboardGui(MaterialEditor me)
        {
            OnFoldoutAndToggleGui(Category.ColoredCheckerboard, IsEnableColoredCheckerboard, () =>
            {
                me.ShaderProperty(ColoredCheckerboardAngle, "Angle");
                me.ShaderProperty(ColoredCheckerboardHeight, "Height");
                me.ShaderProperty(ColoredCheckerboardWidth, "Width");
                me.ShaderProperty(ColoredCheckerboardColor1, "Color 1");
                me.ShaderProperty(ColoredCheckerboardColor2, "Color 2");
                me.ShaderProperty(ColoredCheckerboardWeight, "Weight");
            });
        }

        private void OnBlurGui(MaterialEditor me)
        {
            OnFoldoutAndToggleGui(Category.Blur, IsEnableBlur, () =>
            {
                me.ShaderProperty(BlurAlgorithmMode, "Algorithm");
                me.ShaderProperty(BlurSamplingIterations, "Sampling Iterations");
                me.ShaderProperty(BlurFactor, "Factor");
                me.ShaderProperty(BlurTexel, "Texel");
            });
        }

        private void OnImageOverlayGui(MaterialEditor me)
        {
            OnFoldoutAndToggleGui(Category.ImageOverlay, IsEnableImageOverlay, () =>
            {
                me.TexturePropertySingleLine(new GUIContent("Overlay Texture"), ImageOverlayTexture);
                me.ShaderProperty(ImageOverlayBlendMode, "Blend Mode");
            });
        }

        private void OnStageCurtainGui(MaterialEditor me)
        {
            OnFoldoutAndToggleGui(Category.StageCurtain, IsEnableStageCurtain, () =>
            {
                me.TexturePropertySingleLine(new GUIContent("Stage Curtain Texture"), StageCurtainTexture);
                me.ShaderProperty(StageCurtainColor, "Color");
                me.ShaderProperty(StageCurtainWeight, "Weight");
                me.ShaderProperty(IsStageCurtainFlipped, "Flip Stage Curtain");
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
            Cinemascope = 1,

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

            [EnumMember(Value = "Distortion - Screen Rotation")]
            ScreenRotation,

            [EnumMember(Value = "Effects - Glitch")]
            Glitch,

            [EnumMember(Value = "Effects - GirlsCam")]
            GirlsCam,

            [EnumMember(Value = "Colors - Color Layer")]
            ColorLayer,

            [EnumMember(Value = "Distortion - Screen Transform")]
            ScreenTransform,

            [EnumMember(Value = "Distortion - Pixelation")]
            Pixelation,

            [EnumMember(Value = "Distortion - Checkerboard")]
            Checkerboard,

            [EnumMember(Value = "Effects - Checkerboard (Colored)")]
            ColoredCheckerboard,

            [EnumMember(Value = "Effects - Blur")]
            Blur,

            [EnumMember(Value = "Effects - Image Overlay")]
            ImageOverlay,

            [EnumMember(Value = "Effects - Stage Curtain")]
            StageCurtain,
        }

        // ReSharper disable InconsistentNaming

        private MaterialProperty MainTexture;
        private MaterialProperty IsEnableShrink;
        private MaterialProperty ShrinkWidth;
        private MaterialProperty ShrinkHeight;
        private MaterialProperty IsEnableScreenRotation;
        private MaterialProperty ScreenRotationPitch;
        private MaterialProperty ScreenRotationYaw;
        private MaterialProperty ScreenRotationRoll;
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
        private MaterialProperty CinemascopeAngle;
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
        private MaterialProperty IsEnableGlitch;
        private MaterialProperty GlitchMode;
        private MaterialProperty GlitchBlockSizeX;
        private MaterialProperty GlitchBlockSizeY;
        private MaterialProperty GlitchThreshold;
        private MaterialProperty GlitchAberrationOffset;
        private MaterialProperty GlitchScanLineJitter;
        private MaterialProperty GlitchVerticalJumpAmount;
        private MaterialProperty GlitchHorizontalShake;
        private MaterialProperty GlitchColorDriftAmount;
        private MaterialProperty IsEnableGirlsCam;
        private MaterialProperty GirlsCamSize;
        private MaterialProperty IsEnableColorLayer;
        private MaterialProperty LayerBlendMode;
        private MaterialProperty LayerColor;
        private MaterialProperty IsEnableColorLayerPartially;
        private MaterialProperty ColorLayerDirection;
        private MaterialProperty ColorLayerWeight;
        private MaterialProperty IsEnableScreenTransform;
        private MaterialProperty TransformHorizontal;
        private MaterialProperty TransformVertical;
        private MaterialProperty IsEnablePixelation;
        private MaterialProperty PixelationHeight;
        private MaterialProperty PixelationWidth;
        private MaterialProperty IsEnableCheckerboard;
        private MaterialProperty CheckerboardWidth;
        private MaterialProperty CheckerboardHeight;
        private MaterialProperty CheckerboardAngle;
        private MaterialProperty CheckerboardOffset;
        private MaterialProperty IsEnableColoredCheckerboard;
        private MaterialProperty ColoredCheckerboardWidth;
        private MaterialProperty ColoredCheckerboardHeight;
        private MaterialProperty ColoredCheckerboardAngle;
        private MaterialProperty ColoredCheckerboardColor1;
        private MaterialProperty ColoredCheckerboardColor2;
        private MaterialProperty ColoredCheckerboardWeight;
        private MaterialProperty IsEnableBlur;
        private MaterialProperty BlurAlgorithmMode;
        private MaterialProperty BlurSamplingIterations;
        private MaterialProperty BlurFactor;
        private MaterialProperty BlurTexel;
        private MaterialProperty IsEnableImageOverlay;
        private MaterialProperty ImageOverlayTexture;
        private MaterialProperty ImageOverlayBlendMode;
        private MaterialProperty IsEnableStageCurtain;
        private MaterialProperty StageCurtainColor;
        private MaterialProperty StageCurtainTexture;
        private MaterialProperty StageCurtainWeight;
        private MaterialProperty IsStageCurtainFlipped;
        private MaterialProperty StencilRef;
        private MaterialProperty StencilComp;
        private MaterialProperty StencilPass;
        private MaterialProperty StencilFail;
        private MaterialProperty StencilZFail;
        private MaterialProperty StencilReadMask;
        private MaterialProperty StencilWriteMask;
        private MaterialProperty Culling;
        private MaterialProperty ZWrite;
        private MaterialProperty FoldoutStatus1;
        private MaterialProperty FoldoutStatus2;

        // ReSharper restore InconsistentNaming
    }
}