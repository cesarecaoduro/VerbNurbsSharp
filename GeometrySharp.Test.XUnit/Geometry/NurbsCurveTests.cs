﻿using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using GeometrySharp.Core;
using GeometrySharp.Geometry;
using GeometrySharp.Test.XUnit.Core;
using Xunit;
using Xunit.Abstractions;

namespace GeometrySharp.Test.XUnit.Geometry
{
    [Trait("Category", "NurbsCurve")]
    public class NurbsCurveTests
    {
        private readonly ITestOutputHelper _testOutput;

        public NurbsCurveTests(ITestOutputHelper testOutput)
        {
            _testOutput = testOutput;
        }

        public static (int degree, List<Vector3> pts, Knot knots, List<double> weights) CurveData =>
        (
            2,
            new List<Vector3>()
            {
                new Vector3(){-10,15,5},
                new Vector3(){10,5,5},
                new Vector3(){20,0,0}
            },
            new Knot() { 1, 1, 1, 1, 1, 1 },
            new List<double>() { 0.5, 0.5, 0.5 }
        );

        public static NurbsCurve NurbsCurveExample()
        {
            int degree = 2;
            List<Vector3> pts = new List<Vector3>()
            {
                new Vector3(){-10,15,5},
                new Vector3(){10,5,5},
                new Vector3(){20,0,0}
            };
            Knot knots = new Knot() { 0, 0, 0, 1, 1, 1 };

            return new NurbsCurve(degree, knots, pts);
        }

        public static NurbsCurve NurbsCurvePtsAndWeightsExample()
        {
            int degree = 2;
            List<Vector3> pts = new List<Vector3>()
            {
                new Vector3(){-10,15,5},
                new Vector3(){10,5,5},
                new Vector3(){20,0,0}
            };
            Knot knots = new Knot() { 1, 1, 1, 1, 1, 1 };
            var weights = new List<double>() { 0.5, 0.5, 0.5 };

            return new NurbsCurve(degree, knots, pts, weights);
        }

        public static NurbsCurve NurbsCurveExample2()
        {
            var knots = new Knot() { 0.0, 0.0, 0.0, 0.0, 0.333333, 0.666667, 1.0, 1.0, 1.0, 1.0 };
            var degree = 3;
            var controlPts = new List<Vector3>()
            {
                new Vector3() {5,5,0},
                new Vector3() {10, 10, 0},
                new Vector3() {20, 15, 0},
                new Vector3() {35, 15, 0},
                new Vector3() {45, 10, 0},
                new Vector3() {50, 5, 0}
            };
            return new NurbsCurve(degree, knots, controlPts);
        }

        [Fact]
        public void It_Returns_A_NurbsCurve()
        {
            var nurbsCurve = NurbsCurveExample2();

            nurbsCurve.Should().NotBeNull();
            nurbsCurve.Degree.Should().Be(3);
            nurbsCurve.Weights.Should().BeEquivalentTo(Sets.RepeatData(1.0, 6));
        }

        [Fact]
        public void It_Returns_A_NurbsCurve_Evaluated_With_A_List_Of_Weights()
        {
            var nurbsCurve = NurbsCurvePtsAndWeightsExample();

            nurbsCurve.Should().NotBeNull();
            nurbsCurve.HomogenizedPoints[2].Should().BeEquivalentTo(new Vector3() {10, 0, 0, 0.5});
            nurbsCurve.ControlPoints[2].Should().BeEquivalentTo(new Vector3() { 20, 0, 0 });
        }

        [Fact]
        public void It_Returns_A_NurbsCurve_From_ControlPoints_And_Degree()
        {
            var nurbsCurve = new NurbsCurve(CurveData.pts, CurveData.degree);

            nurbsCurve.Should().NotBeNull();
            nurbsCurve.Degree.Should().Be(2);
            nurbsCurve.Weights.Should().BeEquivalentTo(Sets.RepeatData(1.0, CurveData.pts.Count));
            nurbsCurve.Knots.Should().BeEquivalentTo(new Knot(CurveData.degree, CurveData.pts.Count));
        }

