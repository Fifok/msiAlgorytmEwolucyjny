using System;
using System.Collections;
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
            var population = generatePopulation();
            mutatePopulation(population);
            population = selectNewPopulation(population);
            Console.WriteLine("".PadLeft(100,'='));
            PrintPopulation(population);
            Console.WriteLine("".PadLeft(100,'='));
            population = crossPopulation(population);
            PrintPopulation(population);
        }

        static Osobnik[] generatePopulation()
        {
            Osobnik[] population = new Osobnik[PopSize];

            for (int i = 0; i < PopSize; i++)
            {
                double x = -XD + 2 * XD * rnd.NextDouble();
                double y = -YD + 2 * YD * rnd.NextDouble();

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

        static Osobnik[] crossPopulation(Osobnik[] population)
        {
            Osobnik[] newPopulation = new Osobnik[population.Length];

            for (int i = 0; i < newPopulation.Length; i++)
            {
                int firstIndex = rnd.Next(population.Length);
                int secondIndex;
                do
                {
                     secondIndex = rnd.Next(population.Length);
                } while (firstIndex == secondIndex);
                Console.WriteLine($"First: {firstIndex} --- Second: {secondIndex}");
                newPopulation[i] = generateChild(population[firstIndex], population[secondIndex]);
            }

            return newPopulation;
        }

        static double f(double x, double y)
        {
            return -Pow(x, 2) - Pow(y, 2);
        }

        static Osobnik generateChild(Osobnik first, Osobnik second)
        {
            double x, y;

            if (Abs(first.X) - Abs(second.X) >= 0)
            {
                x = (first.X - second.X) / 2;
            }
            else
            {
                x = (second.X - first.X) / 2;
            }

            if (Abs(first.Y) - Abs(second.Y) >= 0)
            {
                y = (first.Y - second.Y) / 2;
            }
            else
            {
                y = (second.Y - first.Y) / 2;
            }

            return new Osobnik {X = x, Y = y, Dopasowanie = f(x, y)};
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