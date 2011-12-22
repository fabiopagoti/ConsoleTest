using System;
using System.Collections.Generic;
using System.Linq;

namespace WordFinder
{
    /// <summary>
    /// Класс Puzzle содержит таблицу головоломки. Игра использует его для отрисовки
    /// таблицы на экране, а класс WordFinder используется для проверки игрока,
    /// ввел ли он букву из таблицы
    /// </summary>
    class Puzzle
    {
        /// <summary>
        /// Randomizer
        /// </summary>
        private Random random = new Random();

        /// <summary>
        /// Согласные (содержит Y)
        /// </summary>
        public static readonly char[] Consonants = { 'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K',
               'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Y', 'Z' };

        /// <summary>
        /// Гласные (содержит Y)
        /// </summary>
        public static readonly char[] Vowels = { 'A', 'E', 'I', 'O', 'U', 'Y' };

        /// <summary>
        /// Резервное поле для букв
        /// </summary>
        char[] letters;

        /// <summary>
        /// Получить буквы из головоломки
        /// </summary>
        public IEnumerable<char> Letters
        {
            get { return letters; }
        }

        /// <summary>
        /// Номер буквы в головоломке
        /// </summary>
        private int puzzleLength;

        /// <summary>
        /// Конструктор класса Puzzle
        /// </summary>
        /// <param name="puzzleLength">Количество букв в головоломке</param>
        /// <param name="vowelEvery">Каждая n-я буква гласная</param>
        public Puzzle(int puzzleLength, int vowelEvery)
        {
            this.puzzleLength = puzzleLength;

            letters = new char[puzzleLength];

            for (int i = 0; i < puzzleLength; i++)
            {
                if (i % vowelEvery == 0)
                    letters[i] = Vowels[random.Next(Vowels.Length)];
                else
                    letters[i] = Consonants[random.Next(Consonants.Length)];
            }
        }

        /// <summary>
        /// Нарисовать головоломку в выбранном месте экрана
        /// </summary>
        /// <param name="left">Координата X</param>
        /// <param name="top">Координата Y</param>
        public void Draw(int left, int top)
        {
            int oldTop = Console.CursorTop;
            int oldLeft = Console.CursorLeft;

            Console.BackgroundColor = ConsoleColor.Gray;

            // Создать случайную головоломку и нарисовать ее
            for (int i = 0; i < puzzleLength; i++)
            {
                // Используя перемещение курсора рисуем строки в таблице головоломки
                if (i % Math.Floor(Math.Sqrt(puzzleLength)) == 0)
                {
                    Console.CursorTop = top++;
                    Console.CursorLeft = left;
                }

                if (Vowels.Contains(letters[i]))
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                else
                    Console.ForegroundColor = ConsoleColor.DarkBlue;

                Console.Write(" {0} ", letters[i]);
            }

            Console.ResetColor();

            Console.CursorTop = oldTop;
            Console.CursorLeft = oldLeft;
        }
    }
}