        [Fact]
        public void NurbsCurve_Throws_An_Exception_If_ControlPoints_Are_Null()
        {
            Func<NurbsCurve> curve = () => new NurbsCurve(CurveData.degree, CurveData.knots, null, CurveData.weights);

            curve.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void NurbsCurve_Throws_An_Exception_If_Knots_Are_Null()
        {
            Func<NurbsCurve> curve = () => new NurbsCurve(CurveData.degree, null, CurveData.pts, CurveData.weights);

            curve.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void NurbsCurve_Throws_An_Exception_If_Degree_is_Less_Than_1()
        {
            Func<NurbsCurve> curve = () => new NurbsCurve(0, CurveData.knots, CurveData.pts, CurveData.weights);

            curve.Should().Throw<ArgumentException>()
                .WithMessage("Degree must be greater than 1!");
        }

        // Confirm the relations between degree(p), number of control points(n+1), and the number of knots(m+1).
        // m = p + n + 1
        [Fact]
        public void NurbsCurve_Throws_An_Exception_If_Is_Not_Valid_The_Relation_Between_Pts_Degree_Knots()
        {
            Func<NurbsCurve> curve = () => new NurbsCurve(1, CurveData.knots, CurveData.pts, CurveData.weights);

            curve.Should().Throw<ArgumentException>()
                .WithMessage("Number of points + degree + 1 must equal knots length!");
        }

        [Fact]
        public void NurbsCurve_Throws_An_Exception_If_Knots_Are_Not_Valid()
        {
            var knots = new Knot(){0,0,1,1,2,2};
            Func<NurbsCurve> curve = () => new NurbsCurve(CurveData.degree, knots, CurveData.pts, CurveData.weights);

            curve.Should().Throw<ArgumentException>()
                .WithMessage("Invalid knot format! Should begin with degree + 1 repeats and end with degree + 1 repeats!");
        }

        [Fact]
        public void It_Returns_A_Copied_NurbsCurve()
        {
            var nurbsCurve = NurbsCurveExample2();
            var copiedNurbs = nurbsCurve.Clone();

            copiedNurbs.Should().NotBeNull();
            copiedNurbs.Equals(nurbsCurve).Should().BeFalse();
            copiedNurbs.Degree.Should().Be(nurbsCurve.Degree);
            copiedNurbs.Weights.Should().BeEquivalentTo(nurbsCurve.Weights);
        }

        [Fact]
        public void It_Returns_The_Domain_Of_The_Curve()
        {
            var curveDomain = NurbsCurveExample().Domain();

            curveDomain.Min.Should().Be(NurbsCurveExample().Knots.First());
            curveDomain.Max.Should().Be(NurbsCurveExample().Knots.Last());
        }

        [Fact]
        public void It_Returns_A_Transformed_NurbsCurve_By_A_Given_Matrix()
        {
            var curve = NurbsCurveExample2();
            var matrix = MatrixTests.TransformationMatrixExample;

            var transformedCurve = curve.Transform(matrix);

            var pt1 = curve.PointAt(0.5);
            var pt2 = transformedCurve.PointAt(0.5);

            var distanceBetweenPts = System.Math.Round((pt2 - pt1).Length(), 6);

            distanceBetweenPts.Should().Be(22.383029);
        }

        [Theory]
        [InlineData(0.5)]
        [InlineData(3.5)]
        public void It_Splits_A_Curve_Returning_Two_Curves(double parameter)
        {
            // Arrange
            var degree = 3;
            var knots = new Knot() { 0, 0, 0, 0, 1, 2, 3, 4, 5, 5, 5, 5 };
            var controlPts = new List<Vector3>();
            for (int i = 0; i <= knots.Count - 3 - 2; i++)
            {
                controlPts.Add(new Vector3() { i, 0.0, 0.0 });
            }
            var curve = new NurbsCurve(degree, knots, controlPts);
            var weights = curve.Weights;
            // Act
            var splitCurves = curve.Split(parameter);

            // Assert
            for (var i = 0; i < degree + 1; i++)
            {
                var d = splitCurves[0].Knots.Count- (degree + 1);
                splitCurves[0].Knots[d + i].Should().BeApproximately(parameter, GeoSharpMath.TOLERANCE);
            }

            for (var i = 0; i < degree + 1; i++)
            {
                var d = 0;
                splitCurves[1].Knots[d + i].Should().BeApproximately(parameter, GeoSharpMath.TOLERANCE);
            }

            splitCurves.Should().HaveCount(2);
            splitCurves[0].ControlPoints.Last().Should().BeEquivalentTo(splitCurves[1].ControlPoints.First());
        }

        // This values have been compered with Rhino.
        [Theory]
        [InlineData(0.0, new double[] { 0.707107, 0.707107, 0.0 })]
        [InlineData(0.25, new double[] { 0.931457, 0.363851, 0.0 })]
        [InlineData(0.5, new double[] { 1.0, 0.0, 0.0 })]
        [InlineData(0.75, new double[] { 0.931457, -0.363851, 0 })]
        [InlineData(1.0, new double[] { 0.707107, -0.707107, 0.0 })]
        public void It_Returns_The_Tangent_At_Give_Point(double t, double[] tangentData)
        {
            // Verb test
            var degree = 3;
            var knots = new Knot() {0, 0, 0, 0, 0.5, 1, 1, 1, 1};
            List<Vector3> pts = new List<Vector3>()
            {
                new Vector3(){0, 0, 0},
                new Vector3(){1, 0, 0},
                new Vector3(){2, 0, 0},
                new Vector3(){3, 0, 0},
                new Vector3(){4, 0, 0}
            };
            var weights = new List<double>() {1, 1, 1, 1, 1};
            var crv = new NurbsCurve(degree, knots, pts, weights);
            var tangent = crv.Tangent(0.5);

            tangent.Should().BeEquivalentTo(new Vector3() {3, 0, 0});

            // Custom test
            var tangentToCheck = NurbsCurveExample2().Tangent(t);
            var tangentNormalized = tangentToCheck.Normalized();
            var tangentExpected = new Vector3(tangentData);

            tangentNormalized.Should().BeEquivalentTo(tangentExpected, option => option
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, 1e-6))
                .WhenTypeIs<double>());
        }

