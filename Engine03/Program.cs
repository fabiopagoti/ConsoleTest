using System;
using System.IO;
using System.Threading;

namespace WordFinder
{
    /// <summary>
    /// Класс Program содержит главный цикл, окончание игры, запуск таймера,
    /// и обработку нажития пользователем Ctrl-C для выхода из игры. Весь геймплей и отрисовка
    /// обрабатывается в классе Game.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Длина головоломки в буквах
        /// </summary>
        const int PUZZLE_LENGTH = 49;

        /// <summary>
        /// Каждая n-я буква должна быть гласной
        /// </summary>
        const int VOWEL_EVERY = 5;

        /// <summary>
        /// Отведенный лимит времени
        /// </summary>
        const int TIME_LIMIT_SECONDS = 60;

        /// <summary>
        /// Список слов -- Я скачал отсюда http://unix-tree.huihoo.org/V7/usr/dict/words.html
        /// и вставил в words.txt, не забудьте установить своейство копирования в целевую директорию
        /// в "Copy Always" (таким образом он будет в той же папке, где и запускной файл). Конечно, мы можем использовать
        /// файл ресурса, но использование текстового файла позволяет расширить игру, используя различные списки слов
        /// </summary>
        static string[] words = File.ReadAllLines("words.txt");

        /// <summary>
        /// Объект для отслеживания игры
        /// </summary>
        static Game game;

        /// <summary>
        /// Установите это свойство в true для выхода из игры
        /// </summary>
        static bool quit = false;

        /// <summary>
        /// Текущий пользовательский ввод
        /// </summary>
        static string word = String.Empty;

        /// <summary>
        /// Входная точка установки экрана, инициализации игры и запуск главного цикла
        /// </summary>
        static void Main(string[] args)
        {
            // Будьте уверены, что пользователь нажмет Ctrl-C для выхода из игры
            // Установите Console.TreatControlCAsInput в true если хотите использовать Ctrl-C как  допустимое входное значение
            Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);

            Console.CursorVisible = false;

            game = new Game(PUZZLE_LENGTH, VOWEL_EVERY, words);
            game.DrawInititalScreen();
            MainLoop();
        }

        /// <summary>
        /// Обработка нажатия Ctrl-C
        /// </summary>
        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            // К сожалению, в связи с ошибкой в .NET Framework v4.0.30319 вы не можете олаживать это
            // потому что Visual Studio 2010 выдает ошибку "No Source Available".
            // http://connect.microsoft.com/VisualStudio/feedback/details/524889/debugging-c-console-application-that-handles-console-cancelkeypress-is-broken-in-net-4-0
            Console.SetCursorPosition(0, 19);
            Console.WriteLine("Нажата {0}, выход...", e.SpecialKey);
            quit = true;
            e.Cancel = true; // Установка в true позволяет на закрывать программу сразу
        }

        /// <summary>
        /// Главный цикл игры
        /// </summary>
        static void MainLoop()
        {
            int elapsedMilliseconds = 0;
            int totalMilliseconds = TIME_LIMIT_SECONDS * 1000;
            const int INTERVAL = 100;

            while (elapsedMilliseconds < totalMilliseconds && !quit)
            {
                // Задержка на определенный период
                Thread.Sleep(INTERVAL);
                elapsedMilliseconds += INTERVAL;

                HandleInput();

                PrintRemainingTime(elapsedMilliseconds, totalMilliseconds);
            }

            Console.SetCursorPosition(0, 20);
            Console.WriteLine(Environment.NewLine + Environment.NewLine
                + "Игра окончена! Вы нашли {0} слов.", game.NumberFound);
        }

        /// <summary>
        /// Запись оставшегося времени в правый верхний угол экрана
        /// </summary>
        /// <param name="elapsedMilliseconds">Пройденное время с начала запуска игры</param>
        /// <param name="totalMilliseconds">Общее количество милисекудн установленное для игры</param>
        private static void PrintRemainingTime(int elapsedMilliseconds, int totalMilliseconds)
        {
            int milliSecondsLeft = totalMilliseconds - elapsedMilliseconds;
            double secondsLeft = (double)milliSecondsLeft / 1000;
            string timeString = String.Format("{0:00.0} секунд осталось", secondsLeft);

            // Сохранение позиции курсора
            int left = Console.CursorLeft;
            int top = Console.CursorTop;

            // Рисуем время в правом верхнем углу экрана
            Console.SetCursorPosition(Console.WindowWidth - timeString.Length, 0);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(timeString);

            // Восстановление цвета текста консоли и установка курсора на последнюю позицию
            Console.ResetColor();
            Console.SetCursorPosition(left, top);
        }

        /// <summary>
        /// Обрабатывать любые нажатия клавиш пользователем
        /// </summary>
        static void HandleInput()
        {
            Thread.Sleep(50);
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (word.Length > 0)
                        word = word.Substring(0, word.Length - 1);
                }
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    word = String.Empty;
                }
                else
                {
                    string key = keyInfo.KeyChar.ToString().ToUpper();
                    if (game.IsValidLetter(key))
                    {
                        word = word + key;
                    }
                }
                game.CurrentInput = word;
                game.ProcessInput();
                game.UpdateScreen();
            }
        }
    }
}