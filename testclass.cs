using System;
using Xunit;

public class MatrixTests
{
    [Fact]
    public void Constructor_CreatesCorrectDimensions()
    {
        var m = new Matrix(2, 3);
        Assert.Equal(2, m.Rows);
        Assert.Equal(3, m.Cols);
    }

    [Fact]
    public void Constructor_FromArray_CopiesValues()
    {
        double[,] data = { { 1, 2 }, { 3, 4 } };
        var m = new Matrix(data);
        Assert.Equal(2, m.Rows);
        Assert.Equal(2, m.Cols);
        Assert.Equal(3, m[1, 0]);
    }

    [Fact]
    public void Add_TwoMatrices_ReturnsCorrectSum()
    {
        var a = new Matrix(new double[,] { { 1, 2 }, { 3, 4 } });
        var b = new Matrix(new double[,] { { 5, 6 }, { 7, 8 } });
        var sum = a + b;
        Assert.Equal(6, sum[0, 0]);
        Assert.Equal(12, sum[1, 1]);
    }

    [Fact]
    public void Subtract_TwoMatrices_ReturnsCorrectDifference()
    {
        var a = new Matrix(new double[,] { { 5, 5 }, { 5, 5 } });
        var b = new Matrix(new double[,] { { 1, 2 }, { 3, 4 } });
        var diff = a - b;
        Assert.Equal(4, diff[0, 0]);
        Assert.Equal(1, diff[1, 1]);
    }

    [Fact]
    public void Multiply_ByZeroMatrix_ReturnsZeroMatrix()
    {
        var a = new Matrix(new double[,] { { 1, 2 }, { 3, 4 } });
        var b = new Matrix(2, 2);
        var result = a * b;
        Assert.True(result.Equals(new Matrix(2, 2)));
    }

    [Fact]
    public void Multiply_IncompatibleMatrices_Throws()
    {
        var a = new Matrix(2, 3);
        var b = new Matrix(4, 2);
        Assert.Throws<ArgumentException>(() => a * b);
    }

    [Fact]
    public void Transpose_SquareMatrix_ReturnsTransposed()
    {
        var a = new Matrix(new double[,] { { 1, 2 }, { 3, 4 } });
        var t = a.Transpose();
        Assert.Equal(1, t[0, 0]);
        Assert.Equal(3, t[0, 1]);
        Assert.Equal(2, t[1, 0]);
    }

    [Fact]
    public void Transpose_RectangularMatrix_ReturnsTransposed()
    {
        var a = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
        var t = a.Transpose();
        Assert.Equal(3, t.Rows);
        Assert.Equal(2, t.Cols);
        Assert.Equal(4, t[1, 0]);
    }

    [Fact]
    public void Equals_TwoIdenticalMatrices_ReturnsTrue()
    {
        var a = new Matrix(new double[,] { { 1, 2 }, { 3, 4 } });
        var b = new Matrix(new double[,] { { 1, 2 }, { 3, 4 } });
        Assert.True(a.Equals(b));
    }

    [Fact]
    public void Equals_TwoDifferentMatrices_ReturnsFalse()
    {
        var a = new Matrix(new double[,] { { 1, 2 }, { 3, 4 } });
        var b = new Matrix(new double[,] { { 0, 2 }, { 3, 4 } });
        Assert.False(a.Equals(b));
    }

    [Theory]
    [InlineData(1, 2, 3, 4, 5, 6, 7, 8)]
    [InlineData(2, 3, 4, 5, 6, 7, 8, 9)]
    public void Add_Theory_TwoMatrices_ReturnsExpected(double a1, double a2, double a3, double a4, double b1, double b2, double b3, double b4)
    {
        var a = new Matrix(new double[,] { { a1, a2 }, { a3, a4 } });
        var b = new Matrix(new double[,] { { b1, b2 }, { b3, b4 } });
        var sum = a + b;
        Assert.Equal(a1 + b1, sum[0, 0]);
        Assert.Equal(a4 + b4, sum[1, 1]);
    }

