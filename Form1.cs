using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Karishin
{
    public partial class Form1 : Form
    {
        private const int GridSize = 20;
        private const int CellSize = 30;
        private const int InitialRabbits = 7;
        private const int InitialWolves = 7;
        private const int InitialSheWolves = 7;
        private const int SimulationSteps = 50;
        private bool isSimulationRunning = true;

        private Random random = new Random();
        private CellType[,] grid = new CellType[GridSize, GridSize];
        private List<Rabbit> rabbits = new List<Rabbit>();
        private List<Wolf> wolves = new List<Wolf>();
        private List<SheWolf> sheWolves = new List<SheWolf>();
        private Panel[,] cells = new Panel[GridSize, GridSize];
        private int stepCount = 0;

        private System.Windows.Forms.Timer timerSimulation;
        private Label labelStep;
        private Image rabbitImage, wolfImage, sheWolfImage;

        public Form1()
        {
            InitializeComponent();
            InitializeGrid();
            InitializeAnimals();
            string rabbitImagePath = Path.Combine(@"D:\���� �����\Karishin\Images\", "rabbitImage.png");
            string wolfImagePath = Path.Combine(@"D:\���� �����\Karishin\Images\", "wolfImage.png");
            string sheWolfImagePath = Path.Combine(@"D:\���� �����\Karishin\Images\", "sheWolfImage.png");

            rabbitImage = LoadAndResizeImage(rabbitImagePath);
            wolfImage = LoadAndResizeImage(wolfImagePath);
            sheWolfImage = LoadAndResizeImage(sheWolfImagePath);

            timerSimulation = new System.Windows.Forms.Timer();
            timerSimulation.Interval = 500;
            timerSimulation.Tick += timerSimulation_Tick;
            timerSimulation.Start();

            labelStep = new Label();
            labelStep.AutoSize = true;
            labelStep.Location = new Point(10, GridSize * CellSize + 20);
            Controls.Add(labelStep);
        }
        // ����������� ���� ��� ���.
        private void InitializeGrid()
        {
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    Panel panel = new Panel
                    {
                        Size = new Size(CellSize, CellSize),
                        Location = new Point(i * CellSize, j * CellSize),
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = Color.White
                    };

                    cells[i, j] = panel;
                    Controls.Add(panel);
                }
            }

            Size = new Size(GridSize * CellSize + 500, GridSize * CellSize + 150);
        }
        // ���������� ���������� ��� ��������� ����������.
        private void InitializeControls()
        {
            Button buttonStartStop = new Button();
            buttonStartStop.Text = "�����/����";
            buttonStartStop.Location = new Point(10, GridSize * CellSize + 40);
            buttonStartStop.Click += btnGO_Click;
            Controls.Add(buttonStartStop);

            Button buttonOneStep = new Button();
            buttonOneStep.Text = "���� ���";
            buttonOneStep.Location = new Point(buttonStartStop.Right + 10, GridSize * CellSize + 40);
            buttonOneStep.Click += btnOne_Click;
            Controls.Add(buttonOneStep);

            Button buttonRestart = new Button();
            buttonRestart.Text = "�������";
            buttonRestart.Location = new Point(10, GridSize * CellSize + 70);
            buttonRestart.Click += btnRestart_Click;
            Controls.Add(buttonRestart);
        }
        // ����������� ������ �� ���.
        private void InitializeAnimals()
        {
            for (int i = 0; i < InitialRabbits; i++)
            {
                int x = random.Next(GridSize);
                int y = random.Next(GridSize);
                if (grid[x, y] == CellType.Empty)
                {
                    grid[x, y] = CellType.Rabbit;
                    rabbits.Add(new Rabbit(x, y));
                }
                else
                {
                    i--;
                }
            }

            for (int i = 0; i < InitialWolves; i++)
            {
                int x = random.Next(GridSize);
                int y = random.Next(GridSize);
                if (grid[x, y] == CellType.Empty)
                {
                    grid[x, y] = CellType.Wolf;
                    wolves.Add(new Wolf(x, y, random.Next(2) == 0));
                }
                else
                {
                    i--;
                }
            }

            for (int i = 0; i < InitialSheWolves; i++)
            {
                int x = random.Next(GridSize);
                int y = random.Next(GridSize);
                if (grid[x, y] == CellType.Empty)
                {
                    grid[x, y] = CellType.SheWolf;
                    sheWolves.Add(new SheWolf(x, y));
                }
                else
                {
                    i--;
                }
            }

            UpdateUI();
        }
        // ��������� ����� ��� �� ������� ��� �������.
        private void timerSimulation_Tick(object sender, EventArgs e)
        {
            for (int x = 0; x < GridSize; x++)
            {
                for (int y = 0; y < GridSize; y++)
                {
                    grid[x, y] = CellType.Empty;
                }
            }

            foreach (Rabbit rabbit in rabbits.ToArray())
            {
                if (!rabbit.IsDead)
                {
                    rabbit.Move(grid, GridSize, random, rabbits);
                    grid[rabbit.X, rabbit.Y] = CellType.Rabbit;
                }
                else
                {
                    rabbits.Remove(rabbit);
                }
            }

            foreach (Wolf wolf in wolves.ToArray())
            {
                if (!wolf.IsDead)
                {
                    wolf.Move(grid, GridSize, random, wolves, sheWolves, rabbits);
                    grid[wolf.X, wolf.Y] = CellType.Wolf;
                }
                else
                {
                    wolves.Remove(wolf);
                }
            }

            foreach (SheWolf sheWolf in sheWolves.ToArray())
            {
                if (!sheWolf.IsDead)
                {
                    sheWolf.Move(grid, GridSize, random, sheWolves, wolves, rabbits);
                    grid[sheWolf.X, sheWolf.Y] = CellType.SheWolf;
                }
                else
                {
                    sheWolves.Remove(sheWolf);
                }
            }

            UpdateGrid();

            stepCount++;

            if (stepCount >= SimulationSteps)
            {
                timerSimulation.Stop();
            }

            UpdateUI();
        }
        // ��������� ����������� ���� ���.
        private void UpdateGrid()
        {
            for (int x = 0; x < GridSize; x++)
            {
                for (int y = 0; y < GridSize; y++)
                {
                    if (grid[x, y] == CellType.Rabbit)
                    {
                        cells[x, y].BackgroundImage = rabbitImage;
                    }
                    else if (grid[x, y] == CellType.Wolf)
                    {
                        cells[x, y].BackgroundImage = wolfImage;
                    }
                    else if (grid[x, y] == CellType.SheWolf)
                    {
                        cells[x, y].BackgroundImage = sheWolfImage;
                    }
                    else
                    {
                        cells[x, y].BackgroundImage = null;
                        cells[x, y].BackColor = Color.White;
                    }
                }
            }

            labelStep.Text = $"����: {stepCount}";
        }
        // ������������ �� ���� ������ ����������.
        private Image LoadAndResizeImage(string imagePath)
        {
            if (File.Exists(imagePath))
            {
                return ResizeImage(Image.FromFile(imagePath), CellSize, CellSize);
            }
            else
            {
                MessageBox.Show("Image file not found: " + imagePath);
                return null;
            }
        }
        // ���� ������ ���������� �� ������ ������ �� ������.
        private Image ResizeImage(Image imgToResize, int width, int height)
        {
            Bitmap b = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(b))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(imgToResize, 0, 0, width, height);
            }
            return b;
        }
        //������ ������� ��� ������� ���������.
        private void btnGO_Click(object sender, EventArgs e)
        {
            if (isSimulationRunning)
            {
                timerSimulation.Stop();
            }
            else
            {
                timerSimulation.Start();
            }
            isSimulationRunning = !isSimulationRunning;
        }
        // ������ ������ ���� ���� ���������.
        private void btnOne_Click(object sender, EventArgs e)
        {
            PerformSimulationStep();
        }
        private void PerformSimulationStep()
        {
            if (timerSimulation.Enabled)
            {
                timerSimulation.Stop();
            }

            timerSimulation_Tick(null, EventArgs.Empty);
        }
        //������ ����������� ���.
        private void btnRestart_Click(object sender, EventArgs e)
        {
            RestartGame();
        }
        // ����������� ��� �� �������� ������ ������.
        private void RestartGame()
        {
            if (timerSimulation.Enabled)
            {
                timerSimulation.Stop();
            }

            ResetGame();
        }
        // ����� ��� �� ����������� �����.
        private void ResetGame()
        {
            ClearGrid();
            ClearAnimalLists();
            InitializeAnimals();
            stepCount = 0;
            UpdateGrid();
            if (!timerSimulation.Enabled)
            {
                timerSimulation.Start();
            }

            UpdateUI();
        }
        // �������� ���� �� ������.
        private void ClearGrid()
        {
            for (int x = 0; x < GridSize; x++)
            {
                for (int y = 0; y < GridSize; y++)
                {
                    grid[x, y] = CellType.Empty;
                }
            }
        }
        // ��������� ��������������� ���������� (UI) ��� ����������� ������� ������.
        private void UpdateUI()
        {
            label4.Text = $"�������: {rabbits.Count}";
            label2.Text = $"�����: {wolves.Count}";
            label3.Text = $"�������: {sheWolves.Count}";
        }
        // �������� ������ ������.
        private void ClearAnimalLists()
        {
            int rabbitCount = rabbits.Count;
            int wolfCount = wolves.Count;
            int sheWolfCount = sheWolves.Count;
            rabbits.Clear();
            wolves.Clear();
            sheWolves.Clear();
            label4.Text = $"�������: {rabbitCount}";
            label2.Text = $"�����: {wolfCount}";
            label3.Text = $"�������: {sheWolfCount}";
        }

        public enum CellType
        {
            Empty,
            Rabbit,
            Wolf,
            SheWolf
        }
    }
}
