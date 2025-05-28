using System;
using System.Collections.Generic;

namespace Quest3
{

    class SquareMatrix
    {
        //public int ColsAndRows { get; set; }
        public int[,] matrix;

        public SquareMatrix(int Cols)
        {
            try
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
            catch (OutOfMemoryException ex)
            {
                Console.WriteLine(ex.Message);
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
            if (matrix1.matrix.GetLength(0) != matrix2.matrix.GetLength(0))
            {
                MatrixResult.matrix = null;
                return MatrixResult;
            }
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

        //Ну давайте среднее арифметическое элементов сравним
        public static bool operator >(SquareMatrix matrix1, SquareMatrix matrix2)
        {
            int count1 = 0, count2 = 0, sum1 = 0, sum2 = 0;

            for (int i = 0; i < matrix1.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.matrix.GetLength(0); j++)
                {
                    sum1 += matrix1.matrix[i, j];
                    count1++;
                }
            }
            for (int i = 0; i < matrix2.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix2.matrix.GetLength(0); j++)
                {
                    sum2 += matrix2.matrix[i, j];
                    count2++;
                }
            }
            sum1 /= count1;
            sum2 /= count2;


            if (sum1 > sum2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator <(SquareMatrix matrix1, SquareMatrix matrix2)
        {
            int count1 = 0, count2 = 0, sum1 = 0, sum2 = 0;

            for (int i = 0; i < matrix1.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.matrix.GetLength(0); j++)
                {
                    sum1 += matrix1.matrix[i, j];
                    count1++;
                }
            }
            for (int i = 0; i < matrix2.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix2.matrix.GetLength(0); j++)
                {
                    sum2 += matrix2.matrix[i, j];
                    count2++;
                }
            }
            sum1 /= count1;
            sum2 /= count2;

            if (sum1 < sum2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

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
        
        public double[,] ConvertToDoubleArray(SquareMatrix matrix)
        {
            double[,] doublematrix = new double[matrix.matrix.GetLength(0), matrix.matrix.GetLength(0)];

            for (int i = 0; i < matrix.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.matrix.GetLength(0); j++)
                {
                    doublematrix[i, j] = Convert.ToDouble(matrix.matrix[i, j]);
                }
            }

            return doublematrix;
        }
        public double GetDeterminant(SquareMatrix matrix)
        {
            return Determinant.GetDeterminant(ConvertToDoubleArray(matrix));
        }

        public string ToString(SquareMatrix matrix)
        {
            string output = "";
            for (int i = 0; i < matrix.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.matrix.GetLength(0); j++)
                {
                    output += matrix.matrix[i, j] + " ";
                }
                output += "\n";
            }
            return output;
        }

        public override bool Equals(object obj)
        {
            return obj is SquareMatrix matrix &&
                   EqualityComparer<int[,]>.Default.Equals(this.matrix, matrix.matrix);
        }

        public override int GetHashCode()
        {
            return -79939124 + EqualityComparer<int[,]>.Default.GetHashCode(matrix);
        }

        //Я правда пытался
        //public double[,] GerMinor()
        //{
        //    return null;
        //}
        //public double[,] InvertMatrix(SquareMatrix matrix)
        //{
        //    return null;
        //}



    }

    class Program
    {
        static int Main(string[] args)
        {
            Console.WriteLine("\tНУ ТИПА МАТРИЧНЫЙ КАЛЬКУЛЯТОР");
            int ColsAndRows=0;
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
                Console.WriteLine("АХТУНГ АШИБКА\n Число должно быть больше 0");
                return 0;
            }

            SquareMatrix Matrix1 = new SquareMatrix(ColsAndRows);
            if (Matrix1.matrix == null)
            {
                Console.WriteLine("А не слишком ли большая матрица?");
                return 0;
            }
            Matrix1.getmatrix();

            Console.Write("Введите количество строк и столбцов второй квадратной матрицы: ");
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

            SquareMatrix Matrix2 = new SquareMatrix(ColsAndRows);
            if (Matrix2.matrix == null)
            {
                Console.WriteLine("А не слишком ли большая матрица?");
                return 0;
            }
            Matrix2.getmatrix();
            do
            {
                Console.WriteLine("Введите желаемое действие:\n" +
                    "1.Сложение матриц\n" +
                    "2.Вычитание матриц\n" +
                    "3.Умножение матриц\n" +
                    "4.Равны ли матрицы\n" +
                    "5.Какая матрица больше (среднее арифметическое элементов)\n" +
                    "6.Найти детерминанты матриц\n" +
                    "0.Выход");

                string rawchoise = Console.ReadLine();
                int choise;
                try
                {
                    choise = Convert.ToInt32(rawchoise);
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                    choise = -1;
                }
                   
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
                        if (MatrixMultiplication.matrix != null)
                            MatrixMultiplication.getmatrix();
                        else
                            Console.WriteLine("Для вычисления произведения матриц длина строк и столбцов должны быть равны " +
                                "(по крайней мере в случае с квадратными матрицами)");
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

                    case 5:
                        if (Matrix1 == Matrix2)
                            Console.WriteLine("Матрицы равны");
                        else if (Matrix1 > Matrix2)
                            Console.WriteLine("Первая больше");
                        else if (Matrix1 < Matrix2)
                            Console.WriteLine("Вторая больше");
                        else
                            Console.WriteLine("БИПБУП ОШИБКА"); //Не, ну а вдруг
                        break;
                    case 6:
                        Console.WriteLine($"Определитель первой матрицы = {Matrix1.GetDeterminant(Matrix1)}\n" +
                            $"Определитель второй матрицы = {Matrix2.GetDeterminant(Matrix2)}");
                        break;
                    case 7:
                        Console.WriteLine($"Первая матрица в строке:\n{Matrix1.ToString(Matrix1)}\n" +
                            $"Вторая матрица в строке:\n{Matrix2.ToString(Matrix2)}");
                        break;
                    case 0:
                        return 0;

                    default:
                        Console.WriteLine("БИПБУП ОШИБКА!\nНет такого варианта");
                        break;
                }
                Console.WriteLine("\nДля продолжения нажмите (почти) любую клавишу...");
                Console.ReadKey(true);
                Console.WriteLine();
            } while (true);

        }
    }
}
