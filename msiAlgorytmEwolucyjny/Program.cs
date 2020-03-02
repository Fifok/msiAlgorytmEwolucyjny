using System;
using System.Linq;
using static System.Math;

namespace msiAlgorytmEwolucyjny
{
    class Program
    {
        private static Random rnd { get; set; } = new Random();
        private static int PopSize { get; set; } = 100;
        private static double PrawdoMuta { get; set; } = 0.15;

        private static int XD { get; set; } = 2;
        private static int YD { get; set; } = 2;

        static void Main(string[] args)
        {
            var population = generatePopulation(XD, YD, PopSize);

            for (int i = 0; i < PopSize; i++)
            {
                Console.WriteLine($"{i}: {population[i]}");
            }

            mutatePopulation(population);
        }

        static Osobnik[] generatePopulation(int zakresX, int zakresY, int size)
        {
            Osobnik[] population = new Osobnik[size];

            for (int i = 0; i < size; i++)
            {
                double x = -zakresX + 2 * zakresX * rnd.NextDouble();
                double y = -zakresY + 2 * zakresY * rnd.NextDouble();

                population[i] = new Osobnik {X = x, Y = y, Dopasowanie = f(x, y)};
            }

            return population;
        }

        static void mutatePopulation(Osobnik[] population)
        {
            for (int i = 0; i < population.Length; i++)
            {
                if (rnd.NextDouble() < PrawdoMuta)
                {
                    Console.Write($"Mutate {i}");
                    if (rnd.NextDouble() < 0.5)
                        population[i].X += rnd.NextDouble();
                    else
                        population[i].X -= rnd.NextDouble();

                    if (rnd.NextDouble() < 0.5)
                        population[i].Y += rnd.NextDouble();
                    else
                        population[i].Y -= rnd.NextDouble();

                    if (population[i].X < -XD || population[i].X > XD || population[i].Y > YD || population[i].Y < -YD)
                    {
                        Console.Write($"\tPunished   {population[i]}");
                        
                        population[i].Dopasowanie -= 1000;
                    }

                    Console.WriteLine();
                }
            }
        }

        static double f(double x, double y)
        {
            return -Pow(x, 2) - Pow(y, 2);
        }
    }
}