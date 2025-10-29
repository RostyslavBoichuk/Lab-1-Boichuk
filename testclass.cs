using System;
using System.Collections.Generic;
using MatrixLib;
using Xunit;

namespace MatrixLib.Tests
{
    public class MatrixTests
    {
        [Fact]
        public void Constructor_CreatesCorrectDimensions()
        {
            Matrix matrix = new(2, 3);
            Assert.Equal(2, matrix.Rows);
            Assert.Equal(3, matrix.Cols);
        }

        [Fact]
        public void Constructor_FromArray_CopiesValues()
        {
            double[,] arr = { { 1, 2 }, { 3, 4 } };
            Matrix matrix = new(arr);
            Assert.Equal(1, matrix[0, 0]);
            Assert.Equal(4, matrix[1, 1]);
        }

        [Fact]
        public void Add_TwoMatrices_ReturnsCorrectSum()
        {
            Matrix matrix1 = new(new double[,] { { 1, 2 }, { 3, 4 } });
            Matrix matrix2 = new(new double[,] { { 5, 6 }, { 7, 8 } });
            Matrix resultMatrix = matrix1.Add(matrix2);
            Matrix expectedMatrix = new(new double[,] { { 6, 8 }, { 10, 12 } });

            Assert.Equal(expectedMatrix, resultMatrix);
        }

        [Fact]
        public void Subtract_TwoMatrices_ReturnsCorrectDifference()
        {
            Matrix matrix1 = new(new double[,] { { 5, 6 }, { 7, 8 } });
            Matrix matrix2 = new(new double[,] { { 1, 2 }, { 3, 4 } });
            Matrix resultMatrix = matrix1.Subtract(matrix2);
            Matrix expectedMatrix = new(new double[,] { { 4, 4 }, { 4, 4 } });

            Assert.Equal(expectedMatrix, resultMatrix);
        }

        [Fact]
        public void Multiply_ByZeroMatrix_ReturnsZeroMatrix()
        {
            Matrix matrix1 = new(new double[,] { { 1, 2 }, { 3, 4 } });
            Matrix zeroMatrix = new(2, 2);
            Matrix resultMatrix = matrix1.Multiply(zeroMatrix);

            Assert.Equal(zeroMatrix, resultMatrix);
        }

        [Fact]
        public void Multiply_IncompatibleMatrices_Throws()
        {
            Matrix matrix1 = new(2, 3);
            Matrix matrix2 = new(2, 2);
            Assert.Throws<ArgumentException>(() => matrix1.Multiply(matrix2));
        }

        [Fact]
        public void Transpose_SquareMatrix_ReturnsTransposed()
        {
            Matrix matrix = new(new double[,] { { 1, 2 }, { 3, 4 } });
            Matrix resultMatrix = matrix.Transpose();
            Matrix expectedMatrix = new(new double[,] { { 1, 3 }, { 2, 4 } });

            Assert.Equal(expectedMatrix, resultMatrix);
        }

        [Fact]
        public void Transpose_RectangularMatrix_ReturnsTransposed()
        {
            Matrix matrix = new(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            Matrix resultMatrix = matrix.Transpose();
            Matrix expectedMatrix = new(new double[,] { { 1, 4 }, { 2, 5 }, { 3, 6 } });

            Assert.Equal(expectedMatrix, resultMatrix);
        }

        [Fact]
        public void Equals_TwoIdenticalMatrices_ReturnsTrue()
        {
            Matrix matrix1 = new(new double[,] { { 1, 2 }, { 3, 4 } });
            Matrix matrix2 = new(new double[,] { { 1, 2 }, { 3, 4 } });

            Assert.True(matrix1.Equals(matrix2));
        }

        [Fact]
        public void Equals_TwoDifferentMatrices_ReturnsFalse()
        {
            Matrix matrix1 = new(new double[,] { { 1, 2 }, { 3, 4 } });
            Matrix matrix2 = new(new double[,] { { 4, 3 }, { 2, 1 } });

            Assert.False(matrix1.Equals(matrix2));
        }
    }
}
