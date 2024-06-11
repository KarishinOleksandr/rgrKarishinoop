using System;
using System.Collections.Generic;
using System.Linq;
using static Karishin.Form1;

namespace Karishin
{
    public class SheWolf
    {
        public int X { get; private set; } // Поточна координата X вовчиці
        public int Y { get; private set; } // Поточна координата Y вовчиці
        public double Score { get; private set; } // Рівень "енергії" вовчиці
        public bool IsDead => Score <= 0; // Вказує, чи вовчиця мертва (енергія <= 0)

        private int stepsWithoutBreeding; // Лічильник вовків без розмноження
        private const int MaxStepsWithoutBreeding = 3; // Максимальна кількість вовчиць без розмноження

        private const int MaxSheWolvesPerCell = 1; // Максимальна кількість вовчиць на одній клітині
        private const int MaxTotalSheWolves = 100; // Максимальна загальна кількість вовчиць

        // Конструктор, що ініціалізує вовчицю з заданими координатами
        public SheWolf(int x, int y)
        {
            X = x;
            Y = y;
            Score = 1.0; // Початковий рівень "енергії"
            stepsWithoutBreeding = 0;
        }

        // Метод для переміщення вовчиці на нову клітину
        public void Move(CellType[,] grid, int gridSize, Random random, List<SheWolf> sheWolves, List<Wolf> wolves, List<Rabbit> rabbits)
        {
            if (IsDead)
            {
                grid[X, Y] = CellType.Empty; // Якщо вовчиця мертва, звільнити поточну клітину
                return;
            }

            bool rabbitFound = false;
            Rabbit targetRabbit = null;

            // Пошук найближчого кролика
            foreach (var rabbit in rabbits)
            {
                if (!rabbit.IsDead)
                {
                    double distance = Math.Sqrt(Math.Pow(rabbit.X - X, 2) + Math.Pow(rabbit.Y - Y, 2));
                    if (distance <= 1)
                    {
                        rabbitFound = true;
                        targetRabbit = rabbit;
                        break;
                    }
                }
            }

            if (rabbitFound)
            {
                MoveTowards(grid, targetRabbit.X, targetRabbit.Y); // Рухатись до кролика
                Score -= 0.1; // Витрачати енергію на рух

                if (X == targetRabbit.X && Y == targetRabbit.Y)
                {
                    targetRabbit.IsDead = true; // Вбити кролика
                    Score += 1; // Отримати енергію
                }
            }
            else
            {
                MoveRandomly(grid, gridSize, random); // Випадковий рух, якщо кролика не знайдено
            }

            stepsWithoutBreeding++;
            if (stepsWithoutBreeding >= MaxStepsWithoutBreeding)
            {
                // Пошук вовка для розмноження
                var wolf = wolves.FirstOrDefault(w => w.X == X && w.Y == Y && !w.IsDead && w.IsMale);
                if (wolf != null)
                {
                    int sheWolvesInCell = sheWolves.Count(sw => sw.X == X && sw.Y == Y && !sw.IsDead);
                    if (sheWolvesInCell < MaxSheWolvesPerCell && sheWolves.Count < MaxTotalSheWolves)
                    {
                        Breed(wolf, grid, gridSize, random, wolves, sheWolves); // Розмноження
                        stepsWithoutBreeding = 0; // Скидання лічильника кроків без розмноження
                    }
                }
            }
        }

        // Метод для випадкового переміщення вовчиці
        private void MoveRandomly(CellType[,] grid, int gridSize, Random random)
        {
            int dx = random.Next(-1, 2); // Випадковий рух по осі X
            int dy = random.Next(-1, 2); // Випадковий рух по осі Y

            int newX = X + dx;
            int newY = Y + dy;

            // Перевірка, чи нові координати в межах сітки та чи клітина порожня
            if (newX >= 0 && newX < gridSize && newY >= 0 && newY < gridSize && grid[newX, newY] == CellType.Empty)
            {
                grid[X, Y] = CellType.Empty; // Звільнити поточну клітину
                X = newX; // Оновити координату X
                Y = newY; // Оновити координату Y
                grid[X, Y] = CellType.SheWolf; // Зайняти нову клітину
            }
        }

        // Метод для переміщення вовчиці до цілі (кролика)
        private void MoveTowards(CellType[,] grid, int targetX, int targetY)
        {
            int dx = Math.Sign(targetX - X); // Визначення напрямку по осі X
            int dy = Math.Sign(targetY - Y); // Визначення напрямку по осі Y

            int newX = X + dx;
            int newY = Y + dy;

            // Перевірка, чи нові координати є порожніми або містять кролика
            if (grid[newX, newY] == CellType.Empty || grid[newX, newY] == CellType.Rabbit)
            {
                grid[X, Y] = CellType.Empty; // Звільнити поточну клітину
                X = newX; // Оновити координату X
                Y = newY; // Оновити координату Y
                grid[X, Y] = CellType.SheWolf; // Зайняти нову клітину
            }
        }

        // Метод для розмноження вовчиці з вовком
        private void Breed(Wolf wolf, CellType[,] grid, int gridSize, Random random, List<Wolf> wolves, List<SheWolf> sheWolves)
        {
            bool isMale = random.Next(0, 2) == 0; // Визначення статі нового вовка випадково
            if (isMale)
            {
                var newWolf = new Wolf(X, Y, true); // Створення нового вовка
                wolves.Add(newWolf);
                grid[X, Y] = CellType.Wolf;
            }
            else
            {
                var newSheWolf = new SheWolf(X, Y); // Створення нової вовчиці
                sheWolves.Add(newSheWolf);
                grid[X, Y] = CellType.SheWolf;
            }
        }
    }
}