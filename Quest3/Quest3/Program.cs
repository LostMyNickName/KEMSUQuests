using System;

namespace Quest3
{

    class SquareMatrix
    {

        public int ColsAndRows { get; set; }
        public int[,] matrix;

        public SquareMatrix(int Cols)
        {
            matrix = new int[Cols, Cols];
            Random rand = new Random();

            for (int i = 0; i < Cols; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    matrix[i, j] = rand.Next(0, 100);
                }
            }
        }

        public void getmatrix()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    Console.Write($"{matrix[i, j]}\t");
                }
                Console.Write("\n");
            }
        }

        public static SquareMatrix operator +(SquareMatrix matrix1, SquareMatrix matrix2)
        {
            SquareMatrix MatrixResult = new SquareMatrix(matrix1.matrix.GetLength(0));

            if (matrix1.matrix.GetLength(0) != matrix2.matrix.GetLength(0))
            {
                MatrixResult.matrix = null;
                return MatrixResult;
            }
            else
            {

                for (int i = 0; i < matrix1.matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix1.matrix.GetLength(0); j++)
                    {
                        MatrixResult.matrix[i, j] = matrix1.matrix[i, j] + matrix2.matrix[i, j];
                    }
                }

                return MatrixResult;
            }
        }

        public static SquareMatrix operator -(SquareMatrix matrix1, SquareMatrix matrix2)
        {
            SquareMatrix MatrixResult = new SquareMatrix(matrix1.matrix.GetLength(0));
            if (matrix1.matrix.GetLength(0) != matrix2.matrix.GetLength(0))
            {
                MatrixResult.matrix = null;
                return MatrixResult;
            }
            else
            {


                for (int i = 0; i < matrix1.matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix1.matrix.GetLength(0); j++)
                    {
                        MatrixResult.matrix[i, j] = matrix1.matrix[i, j] - matrix2.matrix[i, j];
                    }
                }

                return MatrixResult;
            }
        }

        public static SquareMatrix operator *(SquareMatrix matrix1, SquareMatrix matrix2)
        {
            SquareMatrix MatrixResult = new SquareMatrix(matrix1.matrix.GetLength(0));
            int[,] result = new int[matrix1.matrix.GetLength(0), matrix1.matrix.GetLength(0)];

            for (int i = 0; i < matrix1.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix2.matrix.GetLength(1); j++)
                {
                    for (int k = 0; k < matrix2.matrix.GetLength(0); k++)
                    {
                        result[i, j] += matrix1.matrix[i, k] * matrix2.matrix[k, j];
                    }
                }
            }

            MatrixResult.matrix = result;
            return MatrixResult;
        }

        public static bool operator ==(SquareMatrix matrix1, SquareMatrix matrix2)
        {
            for (int i = 0; i < matrix1.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.matrix.GetLength(0); j++)
                {
                    if (matrix1.matrix[i, j] != matrix2.matrix[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static bool operator !=(SquareMatrix matrix1, SquareMatrix matrix2)
        {
            for (int i = 0; i < matrix1.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.matrix.GetLength(0); j++)
                {
                    if (matrix1.matrix[i, j] != matrix2.matrix[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //public static bool operator <(SquareMatrix matrix1, SquareMatrix matrix2)
        //{
        //    //Из того, что я нагуглил, нет общепринятого понятия сравнивания матриц
        //    return ЕГГОГ; 
        //}
        //public static bool operator >(SquareMatrix matrix1, SquareMatrix matrix2)
        //{
        //    //Из того, что я нагуглил, нет общепринятого понятия сравнивания матриц
        //    return ЕГГОГ; 
        //}

        public static bool operator <=(SquareMatrix matrix1, SquareMatrix matrix2)
        {
            for (int i = 0; i < matrix1.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.matrix.GetLength(0); j++)
                {
                    if (matrix1.matrix[i, j] != matrix2.matrix[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static bool operator >=(SquareMatrix matrix1, SquareMatrix matrix2)
        {
            for (int i = 0; i < matrix1.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.matrix.GetLength(0); j++)
                {
                    if (matrix1.matrix[i, j] != matrix2.matrix[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }

    class Program
    {
        static int Main(string[] args)
        {
            int ColsAndRows = 0;
            Console.Write("Введите количество строк и столбцов первой квадратной матрицы: ");
            try
            {
                ColsAndRows = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Ну это ведь вообще не число");
                return 0;
            }
            if (ColsAndRows <= 0)
            {
                Console.WriteLine("АХТУНГ АШИБКА");
                return 0;
            }

            SquareMatrix Matrix1 = new SquareMatrix(ColsAndRows);
            Matrix1.getmatrix();

            Console.Write("Введите количество строк и столбцов второй квадратной матрицы: ");
            ColsAndRows = Convert.ToInt32(Console.ReadLine());
            if (ColsAndRows <= 0)
            {
                Console.WriteLine("АХТУНГ АШИБКА");
                return 0;
            }

            SquareMatrix Matrix2 = new SquareMatrix(ColsAndRows);
            Matrix2.getmatrix();

            Console.WriteLine("Введите желаемое действие:\n" +
                "1.Сложение матриц\n" +
                "2.Вычитание матриц\n" +
                "3.Умножение матриц\n" +
                "4.Равны ли матрицы");

            int choise = Convert.ToInt32(Console.ReadLine());

            switch (choise)
            {
                case 1:
                    Console.WriteLine("Сумма матриц: ");
                    SquareMatrix MatrixSum = Matrix1 + Matrix2;
                    if (MatrixSum.matrix != null)
                        MatrixSum.getmatrix();
                    else
                        Console.WriteLine("Для вычисления суммы матриц длина строк и столбцов должны быть равны");
                    break;

                case 2:
                    Console.WriteLine("Разница матриц: ");
                    SquareMatrix MatrixMinus = Matrix1 - Matrix2;
                    if (MatrixMinus.matrix != null)
                        MatrixMinus.getmatrix();
                    else
                        Console.WriteLine("Для вычисления разницы матриц длина строк и столбцов должны быть равны");
                    break;

                case 3:
                    Console.WriteLine("Умножение матриц: ");
                    SquareMatrix MatrixMultiplication = Matrix1 * Matrix2;
                    MatrixMultiplication.getmatrix();
                    break;

                case 4:
                    if (Matrix1 == Matrix2)
                    {
                        Console.WriteLine("Матрицы равны");
                    }
                    if (Matrix1 != Matrix2)
                    {
                        Console.WriteLine("Матрицы НЕ равны");
                    }
                    break;

                default:
                    Console.WriteLine("БИПБУП ОШИБКА!\nНет такого варианта");
                    break;
            }

            return 0;
        }
    }
}
