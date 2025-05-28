using System;
using System.Diagnostics;
using System.Text;


namespace Quest3
{
    //Мне искренне лень вспоминать как руками считается определитель
    //Поэтому я просто позаимствовал этот код у добрых людей с форумов (не пойман - не вор)
    //Да и зачем заново изобретать велосипед
    //Извините
    static class Determinant
    {
        /// <summary>
        /// Метод выполняющий перестановку в массиве целых чисел
        /// </summary>
        /// <param name="numList"></param>
        /// <remarks>Массив изначально должен быть отсортирован по возрастанию.</remarks>
        /// <returns></returns>
        static public bool NextPermutation(int[] numList)
        {
            /*
             Knuths
             1. Ищем максимальный индекс j такой, что a[j] < a[j + 1]. Если такого индекса нет, то перестановка последняя.
             2. Найти наибольший индекс l такой, что a[j] < a[l]. Т.к. j + 1 при этом существует, 
             * то l всегда удовлетворяет условию j < l
             3. Меняем местами a[j] и a[l].
             4. Разворачиваем последовательность, начиная a[j + 1] пока не войдёт последний элементa[n].
             */

            //1.
            int largestIndex = -1;
            for (int i = numList.Length - 2; i >= 0; i--)
            {
                if (numList[i] < numList[i + 1])
                {
                    largestIndex = i;
                    break;
                }
            }

            if (largestIndex < 0) return false;
            //2.
            int largestIndex2 = -1;
            for (var i = numList.Length - 1; i >= 0; i--)
            {
                if (numList[largestIndex] < numList[i])
                {
                    largestIndex2 = i;
                    break;
                }
            }
            //3.
            int tmp = numList[largestIndex];
            numList[largestIndex] = numList[largestIndex2];
            numList[largestIndex2] = tmp;
            //4.
            for (int i = largestIndex + 1, j = numList.Length - 1; i < j; i++, j--)
            {
                tmp = numList[i];
                numList[i] = numList[j];
                numList[j] = tmp;
            }

            return true;
        }

        /// <summary>
        /// Метод вычисления определителя матрицы по основному определению.
        /// </summary>
        static public double GetDeterminant(double[,] m)
        {
            if (m.GetLength(0) != m.GetLength(1))
                throw new ArgumentException("Для поиска определителя матрица должна быть квадратной.");

            double result = 0, prod = 1;
            /// Массив индексов. Выполняя в нём перестановки
            /// будем получать все возможные комбинации для произведения элементов матрицы
            int[] indices = IndicesMatrix(m.GetLength(0));

            int sign = 0;
            /// Для вывода результирующей строки
            StringBuilder sb = new StringBuilder();

            do
            {
                for (int i = 0; i < m.GetLength(0); i++)
                {
                    sb.AppendFormat("{0}m{1}{2}", (i == 0 ? string.Empty : "*"), i, indices[i]);
                    prod *= m[i, indices[i]];
                }

                sign = (int)Math.Pow(-1, Inversions(indices));
                result += sign * prod;

                sb.AppendFormat(" {0} ", sign > 0 ? "+" : "-");

                prod = 1;
            } while (NextPermutation(indices));

            sb.Remove(sb.Length - 3, 3);
            Debug.WriteLine("result = {0}", sb);

            return result;
        }

        /// <summary>
        /// Метод для получения исходного массива индексов
        /// </summary>
        /// <param name="n">Размерность массива индексов</param>
        static private int[] IndicesMatrix(int n)
        {
            int[] result = new int[n];
            for (int i = 0; i < n; i++) result[i] = i;
            return result;
        }

        /// <summary>
        /// Метод для определения чётности или нечётности перестановки
        /// </summary>
        /// <remarks>Перестановка называется чётной, если число инверсий в ней чётно,
        /// и нечётной — в противном случае. Инверсию образуют два числа в перестановке,
        /// когда меньшее из них расположено правее большего</remarks>
        static private int Inversions(int[] m)
        {
            int result = 0;
            for (int i = 0; i < m.Length; i++)
                for (int j = i + 1; j < m.Length; j++)
                    if (m[i] > m[j]) result++;
            return result;
        }
    }
}

