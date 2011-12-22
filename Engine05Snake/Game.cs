using System;
using System.IO;

namespace ConsoleEngine
{
    /// <summary>
    /// основной класс игры
    /// </summary>
    class Game
    {
        ConsoleKeyInfo keys;

        PieceChar[,] BaseBoard;
        PieceChar[,] PreStateBoard;

        PieceChar[,] Ground;
        PieceChar[,] Wall;
        PieceChar[] Snake;

        int direcX = 0;
        int direcY = 0;

        PieceChar[] player;

        #region Базовые методы

        /// <summary>
        /// конструктор класса
        /// </summary>
        public Game()
        {
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.DarkGreen;
        }

        /// <summary>
        /// метод инициализации игры
        /// </summary>
        public void Run()
        {
            BaseBoard = new PieceChar[Console.WindowHeight, Console.WindowWidth];
            PreStateBoard = new PieceChar[Console.WindowHeight, Console.WindowWidth];
            Ground = new PieceChar[Console.WindowHeight, Console.WindowWidth];
            Wall = new PieceChar[Console.WindowHeight, Console.WindowWidth];
            Snake = new PieceChar[10];

            //тестовый игрок
            player = new PieceChar[1];
            player[0] = new PieceChar(10, 10, ConsoleColor.Yellow, '█');

            for (int i = 0; i < Console.WindowHeight; i++)
            {
                for (int j = 0; j < Console.WindowWidth; j++)
                {
                    BaseBoard[i,j] = new PieceChar(i,j, ConsoleColor.White, ' ');
                    PreStateBoard[i,j] = new PieceChar(i,j, ConsoleColor.White, ' ');
                    Ground[i,j] = new PieceChar(i,j, ConsoleColor.White, ' ');
                    Wall[i,j] = new PieceChar(i,j, ConsoleColor.White, ' ');
                }
            }

            ReadFiles(Wall, "Wall.txt");

            Console.Clear();
        }

        /// <summary>
        /// проверка, обновление всех состояний
        /// </summary>
        public void Update()
        {
            if (Console.KeyAvailable)
            {
                keys = Console.ReadKey();
                InputKey();
            }

            if (keys.Key == ConsoleKey.Escape) Program.Quit();

            /* апдейт игрока */
            player[0].X += direcX;
            player[0].Y += direcY;

            if (player[0].Y >= Console.WindowHeight)
                player[0].Y = Console.WindowHeight - 1;
            if (player[0].Y <= 1)
                player[0].Y = 1;

            ClearArray(ref BaseBoard);

            /* копирование всех массивов в базовый по очереди */
            CopyArray2Base(Wall);
            CopyArray2Base(player);
        }

        /// <summary>
        /// отрисовка
        /// </summary>
        public void Draw()
        {
            for (int i = 0; i < BaseBoard.GetLength(0); i++)
            {
                for (int j = 0; j < BaseBoard.GetLength(1); j++)
                {
                    if (PreStateBoard[i,j].Body != BaseBoard[i,j].Body | PreStateBoard[i,j].Color != BaseBoard[i,j].Color)
                    {
                        Console.SetCursorPosition(i,j);
                        Console.ForegroundColor = BaseBoard[i,j].Color;
                        Console.Write(BaseBoard[i,j].Body);
                    }

                }
            }
            Console.SetCursorPosition(0, 0);
            CopyBase2Pre();
        }
        #endregion

        #region доп методы

        /// <summary>
        /// копирование базового массива в массив "предыдущего состояния"
        /// </summary>
        void CopyBase2Pre()
        {
            for (int i = 0; i < BaseBoard.GetLength(0); i++)
            {
                for (int j = 0; j < BaseBoard.GetLength(1); j++)
                {
                    PreStateBoard[i,j].Body = BaseBoard[i,j].Body;
                    PreStateBoard[i,j].Color = BaseBoard[i,j].Color;
                }
            }
        }

        /// <summary>
        /// копирование 2D массива в базовый
        /// </summary>
        void CopyArray2Base(PieceChar[,] obj)
        {
            for (int i = 0; i < obj.GetLength(0); i++)
            {
                for (int j = 0; j < obj.GetLength(1); j++)
                {
                    BaseBoard[obj[i,j].Y, obj[i,j].X].Body = obj[i,j].Body;
                    BaseBoard[obj[i,j].Y, obj[i,j].X].Color = obj[i,j].Color;
                }
            }
        }

        /// <summary>
        /// копирование массива в базовый
        /// </summary>
        /// <param name="obj"></param>
        void CopyArray2Base(PieceChar[] obj)
        {
            for (int i = 0; i < obj.Length; i++)
            {
                BaseBoard[obj[i].Y, obj[i].X].Body = obj[i].Body;
                BaseBoard[obj[i].Y, obj[i].X].Color = obj[i].Color;
            }
        }

        /// <summary>
        /// очистка всего массива ' '
        /// </summary>
        /// <param name="obj">массив</param>
        void ClearArray(ref PieceChar[,] obj)
        {
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                for (int j = 0; j < Console.WindowWidth; j++)
                {
                    obj[i,j].Body = ' ';
                    obj[i,j].Color = ConsoleColor.White;
                }
            }
        }

        /// <summary>
        /// чтение карты из файла
        /// </summary>
        /// <param name="obj">массив</param>
        /// <param name="name">имя файла</param>
        void ReadFiles(PieceChar[,] obj, string name)
        {
            string[] words = File.ReadAllLines(name);

            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 80; j++)
                {
                    if (words[i][j] != '.')
                        obj[i,j].Body = words[i][j];
                }
            }
        }

        void InputKey()
        {
            if (keys.Key == ConsoleKey.UpArrow)
            {
                direcY = -1;
                direcX = 0;
            }
            if (keys.Key == ConsoleKey.DownArrow)
            {
                direcY = 1;
                direcX = 0;
            }
            if (keys.Key == ConsoleKey.LeftArrow)
            {
                direcY = 0;
                direcX = -1;
            }
            if (keys.Key == ConsoleKey.RightArrow)
            {
                direcY = 0;
                direcX = 1;
            }
        }
        #endregion

        #region перегрузки

        #endregion
    }
}