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
            mutatePopulation(population);
            var newPopulation = selectNewPopulation(population);

            var result = newPopulation
                .GroupBy(x => x.X)
                .OrderBy(x => x.Count())
                .Select(x=>new{x.Key,Amount = x.Count()});

            foreach (var i in result)
            {
                Console.WriteLine($"{i.Key}: {i.Amount}");
            }
            
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

        static Osobnik[] selectNewPopulation(Osobnik[] population)
        {
            Osobnik[] newPopulation = new Osobnik[population.Length];

            for (int i = 0; i < population.Length; i++)
            {
                int? enemyIndex = null;
                do
                {
                    enemyIndex = rnd.Next(population.Length);
                } while (enemyIndex == i);

                if (population[i].Dopasowanie <= population[enemyIndex.Value].Dopasowanie)
                    newPopulation[i] = population[enemyIndex.Value];
                else
                    newPopulation[i] = population[i];
            }

            return newPopulation;
        }

        static double f(double x, double y)
        {
            return -Pow(x, 2) - Pow(y, 2);
        }

        static void PrintPopulation(Osobnik[] population)
        {
            for (int i = 0; i < population.Length; i++)
            {
                Console.WriteLine($"{i}: {population[i]}");
            }
        }
    }
}