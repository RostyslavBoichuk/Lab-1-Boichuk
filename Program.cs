using System;
using System.Text;

namespace MatrixLib
{
    /// <summary>
    /// @brief Represents a 2D matrix supporting arithmetic operations.
    /// @echo This class allows creation, manipulation, and arithmetic operations on matrices.
    /// @echo Supports addition, subtraction, multiplication, transpose, and identity matrix creation.
    /// @example
    /// Matrix a = new Matrix(2, 2);
    /// Matrix b = Matrix.Identity(2);
    /// Matrix c = a.Add(b);
    /// @echo Example shows creating a 2x2 matrix, an identity matrix, and adding them together.
    /// </summary>

    public class Matrix
    {
        private readonly double[,] _data;

        /// <summary>@brief Number of matrix rows.</summary>
        public int Rows { get; }

        /// <summary>@brief Number of matrix columns.</summary>
        public int Cols { get; }

        /// <summary>
    /// @brief Indexer for accessing matrix elements.
    /// @param i Row index.
    /// @param j Column index.
    /// @return Value at position (i, j).
    /// @echo Allows reading and writing individual matrix elements using [i,j] syntax.
    /// </summary>
        public double this[int i, int j]
        {
            get => _data[i, j];
            set => _data[i, j] = value;
        }


    /// <summary>
    /// @brief Creates an empty matrix of given dimensions.
    /// @param rows Number of rows.
    /// @param cols Number of columns.
    /// @throws ArgumentException If dimensions are non-positive.
    /// @echo Initializes all matrix elements to 0.
    /// </summary>
        public Matrix(int rows, int cols)
        {
            if (rows <= 0 || cols <= 0)
                throw new ArgumentException("Matrix dimensions must be positive.");
            Rows = rows;
            Cols = cols;
            _data = new double[rows, cols];
        }


    /// <summary>
    /// @brief Creates a matrix from a 2D array.
    /// @param source Source 2D double array.
    /// @throws ArgumentNullException If source is null.
    /// @echo Copies all elements from the source array into the new matrix.
    /// </summary>
        public Matrix(double[,] source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            Rows = source.GetLength(0);
            Cols = source.GetLength(1);
            _data = new double[Rows, Cols];
            Array.Copy(source, _data, source.Length);
        }


    /// <summary>
    /// @brief Adds two matrices.
    /// @param other Matrix to add.
    /// @return New matrix containing the sum.
    /// @throws ArgumentException If sizes differ.
    /// @echo Performs element-wise addition and returns a new matrix.
    /// </summary>
        public Matrix Add(Matrix other)
        {
            ValidateSameSize(other);
            Matrix result = new(Rows, Cols);

            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Cols; j++)
                    result[i, j] = _data[i, j] + other[i, j];

            return result;
        }

    /// <summary>
    /// @brief Subtracts another matrix from this one.
    /// @param other Matrix to subtract.
    /// @return New matrix containing the result.
    /// @throws ArgumentException If sizes differ.
    /// @echo Performs element-wise subtraction and returns a new matrix.
    /// </summary>
        public Matrix Subtract(Matrix other)
        {
            ValidateSameSize(other);
            Matrix result = new(Rows, Cols);

            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Cols; j++)
                    result[i, j] = _data[i, j] - other[i, j];

            return result;
        }

    /// <summary>
    /// @brief Multiplies this matrix by another.
    /// @param other Right-hand matrix.
    /// @return Product matrix.
    /// @throws ArgumentException If dimensions are incompatible.
    /// @echo Computes the matrix product using the standard row-by-column multiplication.
    /// </summary>
        public Matrix Multiply(Matrix other)
        {
            if (Cols != other.Rows)
                throw new ArgumentException("Incompatible dimensions for multiplication.");

            Matrix result = new(Rows, other.Cols);

            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < other.Cols; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < Cols; k++)
                        sum += _data[i, k] * other[k, j];
                    result[i, j] = sum;
                }

            return result;
        }

    /// <summary>
    /// @brief Returns the transposed matrix.
    /// @return A new transposed matrix.
    /// @echo Swaps rows and columns of the original matrix.
    /// </summary>
        public Matrix Transpose()
        {
            Matrix result = new(Cols, Rows);
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Cols; j++)
                    result[j, i] = _data[i, j];
            return result;
        }

    /// <summary>
    /// @brief Creates an identity matrix.
    /// @param n Size of the identity matrix.
    /// @return n × n identity matrix.
    /// @throws ArgumentException If n ≤ 0.
    /// @echo All diagonal elements are set to 1, all others to 0.
    /// </summary>
        public static Matrix Identity(int n)
        {
            if (n <= 0)
                throw new ArgumentException("Size must be positive.");

            Matrix id = new(n, n);
            for (int i = 0; i < n; i++)
                id[i, i] = 1;
            return id;
        }


    /// <summary>
    /// @brief Validates that matrix sizes match.
    /// @param other Matrix to compare.
    /// @throws ArgumentNullException If other is null.
    /// @throws ArgumentException If sizes differ.
    /// @echo Ensures matrices have identical dimensions before arithmetic operations.
    /// </summary>
        private void ValidateSameSize(Matrix other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));
            if (Rows != other.Rows || Cols != other.Cols)
                throw new ArgumentException("Matrices must be the same size.");
        }

    /// <summary>
    /// @brief Compares matrices for equality with tolerance.
    /// @echo Returns true if all elements are equal within a tolerance of 1e-9.
    /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj is not Matrix other || other.Rows != Rows || other.Cols != Cols)
                return false;

            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Cols; j++)
                    if (Math.Abs(_data[i, j] - other[i, j]) > 1e-9)
                        return false;

            return true;
        }

        public override int GetHashCode() => HashCode.Combine(Rows, Cols);

    /// <summary>
    /// @brief Returns matrix as formatted string.
    /// @return Readable matrix output.
    /// @echo Each row is displayed in brackets, elements aligned and formatted to 2 decimal places.
    /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new();
            for (int i = 0; i < Rows; i++)
            {
                sb.Append("[ ");
                for (int j = 0; j < Cols; j++)
                    sb.Append($"{_data[i, j],6:F2} ");
                sb.AppendLine("]");
            }
            return sb.ToString();
        }
    }
}
