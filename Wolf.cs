using System;
using System.Collections.Generic;
using System.Linq;
using static Karishin.Form1;

namespace Karishin
{
    public class Wolf
    {
        public int X { get; private set; } // Поточна координата X вовка
        public int Y { get; private set; } // Поточна координата Y вовка
        public double Score { get; private set; } // Рівень "енергії" вовка
        public bool IsMale { get; private set; } // Вказує, чи вовк чоловічої статі
        public bool IsDead => Score <= 0; // Вказує, чи вовк мертвий (енергія <= 0)

        private const int MaxStepsWithoutBreeding = 2; // Максимальна кількість вовків без розмноження
        private int stepsWithoutBreeding; // Лічильник вовків без розмноження

        private const int MaxWolvesPerCell = 2; // Максимальна кількість вовків на одній клітині
        private const int MaxTotalWolves = 100; // Максимальна загальна кількість вовків

        // Конструктор, що ініціалізує вовка з заданими координатами та статтю
        public Wolf(int x, int y, bool isMale)
        {
            X = x;
            Y = y;
            IsMale = isMale;
            Score = 1.0; // Початковий рівень "енергії"
            stepsWithoutBreeding = 0;
        }

        // Метод для переміщення вовка на нову клітину
        public void Move(CellType[,] grid, int gridSize, Random random, List<Wolf> wolves, List<SheWolf> sheWolves, List<Rabbit> rabbits)
        {
            if (IsDead)
            {
                grid[X, Y] = CellType.Empty; // Якщо вовк мертвий, звільнити поточну клітину
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
                // Пошук вовчиці для розмноження
                var sheWolf = sheWolves.FirstOrDefault(sw => sw.X == X && sw.Y == Y && !sw.IsDead);
                if (sheWolf != null)
                {
                    int wolvesInCell = wolves.Count(w => w.X == X && w.Y == Y && !w.IsDead);
                    if (wolvesInCell < MaxWolvesPerCell && wolves.Count < MaxTotalWolves)
                    {
                        bool isMale = random.Next(0, 2) == 0;
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
                        stepsWithoutBreeding = 0; // Скидання лічильника кроків без розмноження
                    }
                }
            }
        }

        // Метод для випадкового переміщення вовка
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
                grid[X, Y] = CellType.Wolf; // Зайняти нову клітину
            }
        }

        // Метод для переміщення вовка до цілі (кролика)
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
                grid[X, Y] = CellType.Wolf; // Зайняти нову клітину
            }
        }
    }
}
