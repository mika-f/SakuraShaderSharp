using System.Runtime.Serialization;

using NatsunekoLaboratory.SakuraShader.MotionGraphics.Shared;

using UnityEditor;

using UnityEngine;

#pragma warning disable 649

namespace NatsunekoLaboratory.SakuraShader
{
    public class MotionGraphicsInspector : SakuraShaderInspector
    {
        public override void OnGUI(MaterialEditor me, MaterialProperty[] properties)
        {
            var material = (Material)me.target;

            InjectMaterialProperties(properties);

            OnHeaderGui("Motion Graphics Shader");
            OnInitialize(material);
            OnInitializeFoldout(FoldoutStatus1, FoldoutStatus2);

            OnMainColorGui(me);
            OnShape1Gui(me);
            OnShape2Gui(me);
            OnShape3Gui(me);
            OnShape4Gui(me);
            OnShape5Gui(me);
            OnShape6Gui(me);
            OnStencilGui(me);
            OnOthersGui(me, Culling, _ZWrite, _ZTest);
            OnStoreFoldout(FoldoutStatus1, FoldoutStatus2);
        }
        private void OnMainColorGui(MaterialEditor me)
        {
            OnFoldOutGui(Category.Color, () =>
            {
                me.TextureProperty(MainTexture, "Texture");
                me.ShaderProperty(MainColor, "Color");
                me.ShaderProperty(AlphaTransparency, "Alpha Transparency");
                me.ShaderProperty(IsOutlined, "Render Outline");

                EnabledWhen(IsOutlined, true, () =>
                {
                    me.ShaderProperty(OutlineColor, "Outline Color");
                });
            });
        }

        private void OnShape1Gui(MaterialEditor me)
        {
            OnFoldOutGui(Category.Shape1, () =>
            {
                me.ShaderProperty(Shape1, "Shape");
                me.ShaderProperty(PositionOffset1, "Position Transform");
                me.ShaderProperty(RotationAngle1, "Rotation");
                me.ShaderProperty(Scale1, "Scale");

                me.ShaderProperty(RepeatMode1, "Repeat Mode");
                DisabledWhen(RepeatMode1, RepeatMode.None, () =>
                {
                    me.ShaderProperty(RepeatPeriod1, "Repeat Period");
                });
                EnabledWhen(RepeatMode1, RepeatMode.Limited, () =>
                {
                    me.ShaderProperty(RepeatLimitedRangeA1, "Repeat Limit Box Range A");
                    me.ShaderProperty(RepeatLimitedRangeB1, "Repeat Limit Box Range B");
                });

                me.ShaderProperty(IsOnion1, "Onion Mode");
                EnabledWhen(IsOnion1, true, () =>
                {
                    me.ShaderProperty(OnionThickness1, "Onion Thickness");
                });

                me.ShaderProperty(Round1, "Corner Round");

                EnabledWhen(Shape1, Shape.Box, () =>
                {
                    me.ShaderProperty(BoxSize1, "Box Size (WH)");
                });

                EnabledWhen(Shape1, Shape.IsoscelesTriangle, () =>
                {
                    me.ShaderProperty(TriangleSize1, "Triangle Size (WH)");
                });

                EnabledWhen(Shape1, Shape.Segment, () =>
                {
                    me.ShaderProperty(SegmentA1, "Segment A");
                    me.ShaderProperty(SegmentB1, "Segment B");
                    me.ShaderProperty(SegmentThickness1, "Segment Thickness");
                });

                EnabledWhen(Shape1, Shape.Pie, () =>
                {
                    me.ShaderProperty(PieAngle1, "Pie Angle");
                });
            });
        }

