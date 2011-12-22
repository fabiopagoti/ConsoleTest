using System;

namespace ConsoleEngine
{
    /// <summary>
    /// основной класс игры
    /// </summary>
    class Game
    {
        ConsoleKeyInfo keys;

        /// <summary>
        /// метод инициализации игры
        /// </summary>
        public void Run()
        {

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
        }

        /// <summary>
        /// отрисовка
        /// </summary>
        public void Draw()
        {

        }
    }
}