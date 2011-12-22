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

        public PieceChar(int x, int y, ConsoleColor color, char body)
        {
            this.X = x;
            this.Y = y;
            this.Color = color;
            this.Body = body;
        }
    }
}