        private void OnShape2Gui(MaterialEditor me)
        {
            OnFoldOutGui(Category.Shape2, () =>
            {
                me.ShaderProperty(Shape2, "Shape");
                me.ShaderProperty(CombinationFunction2, "Combination Function");
                me.ShaderProperty(PositionOffset2, "Position Transform");
                me.ShaderProperty(RotationAngle2, "Rotation");
                me.ShaderProperty(Scale2, "Scale");

                me.ShaderProperty(RepeatMode2, "Repeat Mode");
                DisabledWhen(RepeatMode2, RepeatMode.None, () =>
                {
                    me.ShaderProperty(RepeatPeriod2, "Repeat Period");
                });
                EnabledWhen(RepeatMode2, RepeatMode.Limited, () =>
                {
                    me.ShaderProperty(RepeatLimitedRangeA2, "Repeat Limit Box Range A");
                    me.ShaderProperty(RepeatLimitedRangeB2, "Repeat Limit Box Range B");
                });

                me.ShaderProperty(IsOnion2, "Onion Mode");
                EnabledWhen(IsOnion2, true, () =>
                {
                    me.ShaderProperty(OnionThickness2, "Onion Thickness");
                });

                me.ShaderProperty(Round2, "Corner Round");

                EnabledWhen(Shape2, Shape.Box, () =>
                {
                    me.ShaderProperty(BoxSize2, "Box Size (WH)");
                });

                EnabledWhen(Shape2, Shape.IsoscelesTriangle, () =>
                {
                    me.ShaderProperty(TriangleSize2, "Triangle Size (WH)");
                });

                EnabledWhen(Shape2, Shape.Segment, () =>
                {
                    me.ShaderProperty(SegmentA2, "Segment A");
                    me.ShaderProperty(SegmentB2, "Segment B");
                    me.ShaderProperty(SegmentThickness2, "Segment Thickness");
                });

                EnabledWhen(Shape2, Shape.Pie, () =>
                {
                    me.ShaderProperty(PieAngle2, "Pie Angle");
                });
            });
        }

        private void OnShape3Gui(MaterialEditor me)
        {
            OnFoldOutGui(Category.Shape3, () =>
            {
                me.ShaderProperty(Shape3, "Shape");
                me.ShaderProperty(CombinationFunction3, "Combination Function");
                me.ShaderProperty(PositionOffset3, "Position Transform");
                me.ShaderProperty(RotationAngle3, "Rotation");
                me.ShaderProperty(Scale3, "Scale");

                me.ShaderProperty(RepeatMode3, "Repeat Mode");
                DisabledWhen(RepeatMode3, RepeatMode.None, () =>
                {
                    me.ShaderProperty(RepeatPeriod3, "Repeat Period");
                });
                EnabledWhen(RepeatMode3, RepeatMode.Limited, () =>
                {
                    me.ShaderProperty(RepeatLimitedRangeA3, "Repeat Limit Box Range A");
                    me.ShaderProperty(RepeatLimitedRangeB3, "Repeat Limit Box Range B");
                });

                me.ShaderProperty(IsOnion3, "Onion Mode");
                EnabledWhen(IsOnion3, true, () =>
                {
                    me.ShaderProperty(OnionThickness3, "Onion Thickness");
                });

                me.ShaderProperty(Round3, "Corner Round");

                EnabledWhen(Shape3, Shape.Box, () =>
                {
                    me.ShaderProperty(BoxSize3, "Box Size (WH)");
                });

                EnabledWhen(Shape3, Shape.IsoscelesTriangle, () =>
                {
                    me.ShaderProperty(TriangleSize3, "Triangle Size (WH)");
                });

                EnabledWhen(Shape3, Shape.Segment, () =>
                {
                    me.ShaderProperty(SegmentA3, "Segment A");
                    me.ShaderProperty(SegmentB3, "Segment B");
                    me.ShaderProperty(SegmentThickness3, "Segment Thickness");
                });

                EnabledWhen(Shape3, Shape.Pie, () =>
                {
                    me.ShaderProperty(PieAngle3, "Pie Angle");
                });
            });
        }

