﻿#if SHARPX_COMPILER

using NatsunekoLaboratory.SakuraShader.MotionGraphics.Shared;

using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Primitives;
using SharpX.Library.ShaderLab.Statements;

namespace NatsunekoLaboratory.SakuraShader.MotionGraphics.Shader
{
    [Export("frag")]
    public class FragmentShader
    {

        #region 2D Distance Field Functions

        // 2D Signed Distance Field Functions by Inigo Quilez (iq) - https://www.iquilezles.org/www/articles/distfunctions2d/distfunctions2d.htm

        private SlFloat Dot2(SlFloat2 v)
        {
            return Dot(v, v);
        }

        private SlFloat SdCircle(SlFloat2 p, SlFloat r)
        {
            return Length(p) - r;
        }

        private SlFloat SdBox(SlFloat2 p, SlFloat width, SlFloat height)
        {
            var b = new SlFloat2(width, height);
            var d = Abs(p) - b;
            return Length(Max(d, new SlFloat2(0.0f, 0.0f))) + Min(Max(d.X, d.Y), 0.0f);
        }

        private SlFloat SdSegment(SlFloat2 p, SlFloat2 a, SlFloat2 b, SlFloat thickness)
        {
            var pa = p - a;
            var ba = b - a;
            var h = Saturate(Dot(pa, ba) / Dot(ba, ba));
            return Length(pa - ba * h) - thickness;
        }

        private SlFloat SdEquilateralTriangle(SlFloat2 i)
        {
            var k = Sqrt(3.0f);
            var p = new SlFloat2(Abs(i.X) - 1.0f, i.Y + 1.0f / k);
            p = Lerp(p, new SlFloat2(p.X - k * p.Y, -k * p.X - p.Y) / 2.0f, 1 - Step(p.X + k * p.Y, 0.0f));
            p.X -= Clamp(p.X, -2.0f, 0.0f);

            return -Length(p) * Sign(p.Y);
        }

        private SlFloat SdIsoscelesTriangle(SlFloat2 i, SlFloat2 q)
        {
            var p = new SlFloat2(Abs(i.X), i.Y);
            var a = p - q * Saturate(Dot(p, q) / Dot(q, q));
            var b = p - q * new SlFloat2(Saturate(p.X / q.X), 1.0f);
            var s = -Sign(q.Y);
            var d = Min(new SlFloat2(Dot(a, a), s * (p.X * q.Y - p.Y * q.X)), new SlFloat2(Dot(b, b), s * (p.Y - q.Y)));

            return -Sqrt(d.X) * Sign(d.Y);
        }

        private SlFloat SdPentagon(SlFloat2 op, SlFloat r)
        {
            var k = new SlFloat3(0.809016994f, 0.587785252f, 0.726542528f);
            var p = new SlFloat2(Abs(op.X), op.Y);
            p -= 2.0f * Min(Dot(new SlFloat2(-k.X, k.Y), p), 0.0f) * new SlFloat2(-k.X, k.Y);
            p -= 2.0f * Min(Dot(new SlFloat2(k.X, k.Y), p), 0.0f) * new SlFloat2(k.X, k.Y);
            p -= new SlFloat2(Clamp(p.X, -r * k.Z, r * k.Z), r);

            return Length(p) * Sign(p.Y);
        }

        private SlFloat SdHexagon(SlFloat2 op, SlFloat r)
        {
            var k = new SlFloat3(-0.866025404f, 0.5f, 0.577350269f);
            var p = Abs(op);
            p -= 2.0f * Min(Dot(k.XY, p), 0.0f) * k.XY;
            p -= new SlFloat2(Clamp(p.X, -k.Z * r, k.Z * r), r);

            return Length(p) * Sign(p.Y);
        }

        private SlFloat SdStar(SlFloat2 op, SlFloat r, SlFloat rf)
        {
            var k1 = new SlFloat2(0.809016994375f, -0.587785252292f);
            var k2 = new SlFloat2(-k1.X, k1.Y);
            var p = new SlFloat2(Abs(op.X), op.Y);
            p -= 2.0f * Max(Dot(k1, p), 0.0f) * k1;
            p -= 2.0f * Max(Dot(k2, p), 0.0f) * k2;
            p.X = Abs(p.X);
            p.Y -= r;

            var ba = new SlFloat2(-k1.Y, k1.X) * rf - new SlFloat2(0, 1);
            var h = Clamp(Dot(p, ba) / Dot(ba, ba), 0.0f, r);

            return Length(p - ba * h) * Sign(p.Y * ba.X - p.X * ba.Y);
        }

