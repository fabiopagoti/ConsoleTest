using System;
using System.Collections.Generic;
using System.Linq;

namespace WordFinder
{
    /// <summary>
    /// Класс Game отслеживает состояние игры и рисует на экране и обновляет его
    /// Он использует экземпляр класса Puzzle следить за буквами в таблице
    /// головоломки, а экземпляр WordChecker следить за
    /// словами и занимается проверкой ответов игрока.
    /// </summary>
    class Game
    {
        /// <summary>
        /// Объект WordChecker проверяет слова
        /// </summary>
        private WordChecker wordChecker;

        /// <summary>
        /// Получить количество найденных слов
        /// </summary>
        public int NumberFound
        {
            // Объет WordChecker содержит эту информацию
            get { return wordChecker.NumberFound; }
        }

        /// <summary>
        /// Объект Puzzle содержит буквы и проверяет слова
        /// </summary>
        private Puzzle puzzle;

        /// <summary>
        /// Текущей пользовательский ввод
        /// </summary>
        public string CurrentInput { private get; set; }

        /// <summary>
        /// Конструктор класса Game
        /// </summary>
        /// <param name="puzzleLength">Количество букв в головоломке</param>
        /// <param name="vowelEvery">Добавлять гласную каждую n-ю букву</param>
        /// <param name="validWords">Список проверочных слов</param>
        public Game(int puzzleLength, int vowelEvery, IEnumerable<string> validWords)
        {
            this.wordChecker = new WordChecker(validWords);
            this.puzzle = new Puzzle(puzzleLength, vowelEvery);
            CurrentInput = String.Empty;
        }

        /// <summary>
        /// Рисуем на экране консоли стартовую заставку
        /// </summary>
        public void DrawInititalScreen()
        {
            Console.Clear();
            Console.Title = "Word finder";
            puzzle.Draw(25, 3);
            Console.SetCursorPosition(7, 11);
            Console.Write("--------------------------------------------------------¬");
            Console.SetCursorPosition(7, 12);
            Console.Write("¦");
            Console.SetCursorPosition(63, 12);
            Console.Write("¦");
            Console.SetCursorPosition(7, 13);
            Console.Write("L=======================================================-");
            UpdateScreen();
        }

        /// <summary>
        /// Обновляем экран
        /// </summary>
        public void UpdateScreen()
        {
            // Используем String.PadRight() чтобы убедиться, что желтое поле ввода остается постоянного
            // размера, вне зависимости от длины слова
            Console.SetCursorPosition(8, 12);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.BackgroundColor = ConsoleColor.Yellow;
            string message = String.Format("Введите слово #{0}: {1}",
                    wordChecker.NumberFound, CurrentInput);
            Console.Write(message.PadRight(54));
            Console.ResetColor();

            Console.SetCursorPosition(0, 17);
            Console.Write("Найдено слов: ");
            foreach (string word in wordChecker.FoundWords)
                Console.Write("{0} ", word);

            Console.SetCursorPosition(7, 14);
            Console.Write("Наберайте любые слова, которые вы нашли. <ESC> очищает поле.");
        }

        /// <summary>
        /// Обработка слова при каждом вводе
        /// </summary>
        public void ProcessInput()
        {
            wordChecker.CheckAnswer(CurrentInput, puzzle);
        }

        /// <summary>
        /// Возвращает true, если нажата правильная буква из набора.
        /// </summary>
        /// <param name="key">Буква, которая была нажата</param>
        /// <returns>True если буква гласная или согласная из таблицы</returns>
        public bool IsValidLetter(string key)
        {
            if (key.Length == 1)
            {
                char c = key.ToCharArray()[0];
                return Puzzle.Consonants.Contains(c) || Puzzle.Vowels.Contains(c);
            }
            return false;
        }

    }
}