        private void OnShape4Gui(MaterialEditor me)
        {
            OnFoldOutGui(Category.Shape4, () =>
            {
                me.ShaderProperty(Shape4, "Shape");
                me.ShaderProperty(CombinationFunction4, "Combination Function");
                me.ShaderProperty(PositionOffset4, "Position Transform");
                me.ShaderProperty(RotationAngle4, "Rotation");
                me.ShaderProperty(Scale4, "Scale");

                me.ShaderProperty(RepeatMode4, "Repeat Mode");
                DisabledWhen(RepeatMode4, RepeatMode.None, () =>
                {
                    me.ShaderProperty(RepeatPeriod4, "Repeat Period");
                });
                EnabledWhen(RepeatMode4, RepeatMode.Limited, () =>
                {
                    me.ShaderProperty(RepeatLimitedRangeA4, "Repeat Limit Box Range A");
                    me.ShaderProperty(RepeatLimitedRangeB4, "Repeat Limit Box Range B");
                });

                me.ShaderProperty(IsOnion4, "Onion Mode");
                EnabledWhen(IsOnion4, true, () =>
                {
                    me.ShaderProperty(OnionThickness4, "Onion Thickness");
                });

                me.ShaderProperty(Round4, "Corner Round");

                EnabledWhen(Shape4, Shape.Box, () =>
                {
                    me.ShaderProperty(BoxSize4, "Box Size (WH)");
                });

                EnabledWhen(Shape4, Shape.IsoscelesTriangle, () =>
                {
                    me.ShaderProperty(TriangleSize4, "Triangle Size (WH)");
                });

                EnabledWhen(Shape4, Shape.Segment, () =>
                {
                    me.ShaderProperty(SegmentA4, "Segment A");
                    me.ShaderProperty(SegmentB4, "Segment B");
                    me.ShaderProperty(SegmentThickness4, "Segment Thickness");
                });

                EnabledWhen(Shape4, Shape.Pie, () =>
                {
                    me.ShaderProperty(PieAngle4, "Pie Angle");
                });
            });
        }

        private void OnShape5Gui(MaterialEditor me)
        {
            OnFoldOutGui(Category.Shape5, () =>
            {
                me.ShaderProperty(Shape5, "Shape");
                me.ShaderProperty(CombinationFunction5, "Combination Function");
                me.ShaderProperty(PositionOffset5, "Position Transform");
                me.ShaderProperty(RotationAngle5, "Rotation");
                me.ShaderProperty(Scale5, "Scale");

                me.ShaderProperty(RepeatMode5, "Repeat Mode");
                DisabledWhen(RepeatMode5, RepeatMode.None, () =>
                {
                    me.ShaderProperty(RepeatPeriod5, "Repeat Period");
                });
                EnabledWhen(RepeatMode5, RepeatMode.Limited, () =>
                {
                    me.ShaderProperty(RepeatLimitedRangeA5, "Repeat Limit Box Range A");
                    me.ShaderProperty(RepeatLimitedRangeB5, "Repeat Limit Box Range B");
                });

                me.ShaderProperty(IsOnion5, "Onion Mode");
                EnabledWhen(IsOnion5, true, () =>
                {
                    me.ShaderProperty(OnionThickness5, "Onion Thickness");
                });

                me.ShaderProperty(Round5, "Corner Round");

                EnabledWhen(Shape5, Shape.Box, () =>
                {
                    me.ShaderProperty(BoxSize5, "Box Size (WH)");
                });

                EnabledWhen(Shape5, Shape.IsoscelesTriangle, () =>
                {
                    me.ShaderProperty(TriangleSize5, "Triangle Size (WH)");
                });

                EnabledWhen(Shape5, Shape.Segment, () =>
                {
                    me.ShaderProperty(SegmentA5, "Segment A");
                    me.ShaderProperty(SegmentB5, "Segment B");
                    me.ShaderProperty(SegmentThickness5, "Segment Thickness");
                });

                EnabledWhen(Shape5, Shape.Pie, () =>
                {
                    me.ShaderProperty(PieAngle5, "Pie Angle");
                });
            });
        }

