using System;

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
            BaseBoard = new PieceChar[Console.WindowWidth, Console.WindowHeight];
            PreStateBoard = new PieceChar[Console.WindowWidth, Console.WindowHeight];
            Ground = new PieceChar[Console.WindowWidth, Console.WindowHeight];
            Wall = new PieceChar[Console.WindowWidth, Console.WindowHeight];
            Snake = new PieceChar[10];

            for (int i = 0; i < Console.WindowWidth; i++)
            {
                for (int j = 0; j < Console.WindowHeight; j++)
                {
                    BaseBoard[i, j] = new PieceChar(j, i, ConsoleColor.White, ' ');
                    PreStateBoard[i, j] = new PieceChar(j, i, ConsoleColor.White, ' ');
                    Ground[i, j] = new PieceChar(j, i, ConsoleColor.White, ' ');
                    Wall[i, j] = new PieceChar(j, i, ConsoleColor.White, ' ');
                }
            }

            for (int i = 1; i < Console.WindowWidth-1; i++)
            {
                Wall[i, 0].Body = '═';
                Wall[i, Console.WindowHeight - 1].Body = '═';
            }

            for (int i = 1; i < Console.WindowHeight - 1; i++)
            {
                Wall[0, i].Body = '║';
                Wall[Console.WindowWidth - 1, i].Body = '║';
            }

            Wall[0,0].Body ='╔';
            Wall[0, Console.WindowHeight - 1].Body = '╚';
            Wall[Console.WindowWidth - 1, 0].Body = '╗';
            Wall[Console.WindowWidth - 1, Console.WindowHeight - 1].Body = '╝';

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
            }

            if (keys.Key == ConsoleKey.Escape) Program.Quit();

            for (int i = 0; i < BaseBoard.GetLength(0); i++)
            {
                for (int j = 0; j < BaseBoard.GetLength(1); j++)
                {
                    BaseBoard[i, j].Body = Wall[i, j].Body;
                    BaseBoard[i, j].Color = Wall[i, j].Color;
                }
            }

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
                    if (PreStateBoard[i, j].Body != BaseBoard[i, j].Body | PreStateBoard[i, j].Color != BaseBoard[i, j].Color)
                    {
                        Console.SetCursorPosition(i, j);
                        Console.ForegroundColor = BaseBoard[i, j].Color;
                        Console.Write(BaseBoard[i, j].Body);
                    }
                    
                }
            }
            Console.SetCursorPosition(0, 0);
            CopyArray();
        }

        void CopyArray()
        {
            for (int i = 0; i < BaseBoard.GetLength(0); i++)
            {
                for (int j = 0; j < BaseBoard.GetLength(1); j++)
                {
                    PreStateBoard[i, j].Body = BaseBoard[i, j].Body;
                    PreStateBoard[i, j].Color = BaseBoard[i, j].Color;
                }
            }
        }
    }
}