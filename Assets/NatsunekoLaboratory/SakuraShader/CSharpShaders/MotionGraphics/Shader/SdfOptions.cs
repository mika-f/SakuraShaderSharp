#if SHARPX_COMPILER

using NatsunekoLaboratory.SakuraShader.MotionGraphics.Shared;

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Primitives;

namespace NatsunekoLaboratory.SakuraShader.MotionGraphics.Shader
{
    [Component]
    [Export("core")]
    public class SdfOptions
    {
        public Shape Shape { get; set; }

        public CombinationFunction Combination { get; set; }

        public SlFloat CombinationRate { get; set; }

        public SlFloat2 PositionOffset { get; set; }

        public SlFloat RotationAngle { get; set; }

        public SlFloat Scale { get; set; }

        public SlFloat RepeatPeriod { get; set; }

        public SlBool IsRepeatInfinity { get; set; }

        public SlBool IsRepeatLimited { get; set; }

        public SlFloat2 RepeatLimitedRangeA { get; set; }

        public SlFloat2 RepeatLimitedRangeB { get; set; }

        public SlBool IsOnion { get; set; }

        public SlFloat OnionThickness { get; set; }

        public SlFloat Round { get; set; }

        public Displacement Displacement { get; set; }

        public SlFloat BoxWidth { get; set; }

        public SlFloat BoxHeight { get; set; }

        public SlFloat TriangleWidth { get; set; }

        public SlFloat TriangleHeight { get; set; }

        public SlFloat2 SegmentA { get; set; }

        public SlFloat2 SegmentB { get; set; }

        public SlFloat SegmentThickness { get; set; }

		public SlFloat PieAngle { get; set; }
    }
}

#endif