using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Karishin.Form1;

namespace Karishin
{
    public class Rabbit
    {
        public int X { get; private set; } // Поточна координата X кролика
        public int Y { get; private set; } // Поточна координата Y кролика
        public bool IsDead { get; set; } // Вказує, чи кролик мертвий

        private int stepsWithoutBreeding; // Лічильник кроків без розмноження
        private const int MaxStepsWithoutBreeding = 5; // Максимальна кількість кроків без розмноження
        private const int MaxRabbitsPerCell = 2; // Максимальна кількість кроликів на одній клітині
        private const int MaxTotalRabbits = 200; // Максимальна загальна кількість кроликів

        // Конструктор, що ініціалізує кролика з заданими координатами
        public Rabbit(int x, int y)
        {
            X = x;
            Y = y;
            IsDead = false;
            stepsWithoutBreeding = 0;
        }

        // Метод для переміщення кролика на нову клітину
        public void Move(CellType[,] grid, int gridSize, Random random, List<Rabbit> rabbits)
        {
            List<(int, int)> emptyCells = GetEmptyAdjacentCells(grid, gridSize); // Отримати список порожніх сусідніх клітин
            if (emptyCells.Count > 0)
            {
                var newCell = emptyCells[random.Next(emptyCells.Count)]; // Вибрати випадкову порожню клітину
                grid[X, Y] = CellType.Empty; // Звільнити поточну клітину
                X = newCell.Item1; // Оновити координату X
                Y = newCell.Item2; // Оновити координату Y
                grid[X, Y] = CellType.Rabbit; // Зайняти нову клітину

                stepsWithoutBreeding++; // Збільшити лічильник кроків без розмноження
                if (stepsWithoutBreeding >= MaxStepsWithoutBreeding && rabbits.Count < MaxTotalRabbits)
                {
                    var breedCell = emptyCells[random.Next(emptyCells.Count)]; // Вибрати випадкову порожню клітину для розмноження
                    if (grid[breedCell.Item1, breedCell.Item2] == CellType.Empty)
                    {
                        int rabbitsInCell = rabbits.Count(r => r.X == breedCell.Item1 && r.Y == breedCell.Item2);
                        if (rabbitsInCell < MaxRabbitsPerCell)
                        {
                            rabbits.Add(new Rabbit(breedCell.Item1, breedCell.Item2)); // Додати нового кролика
                            grid[breedCell.Item1, breedCell.Item2] = CellType.Rabbit; // Зайняти клітину новим кроликом
                            stepsWithoutBreeding = 0; // Скинути лічильник кроків без розмноження
                        }
                    }
                }
            }
        }

        // Метод для отримання списку порожніх сусідніх клітин
        private List<(int, int)> GetEmptyAdjacentCells(CellType[,] grid, int gridSize)
        {
            List<(int, int)> emptyCells = new List<(int, int)>();

            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0) continue; // Пропустити поточну клітину

                    int newX = X + dx;
                    int newY = Y + dy;

                    // Перевірити, чи нові координати знаходяться в межах сітки і чи клітина порожня
                    if (newX >= 0 && newX < gridSize && newY >= 0 && newY < gridSize && grid[newX, newY] == CellType.Empty)
                    {
                        emptyCells.Add((newX, newY)); // Додати порожню клітину до списку
                    }
                }
            }

            return emptyCells;
        }
    }
}