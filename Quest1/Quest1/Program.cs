using System;

namespace Quest1
{
	class Program
	{
		static void Main(string[] args)
		{
			int choise = 1;
			do
			{
				Console.WriteLine("МЕНЮШКА:)\n\t1. Первое задание\n\t2. Второе задание\n\t0.Выход из программы");
				choise = Convert.ToInt32(Console.ReadLine());

				if (choise == 1)
				{
					int a = 1, n = 1;

					Console.Write("Введите a = ");
					a = Convert.ToInt32(Console.ReadLine());
					Console.Write("Введите n = ");
					n = Convert.ToInt32(Console.ReadLine());

					int result = a;

					for (int i = 1; i < n; ++i)
					{
						result *= a;
						Console.WriteLine($"result = {result}");
					}
					Console.WriteLine($"{a} в степени {n} = {result}");
				}
				else if (choise == 2)
				{
					string x;
					Console.WriteLine("Введите строку");
					x = Console.ReadLine();
					x += x[1];
					x = x.Remove(1, 1);
					Console.WriteLine(x);
				}
				else if (choise<0 || choise>2)
				{
					Console.WriteLine("Нет такого варианта");
				}
			} while (choise != 0);
			
		}
	}
}
