using System;
using System.Text;

namespace MatrixLib
{
    public class Matrix
    {
        private readonly double[,] _data;

        public int Rows { get; }
        public int Cols { get; }

        public double this[int i, int j]
        {
            get => _data[i, j];
            set => _data[i, j] = value;
        }

        public Matrix(int rows, int cols)
        {
            if (rows <= 0 || cols <= 0)
                throw new ArgumentException("Matrix dimensions must be positive.");
            Rows = rows;
            Cols = cols;
            _data = new double[rows, cols];
        }


        public Matrix(double[,] source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            Rows = source.GetLength(0);
            Cols = source.GetLength(1);
            _data = new double[Rows, Cols];
            Array.Copy(source, _data, source.Length);
        }


        public Matrix Add(Matrix other)
        {
            ValidateSameSize(other);
            Matrix result = new(Rows, Cols);

            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Cols; j++)
                    result[i, j] = _data[i, j] + other[i, j];

            return result;
        }

        public Matrix Subtract(Matrix other)
        {
            ValidateSameSize(other);
            Matrix result = new(Rows, Cols);

            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Cols; j++)
                    result[i, j] = _data[i, j] - other[i, j];

            return result;
        }

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

        public Matrix Transpose()
        {
            Matrix result = new(Cols, Rows);
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Cols; j++)
                    result[j, i] = _data[i, j];
            return result;
        }

        public static Matrix Identity(int n)
        {
            if (n <= 0)
                throw new ArgumentException("Size must be positive.");

            Matrix id = new(n, n);
            for (int i = 0; i < n; i++)
                id[i, i] = 1;
            return id;
        }


        private void ValidateSameSize(Matrix other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));
            if (Rows != other.Rows || Cols != other.Cols)
                throw new ArgumentException("Matrices must be the same size.");
        }

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