        private SlFloat SdHeart(SlFloat2 op)
        {
            var p = new SlFloat2(Abs(op.X), op.Y);
            var a = Sqrt(Dot2(p - new SlFloat2(0.25f, 0.75f))) - Sqrt(2.0f) / 4.0f;
            var b = Sqrt(Min(Dot2(p - new SlFloat2(0f, 1f)), Dot2(p - 0.5f * Max(p.X + p.Y, 0.0f)))) * Sign(p.X - p.Y);

            return Lerp(a, b, Operator.LessThanOrEquals(p.X + p.Y, 1.0f));
        }

        private SlFloat SdPie(SlFloat2 op, SlFloat2 c, SlFloat r)
        {
            var p = new SlFloat2(Abs(op.X), op.Y);
            var l = Length(p) - r;
            var m = Length(p - c * Clamp(Dot(p, c), 0.0f, r));

            return Max(l, m * Sign(c.Y * p.X - c.X * p.Y));
        }

        #endregion

        #region 2D Distance Field Operators

        private SlFloat2 OpTransform(SlFloat2 a, SlFloat2 o)
        {
            return a + o;
        }

        private SlFloat2 OpRotate(SlFloat2 a, SlFloat r)
        {
            var mat = new SlFloat2x2(Cos(r), -Sin(r), Sin(r), Cos(r));
            return Mul<SlFloat2>(mat, a);
        }

        private SlFloat OpRound(SlFloat a, SlFloat r)
        {
            return a - r;
        }

        private SlFloat OpUnion(SlFloat a, SlFloat b)
        {
            return Min(a, b);
        }

        private SlFloat OpSmoothUnion(SlFloat a, SlFloat b, SlFloat c)
        {
            var h = Saturate(0.5f + 0.5f * (b - a) / c);
            return Lerp(b, a, h) - c * h * (1.0f - h);
        }

        private SlFloat OpInterpolation(SlFloat a, SlFloat b, SlFloat c)
        {
			return Lerp(a, b, c);
		}

        private SlFloat OpSubtraction(SlFloat a, SlFloat b)
        {
            return Max(-a, b);
        }

        private SlFloat OpIntersection(SlFloat a, SlFloat b)
        {
            return Max(a, b);
        }

        private SlFloat OpDifference(SlFloat a, SlFloat b)
        {
            return Max(a, -b);
        }

        private SlFloat OpOnion(SlFloat a, SlFloat r)
        {
            return Abs(a) - r;
        }

        private SlFloat2 OpRepeatLimitation(SlFloat2 p, SlFloat s, SlFloat2 a, SlFloat2 b)
        {
            return p - s * Clamp(Round(p / s), a, b);
        }

        private SlFloat2 OpRepeatInfinity(SlFloat2 p, SlFloat s)
        {
            // this infinity repeat function only works in the positive direction and needs to be modified.
			return Fmod(p + s * 0.5f, new SlFloat2(s, s)) - s * new SlFloat2(0.5f, 0.5f);
		}

        #endregion

        private SlFloat MakeShape(SlFloat2 position, SdfOptions options)
        {
            Compiler.AnnotatedStatement("branch", () => { });
            switch (options.Shape)
            {
                case Shape.Circle:
                    return SdCircle(position, 0.25f);

                case Shape.Box:
                    return SdBox(position, options.BoxWidth, options.BoxHeight);

                case Shape.EquilateralTriangle:
                    return SdEquilateralTriangle(position);

                case Shape.IsoscelesTriangle:
                    return SdIsoscelesTriangle(position, new SlFloat2(options.TriangleWidth, options.TriangleHeight));

                case Shape.Pentagon:
                    return SdPentagon(position, 0.25f);

                case Shape.Hexagon:
                    return SdHexagon(position, 1f);

                case Shape.Star:
                    return SdStar(position, 0.7f, 2.0f);

                case Shape.Heart:
                    return SdHeart(position);

                case Shape.Segment:
                    return SdSegment(position, options.SegmentA - options.PositionOffset, options.SegmentB - options.PositionOffset, options.SegmentThickness);

                case Shape.Pie:
                    return SdPie(position, new SlFloat2(Sin(Radians(options.PieAngle)), Cos(Radians(options.PieAngle))), 0.5f);

                default:
                    return 1f;
            }
        }