        // This values have been compered with Rhino.
        [Theory]
        [InlineData(0.0, new double[] { 5.0, 5.0, 0.0 })]
        [InlineData(0.25, new double[] { 16.25, 12.558594, 0.0 })]
        [InlineData(0.5, new double[] { 27.5, 14.6875, 0.0 })]
        [InlineData(0.75, new double[] { 38.75, 12.558594, 0.0 })]
        [InlineData(1.0, new double[] { 50.0, 5.0, 0.0 })]
        public void It_Returns_A_Point_At_The_Value(double t, double[] pointData)
        {
            var pointToCheck = LinearAlgebra.Dehomogenize(NurbsCurveExample2().PointAt(t));
            var pointExpected = new Vector3(pointData);

            pointToCheck.Should().BeEquivalentTo(pointExpected, option => option
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, 1e-5))
                .WhenTypeIs<double>());
        }

        [Fact]
        public  void t()
        {
            var pts = new Array<object>();

            pts.push(new Array<double>(new double[] { 5, 5, 0 }));
            pts.push(new Array<double>(new double[] { 10, 10, 0 }));
            pts.push(new Array<double>(new double[] { 20, 15, 0 }));
            pts.push(new Array<double>(new double[] { 35, 15, 0 }));
            pts.push(new Array<double>(new double[] { 45, 10, 0 }));
            pts.push(new Array<double>(new double[] { 50, 5, 0 }));

            var knots = new Array<double>(new double[] { 0.0, 0.0, 0.0, 0.0, 0.333333, 0.666667, 1.0, 1.0, 1.0, 1.0 });
            var weights = new Array<double>(new double[] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 });

            var curve = verb.geom.NurbsCurve.byKnotsControlPointsWeights(3, knots, pts, weights);
            var curve2 = verb.geom.NurbsCurve.byPoints(pts, 3);

            var ptAt = curve2.point(0.5);
            var vec = verb.core.Vec.normalized(curve2.tangent(0.5));
            var k = curve2.knots();

            for (int i = 0; i < k.length; i++)
            {
                k[i] = System.Math.Round(k[i], 6);
            }

            _testOutput.WriteLine($"{k}");
            _testOutput.WriteLine($"{ptAt[0]},{ptAt[1]},{ptAt[2]}");
        }

    }
}
