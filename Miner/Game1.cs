using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Miner
{
    class Game1
    {
        char[,] massiv = { {'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O'}, //point 2,2
                           {'O', 'O', 'X', 'X', 'O', 'O', 'O', 'O', 'O', 'O'},
                           {'O', 'X', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O'},
                           {'O', 'O', 'O', 'X', 'O', 'O', 'O', 'O', 'O', 'O'},
                           {'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O'},
                           {'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O'},
                           {'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O'},
                           {'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O'},
                           {'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O'},
                           {'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O'}};

        int count;

        public Game1()
        {
            PrintArray();
            Console.ReadLine();
            CountMine(2, 2);
            PrintArray();
        }

        /*---------------------------------------*/

        private void CountMine(int X, int Y)
        {
            for (int column = Y - 1; column <= Y + 1; column++)
            {
                for (int row = X - 1; row <= X + 1; row++)
                {
                    if (massiv[row, column] == 'X')
                    {
                        count++;
                    }
                }
            }

            char a = Convert.ToChar( count.ToString());

            massiv[X, Y] = a;
        }

        private void PrintArray()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write(massiv[i, j]);
                }
            }
        }

    }
}