        private SlFloat MakeProcessedShape(SlFloat2 coord, SdfOptions options)
        {
            if (options.Scale == 0)
                return 1f;

            var a = OpTransform(coord, options.PositionOffset);
            var b = OpRotate(a, Radians(options.RotationAngle));
            var c = Lerp(b, OpRepeatInfinity(b, options.RepeatPeriod), options.IsRepeatInfinity ? 1 : 0);
            var d = Lerp(b, OpRepeatLimitation(b, options.RepeatPeriod, options.RepeatLimitedRangeA, options.RepeatLimitedRangeB), options.IsRepeatLimited ? 1 : 0);
            var e = Lerp(Lerp(b, c, options.IsRepeatInfinity ? 1 : 0), d, options.IsRepeatLimited ? 1 : 0);
            var f = MakeShape(e / options.Scale, options) * options.Scale;
            var g = Lerp(f, OpOnion(f, options.OnionThickness), options.IsOnion ? 1 : 0);
            var h = OpRound(g, options.Round);

            return h;
        }

        private SlFloat MakeCombine(SlFloat a, SlFloat b, SdfOptions c)
        {
            Compiler.AnnotatedStatement("branch", () => { });
            switch (c.Combination)
            {
                case CombinationFunction.Union:
                    return OpUnion(a, b);

                case CombinationFunction.SmoothUnion:
                    return OpSmoothUnion(a, b, 0.5f);

                case CombinationFunction.Subtraction:
                    return OpSubtraction(a, b);

                case CombinationFunction.Intersection:
                    return OpIntersection(a, b);

                case CombinationFunction.Difference:
                    return OpDifference(a, b);

                case CombinationFunction.Interpolation:
                    return OpInterpolation(a, b, c.CombinationRate);

                default:
                    return a;
            }
        }

        private SlFloat MakeScene(SlFloat2 coord, SdfOptions options1, SdfOptions options2, SdfOptions options3, SdfOptions options4, SdfOptions options5, SdfOptions options6)
        {
            var a = MakeProcessedShape(coord, options1);
            var b = MakeProcessedShape(coord, options2);
            var c = MakeProcessedShape(coord, options3);
            var d = MakeProcessedShape(coord, options4);
            var e = MakeProcessedShape(coord, options5);
            var f = MakeProcessedShape(coord, options6);

            var g = MakeCombine(a, b, options2);
            var h = MakeCombine(g, c, options3);
            var i = MakeCombine(h, d, options4);
            var j = MakeCombine(i, e, options5);
            var k = MakeCombine(j, f, options6);

            return k;
        }

        private SdfOptions MakeOptionsFromShaderUniforms(Shape shape, CombinationFunction combination, SlFloat combinationRate, SlFloat4 offset, SlFloat angle, SlFloat scale, SlFloat period, RepeatMode repeat, SlFloat4 rangeA, SlFloat4 rangeB, SlBool isOnion, SlFloat onionThickness, SlFloat round, SlFloat4 box, SlFloat4 tri, SlFloat4 segA, SlFloat4 segB, SlFloat segmentThickness, SlFloat pie)
        {
            return new SdfOptions
            {
                Shape = shape,
                Combination = combination,
                CombinationRate =combinationRate,
                PositionOffset = offset.XY,
                RotationAngle = angle,
                Scale = scale,
                RepeatPeriod = period,
                IsRepeatInfinity = repeat == RepeatMode.Infinity,
                IsRepeatLimited = repeat == RepeatMode.Limited,
                RepeatLimitedRangeA = rangeA.XY,
                RepeatLimitedRangeB = rangeB.XY,
                IsOnion = isOnion,
                OnionThickness = onionThickness,
                Round = round,
                BoxWidth = box.X,
                BoxHeight = box.Y,
                TriangleWidth = tri.X,
                TriangleHeight = tri.Y,
                SegmentA = segA.XY,
                SegmentB = segB.XY,
                SegmentThickness = segmentThickness,
                PieAngle = pie
            };
        }