        private void OnShape6Gui(MaterialEditor me)
        {
            OnFoldOutGui(Category.Shape6, () =>
            {
                me.ShaderProperty(Shape6, "Shape");
                me.ShaderProperty(CombinationFunction6, "Combination Function");
                me.ShaderProperty(PositionOffset6, "Position Transform");
                me.ShaderProperty(RotationAngle6, "Rotation");
                me.ShaderProperty(Scale6, "Scale");

                me.ShaderProperty(RepeatMode6, "Repeat Mode");
                DisabledWhen(RepeatMode6, RepeatMode.None, () =>
                {
                    me.ShaderProperty(RepeatPeriod6, "Repeat Period");
                });
                EnabledWhen(RepeatMode6, RepeatMode.Limited, () =>
                {
                    me.ShaderProperty(RepeatLimitedRangeA6, "Repeat Limit Box Range A");
                    me.ShaderProperty(RepeatLimitedRangeB6, "Repeat Limit Box Range B");
                });

                me.ShaderProperty(IsOnion6, "Onion Mode");
                EnabledWhen(IsOnion6, true, () =>
                {
                    me.ShaderProperty(OnionThickness6, "Onion Thickness");
                });

                me.ShaderProperty(Round6, "Corner Round");

                EnabledWhen(Shape6, Shape.Box, () =>
                {
                    me.ShaderProperty(BoxSize6, "Box Size (WH)");
                });

                EnabledWhen(Shape6, Shape.IsoscelesTriangle, () =>
                {
                    me.ShaderProperty(TriangleSize6, "Triangle Size (WH)");
                });

                EnabledWhen(Shape6, Shape.Segment, () =>
                {
                    me.ShaderProperty(SegmentA6, "Segment A");
                    me.ShaderProperty(SegmentB6, "Segment B");
                    me.ShaderProperty(SegmentThickness6, "Segment Thickness");
                });

                EnabledWhen(Shape6, Shape.Pie, () =>
                {
                    me.ShaderProperty(PieAngle6, "Pie Angle");
                });
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
            [EnumMember(Value = "Main Color")]
            Color = 1,

            Stencil,

            [EnumMember(Value = "1st Shape")]
            Shape1,

            [EnumMember(Value = "2nd Shape")]
            Shape2,

            [EnumMember(Value = "3rs Shape")]
            Shape3,

            [EnumMember(Value = "4th Shape")]
            Shape4,

            [EnumMember(Value = "5th Shape")]
            Shape5,

            [EnumMember(Value = "6th Shape")]
            Shape6,
        }

        // ReSharper disable InconsistentNaming

        private MaterialProperty MainTexture;
        private MaterialProperty MainColor;
        private MaterialProperty AlphaTransparency;
        private MaterialProperty IsOutlined;
        private MaterialProperty OutlineColor;
        private MaterialProperty Shape1;
        private MaterialProperty PositionOffset1;
        private MaterialProperty RotationAngle1;
        private MaterialProperty Scale1;
        private MaterialProperty RepeatMode1;
        private MaterialProperty RepeatPeriod1;
        private MaterialProperty RepeatLimitedRangeA1;
        private MaterialProperty RepeatLimitedRangeB1;
        private MaterialProperty IsOnion1;
        private MaterialProperty OnionThickness1;
        private MaterialProperty Round1;
        private MaterialProperty BoxSize1;
        private MaterialProperty TriangleSize1;
        private MaterialProperty SegmentA1;
        private MaterialProperty SegmentB1;
        private MaterialProperty SegmentThickness1;
        private MaterialProperty PieAngle1;
        private MaterialProperty Shape2;
        private MaterialProperty CombinationFunction2;
        private MaterialProperty PositionOffset2;
        private MaterialProperty RotationAngle2;
        private MaterialProperty Scale2;
        private MaterialProperty RepeatMode2;
        private MaterialProperty RepeatPeriod2;
        private MaterialProperty RepeatLimitedRangeA2;
        private MaterialProperty RepeatLimitedRangeB2;
        private MaterialProperty IsOnion2;
        private MaterialProperty OnionThickness2;
        private MaterialProperty Round2;
        private MaterialProperty BoxSize2;
        private MaterialProperty TriangleSize2;
        private MaterialProperty SegmentA2;
        private MaterialProperty SegmentB2;
        private MaterialProperty SegmentThickness2;
        private MaterialProperty PieAngle2;
        private MaterialProperty Shape3;
        private MaterialProperty CombinationFunction3;
        private MaterialProperty PositionOffset3;
        private MaterialProperty RotationAngle3;
        private MaterialProperty Scale3;
        private MaterialProperty RepeatMode3;
        private MaterialProperty RepeatPeriod3;
        private MaterialProperty RepeatLimitedRangeA3;
        private MaterialProperty RepeatLimitedRangeB3;
        private MaterialProperty IsOnion3;
        private MaterialProperty OnionThickness3;
        private MaterialProperty Round3;
        private MaterialProperty BoxSize3;
        private MaterialProperty TriangleSize3;
        private MaterialProperty SegmentA3;
        private MaterialProperty SegmentB3;
        private MaterialProperty SegmentThickness3;
        private MaterialProperty PieAngle3;
        private MaterialProperty Shape4;
        private MaterialProperty CombinationFunction4;
        private MaterialProperty PositionOffset4;
        private MaterialProperty RotationAngle4;
        private MaterialProperty Scale4;
        private MaterialProperty RepeatMode4;
        private MaterialProperty RepeatPeriod4;
        private MaterialProperty RepeatLimitedRangeA4;
        private MaterialProperty RepeatLimitedRangeB4;
        private MaterialProperty IsOnion4;
        private MaterialProperty OnionThickness4;
        private MaterialProperty Round4;
        private MaterialProperty BoxSize4;
        private MaterialProperty TriangleSize4;
        private MaterialProperty SegmentA4;
        private MaterialProperty SegmentB4;
        private MaterialProperty SegmentThickness4;
        private MaterialProperty PieAngle4;
        private MaterialProperty Shape5;
        private MaterialProperty CombinationFunction5;
        private MaterialProperty PositionOffset5;
        private MaterialProperty RotationAngle5;
        private MaterialProperty Scale5;
        private MaterialProperty RepeatMode5;
        private MaterialProperty RepeatPeriod5;
        private MaterialProperty RepeatLimitedRangeA5;
        private MaterialProperty RepeatLimitedRangeB5;
        private MaterialProperty IsOnion5;
        private MaterialProperty OnionThickness5;
        private MaterialProperty Round5;
        private MaterialProperty BoxSize5;
        private MaterialProperty TriangleSize5;
        private MaterialProperty SegmentA5;
        private MaterialProperty SegmentB5;
        private MaterialProperty SegmentThickness5;
        private MaterialProperty PieAngle5;
        private MaterialProperty Shape6;
        private MaterialProperty CombinationFunction6;
        private MaterialProperty PositionOffset6;
        private MaterialProperty RotationAngle6;
        private MaterialProperty Scale6;
        private MaterialProperty RepeatMode6;
        private MaterialProperty RepeatPeriod6;
        private MaterialProperty RepeatLimitedRangeA6;
        private MaterialProperty RepeatLimitedRangeB6;
        private MaterialProperty IsOnion6;
        private MaterialProperty OnionThickness6;
        private MaterialProperty Round6;
        private MaterialProperty BoxSize6;
        private MaterialProperty TriangleSize6;
        private MaterialProperty SegmentA6;
        private MaterialProperty SegmentB6;
        private MaterialProperty SegmentThickness6;
        private MaterialProperty PieAngle6;
        private MaterialProperty StencilRef;
        private MaterialProperty StencilComp;
        private MaterialProperty StencilPass;
        private MaterialProperty StencilFail;
        private MaterialProperty StencilZFail;
        private MaterialProperty StencilReadMask;
        private MaterialProperty StencilWriteMask;
        private MaterialProperty Culling;
        private MaterialProperty _ZTest;
        private MaterialProperty _ZWrite;
        private MaterialProperty FoldoutStatus1;
        private MaterialProperty FoldoutStatus2;

        // ReSharper restore InconsistentNaming
    }
}