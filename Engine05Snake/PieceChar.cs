using System;

namespace ConsoleEngine
{
    /// <summary>
    /// структура с описанием ячеек
    /// </summary>
    struct PieceChar
    {
        public int X;
        public int Y;
        public ConsoleColor Color;
        public char Body;

        /// <summary>
        /// ячейка
        /// </summary>
        /// <param name="y">строка</param>
        /// <param name="x">столбец</param>
        /// <param name="color">цвет</param>
        /// <param name="body">символ</param>
        public PieceChar(int y, int x, ConsoleColor color, char body)
        {
            this.X = x;
            this.Y = y;
            this.Color = color;
            this.Body = body;
        }
    }
}