        [FragmentShader]
        [return: Semantic("SV_Target")]
        public Color FragmentMain(Vertex2Fragment i)
        {
            var baseColor = Tex2D(ShaderProperties.MainTexture, i.TexCoord) * ShaderProperties.MainColor;
            var color = new Color(baseColor.XYZ, baseColor.A * ShaderProperties.AlphaTransparency);

            var options1 = MakeOptionsFromShaderUniforms(ShaderProperties.Shape1, CombinationFunction.Union, 1, ShaderProperties.PositionOffset1, ShaderProperties.RotationAngle1, ShaderProperties.Scale1, ShaderProperties.RepeatPeriod1, ShaderProperties.RepeatMode1, ShaderProperties.RepeatLimitedRangeA1, ShaderProperties.RepeatLimitedRangeB1, ShaderProperties.IsOnion1, ShaderProperties.OnionThickness1, ShaderProperties.Round1, ShaderProperties.BoxSize1, ShaderProperties.TriangleSize1, ShaderProperties.SegmentA1, ShaderProperties.SegmentB1, ShaderProperties.SegmentThickness1, ShaderProperties.PieAngle1);
            var options2 = MakeOptionsFromShaderUniforms(ShaderProperties.Shape2, ShaderProperties.CombinationFunction2, ShaderProperties.CombinationRate2, ShaderProperties.PositionOffset2, ShaderProperties.RotationAngle2, ShaderProperties.Scale2, ShaderProperties.RepeatPeriod2, ShaderProperties.RepeatMode2, ShaderProperties.RepeatLimitedRangeA2, ShaderProperties.RepeatLimitedRangeB2, ShaderProperties.IsOnion2, ShaderProperties.OnionThickness2, ShaderProperties.Round2, ShaderProperties.BoxSize2, ShaderProperties.TriangleSize2, ShaderProperties.SegmentA2, ShaderProperties.SegmentB2, ShaderProperties.SegmentThickness2, ShaderProperties.PieAngle2);
            var options3 = MakeOptionsFromShaderUniforms(ShaderProperties.Shape3, ShaderProperties.CombinationFunction3, ShaderProperties.CombinationRate3, ShaderProperties.PositionOffset3, ShaderProperties.RotationAngle3, ShaderProperties.Scale3, ShaderProperties.RepeatPeriod3, ShaderProperties.RepeatMode3, ShaderProperties.RepeatLimitedRangeA3, ShaderProperties.RepeatLimitedRangeB3, ShaderProperties.IsOnion3, ShaderProperties.OnionThickness3, ShaderProperties.Round3, ShaderProperties.BoxSize3, ShaderProperties.TriangleSize3, ShaderProperties.SegmentA3, ShaderProperties.SegmentB3, ShaderProperties.SegmentThickness3, ShaderProperties.PieAngle3);
            var options4 = MakeOptionsFromShaderUniforms(ShaderProperties.Shape4, ShaderProperties.CombinationFunction4, ShaderProperties.CombinationRate4, ShaderProperties.PositionOffset4, ShaderProperties.RotationAngle4, ShaderProperties.Scale4, ShaderProperties.RepeatPeriod4, ShaderProperties.RepeatMode4, ShaderProperties.RepeatLimitedRangeA4, ShaderProperties.RepeatLimitedRangeB4, ShaderProperties.IsOnion4, ShaderProperties.OnionThickness4, ShaderProperties.Round4, ShaderProperties.BoxSize4, ShaderProperties.TriangleSize4, ShaderProperties.SegmentA4, ShaderProperties.SegmentB4, ShaderProperties.SegmentThickness4, ShaderProperties.PieAngle4);
            var options5 = MakeOptionsFromShaderUniforms(ShaderProperties.Shape5, ShaderProperties.CombinationFunction5, ShaderProperties.CombinationRate5, ShaderProperties.PositionOffset5, ShaderProperties.RotationAngle5, ShaderProperties.Scale5, ShaderProperties.RepeatPeriod5, ShaderProperties.RepeatMode5, ShaderProperties.RepeatLimitedRangeA5, ShaderProperties.RepeatLimitedRangeB5, ShaderProperties.IsOnion5, ShaderProperties.OnionThickness5, ShaderProperties.Round5, ShaderProperties.BoxSize5, ShaderProperties.TriangleSize5, ShaderProperties.SegmentA5, ShaderProperties.SegmentB5, ShaderProperties.SegmentThickness5, ShaderProperties.PieAngle5);
            var options6 = MakeOptionsFromShaderUniforms(ShaderProperties.Shape6, ShaderProperties.CombinationFunction6, ShaderProperties.CombinationRate6, ShaderProperties.PositionOffset6, ShaderProperties.RotationAngle6, ShaderProperties.Scale6, ShaderProperties.RepeatPeriod6, ShaderProperties.RepeatMode6, ShaderProperties.RepeatLimitedRangeA6, ShaderProperties.RepeatLimitedRangeB6, ShaderProperties.IsOnion6, ShaderProperties.OnionThickness6, ShaderProperties.Round6, ShaderProperties.BoxSize6, ShaderProperties.TriangleSize6, ShaderProperties.SegmentA6, ShaderProperties.SegmentB6, ShaderProperties.SegmentThickness6, ShaderProperties.PieAngle6);

            var position = Frac(i.TexCoord) * 2 - 1;
            var a = MakeScene(position, options1, options2, options3, options4, options5, options6);
            var b = Lerp(new SlFloat4(1, 1, 1, 0), color, Operator.LessThanOrEquals(a, 0.0f));
            var c = Lerp(b, ShaderProperties.OutlineColor, ShaderProperties.IsOutlined ? Operator.LessThanOrEquals(Abs(a), 0.001f) : 0);

            return c;
        }
    }
}

#endif