using System;
using System.Threading;

namespace ConsoleEngine
{
    class Program
    {
        static Game game;

        /// <summary>
        /// life game =)
        /// </summary>
        static bool GameLife = true;

        /// <summary>
        /// точка входа
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            game = new Game();
            game.Run();
            MainLoop();
        }

        /// <summary>
        /// основной цикл игры
        /// </summary>
        static void MainLoop()
        {
            while(GameLife)
            {
                game.Draw();
                game.Update();

                Thread.Sleep(250); // задержка
            }
        }

        /// <summary>
        /// завершение програмы
        /// </summary>
        public static void Quit()
        {
            GameLife = false;
        }
    }
}