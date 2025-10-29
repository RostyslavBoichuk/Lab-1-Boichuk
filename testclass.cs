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



        [Theory]
        [MemberData(nameof(AdditionData))]
        public void Add_Theory_TwoMatrices_ReturnsExpected(Matrix matrix1, Matrix matrix2, Matrix expectedMatrix)
        {
            Matrix resultMatrix = matrix1.Add(matrix2);
            Assert.Equal(expectedMatrix, resultMatrix);
        }

        public static IEnumerable<object[]> AdditionData()
        {
            yield return new object[]
            {
                new Matrix(new double[,] { { 1, 1 }, { 1, 1 } }),
                new Matrix(new double[,] { { 2, 2 }, { 2, 2 } }),
                new Matrix(new double[,] { { 3, 3 }, { 3, 3 } })
            };

            yield return new object[]
            {
                new Matrix(new double[,] { { -1, 5 }, { 3, 0 } }),
                new Matrix(new double[,] { { 1, -2 }, { -3, 4 } }),
                new Matrix(new double[,] { { 0, 3 }, { 0, 4 } })
            };
        }

        [Theory]
        [MemberData(nameof(MultiplicationData))]
        public void Multiply_Theory_ReturnsExpected(Matrix matrix1, Matrix matrix2, Matrix expectedMatrix)
        {
            Matrix resultMatrix = matrix1.Multiply(matrix2);
            Assert.Equal(expectedMatrix, resultMatrix);
        }

        public static IEnumerable<object[]> MultiplicationData()
        {
            yield return new object[]
            {
                new Matrix(new double[,] { { 1, 2 }, { 3, 4 } }),
                new Matrix(new double[,] { { 2, 0 }, { 1, 2 } }),
                new Matrix(new double[,] { { 4, 4 }, { 10, 8 } })
            };

            yield return new object[]
            {
                new Matrix(new double[,] { { 1, 0 }, { 0, 1 } }),
                new Matrix(new double[,] { { 9, 8 }, { 7, 6 } }),
                new Matrix(new double[,] { { 9, 8 }, { 7, 6 } })
            };
        }

        [Theory]
        [MemberData(nameof(TransposeData))]
        public void Transpose_Theory_ReturnsExpected(Matrix inputMatrix, Matrix expectedMatrix)
        {
            Matrix resultMatrix = inputMatrix.Transpose();
            Assert.Equal(expectedMatrix, resultMatrix);
        }

        public static IEnumerable<object[]> TransposeData()
        {
            yield return new object[]
            {
                new Matrix(new double[,] { { 1, 2, 3 } }),
                new Matrix(new double[,] { { 1 }, { 2 }, { 3 } })
            };

            yield return new object[]
            {
                new Matrix(new double[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } }),
                new Matrix(new double[,] { { 1, 3, 5 }, { 2, 4, 6 } })
            };
        }

        [Theory]
        [MemberData(nameof(IdentitySizes))]
        public void Identity_Theory_IsDiagonal(int size)
        {
            Matrix identityMatrix = Matrix.Identity(size);

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    Assert.Equal(i == j ? 1 : 0, identityMatrix[i, j]);
        }

        public static IEnumerable<object[]> IdentitySizes()
        {
            yield return new object[] { 1 };
            yield return new object[] { 2 };
            yield return new object[] { 5 };
        }

        [Theory]
        [MemberData(nameof(AddSubtractReversibilityData))]
        public void AddThenSubtract_ReturnsOriginal(Matrix matrix1, Matrix matrix2)
        {
            Matrix resultMatrix = matrix1.Add(matrix2).Subtract(matrix2);
            Assert.Equal(matrix1, resultMatrix);
        }

        public static IEnumerable<object[]> AddSubtractReversibilityData()
        {
            yield return new object[]
            {
                new Matrix(new double[,] { { 1, 2 }, { 3, 4 } }),
                new Matrix(new double[,] { { 5, 6 }, { 7, 8 } })
            };

            yield return new object[]
            {
                new Matrix(new double[,] { { -1, 0 }, { 0, -1 } }),
                new Matrix(new double[,] { { 2, 3 }, { 4, 5 } })
            };
        }

        [Theory]
        [MemberData(nameof(MultiplyByIdentityData))]
        public void MultiplyByIdentity_ReturnsOriginal(Matrix matrix)
        {
            Matrix identityMatrix = Matrix.Identity(matrix.Rows);
            Matrix resultMatrix = matrix.Multiply(identityMatrix);
            Assert.Equal(matrix, resultMatrix);
        }

        public static IEnumerable<object[]> MultiplyByIdentityData()
        {
            yield return new object[] { new Matrix(new double[,] { { 1, 2 }, { 3, 4 } }) };
            yield return new object[] { new Matrix(new double[,] { { 0, 5 }, { -1, 2 } }) };
            yield return new object[] { new Matrix(new double[,] { { 2 } }) };
        }

        [Theory]
        [MemberData(nameof(TransposeTwiceData))]
        public void TransposeTwice_ReturnsOriginal(Matrix matrix)
        {
            Matrix resultMatrix = matrix.Transpose().Transpose();
            Assert.Equal(matrix, resultMatrix);
        }

        public static IEnumerable<object[]> TransposeTwiceData()
        {
            yield return new object[] { new Matrix(new double[,] { { 1, 2 }, { 3, 4 } }) };
            yield return new object[] { new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } }) };
        }
    }
}