    [Theory]
    [InlineData(1, 2, 3, 4)]
    [InlineData(2, 0, 1, 2)]
    public void Multiply_Theory_ReturnsExpected(double a1, double a2, double a3, double a4)
    {
        var a = new Matrix(new double[,] { { a1, a2 }, { a3, a4 } });
        var id = Matrix.Identity(2);
        var result = a * id;
        Assert.Equal(a, result);
    }

    [Theory]
    [InlineData(1, 2, 3, 4)]
    [InlineData(2, 5, 7, 8)]
    public void Transpose_Theory_ReturnsExpected(double a1, double a2, double a3, double a4)
    {
        var a = new Matrix(new double[,] { { a1, a2 }, { a3, a4 } });
        var t = a.Transpose();
        Assert.Equal(a1, t[0, 0]);
        Assert.Equal(a3, t[0, 1]);
        Assert.Equal(a2, t[1, 0]);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void Identity_Theory_IsDiagonal(int size)
    {
        var id = Matrix.Identity(size);
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                Assert.Equal(i == j ? 1.0 : 0.0, id[i, j]);
    }

    [Theory]
    [InlineData(1, 2, 3, 4)]
    [InlineData(5, 6, 7, 8)]
    public void AddThenSubtract_ReturnsOriginal(double a1, double a2, double a3, double a4)
    {
        var a = new Matrix(new double[,] { { a1, a2 }, { a3, a4 } });
        var b = new Matrix(new double[,] { { 1, 1 }, { 1, 1 } });
        var result = (a + b) - b;
        Assert.Equal(a, result);
    }

    [Theory]
    [InlineData(1, 2, 3, 4)]
    [InlineData(2, 3, 4, 5)]
    [InlineData(0, 0, 0, 0)]
    public void MultiplyByIdentity_ReturnsOriginal(double a1, double a2, double a3, double a4)
    {
        var a = new Matrix(new double[,] { { a1, a2 }, { a3, a4 } });
        var id = Matrix.Identity(2);
        var result = a * id;
        Assert.Equal(a, result);
    }

    [Theory]
    [InlineData(1, 2, 3, 4)]
    [InlineData(2, 5, 7, 8)]
    public void TransposeTwice_ReturnsOriginal(double a1, double a2, double a3, double a4)
    {
        var a = new Matrix(new double[,] { { a1, a2 }, { a3, a4 } });
        var t2 = a.Transpose().Transpose();
        Assert.Equal(a, t2);
    }

    [Fact]
    public void Constructor_NegativeDimensions_Throws()
    {
        Assert.Throws<ArgumentException>(() => new Matrix(-1, 2));
    }

    [Fact]
    public void Constructor_ZeroDimensions_CreatesEmptyMatrix()
    {
        var m = new Matrix(0, 0);
        Assert.Equal(0, m.Rows);
        Assert.Equal(0, m.Cols);
    }

    [Fact]
    public void Multiply_IncompatibleDimensions_ThrowsWithMessage()
    {
        var a = new Matrix(2, 3);
        var b = new Matrix(2, 2);
        var ex = Assert.Throws<ArgumentException>(() => a * b);
        Assert.Contains("incompatible", ex.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Add_FloatingPointPrecision_TolerantComparison()
    {
        var a = new Matrix(new double[,] { { 0.1, 0.2 }, { 0.3, 0.4 } });
        var b = new Matrix(new double[,] { { 0.2, 0.1 }, { 0.4, 0.3 } });
        var expected = new Matrix(new double[,] { { 0.3, 0.3 }, { 0.7, 0.7 } });
        var sum = a + b;
        for (int i = 0; i < 2; i++)
            for (int j = 0; j < 2; j++)
                Assert.True(Math.Abs(sum[i, j] - expected[i, j]) < 1e-9);
    }

    [Fact]
    public void Identity_ZeroSize_Throws()
    {
        Assert.Throws<ArgumentException>(() => Matrix.Identity(0));
    }

    [Fact]
    public void Identity_SingleElement_IsOne()
    {
        var id = Matrix.Identity(1);
        Assert.Equal(1, id[0, 0]);
    }
}