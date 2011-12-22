using System;
using System.Collections.Generic;
using System.Linq;

namespace WordFinder
{
    /// <summary>
    /// Класс WordChecker содержит список правильных слов и проверяет
    /// буквы введенные пользователем с буквами из таблицы
    /// </summary>
    class WordChecker
    {
        /// <summary>
        /// Список слов для проверки
        /// </summary>
        private List<string> words = new List<string>();

        /// <summary>
        /// Найденные слова
        /// </summary>
        private List<string> foundWords = new List<string>();

        /// <summary>
        /// Возвращает количество найденных слов
        /// </summary>
        public int NumberFound
        {
            get { return foundWords.Count; }
        }

        /// <summary>
        /// Возвращает найденные слова
        /// </summary>
        public IEnumerable<string> FoundWords
        {
            get
            {
                List<string> value = new List<string>();
                foreach (string word in foundWords)
                {
                    value.Add(word.ToUpper());
                }
                return value;
            }
        }

        /// <summary>
        /// Конструктор WordChecker
        /// </summary>
        /// <param name="validWords">Список правильных слов</param>
        public WordChecker(IEnumerable<string> validWords)
        {
            // Перевести слово в верхний регист и добавить в список
            foreach (string word in validWords)
                this.words.Add(word.ToUpper());
        }

        /// <summary>
        /// Проверить введенное польвателем слова с головоломной
        /// </summary>
        /// <param name="word">Проверяемое слово</param>
        /// <param name="puzzle">Ссылка на объект головоломки</param>
        public void CheckAnswer(string word, Puzzle puzzle)
        {
            // Проверяем что слово не пустое, что такого слова еще нет и в нем содержится не менее 4-х букв
            if (String.IsNullOrEmpty(word) || foundWords.Contains(word) || word.Length < 4)
                return;

            // Переводим слово в верхний регистр -- строка upperCaseWord должна быть уничтожена,
            // и поэтому нам нужна копия. Мы удаляем каждую найденную букву, пока в слове не останется букв.
            // Если буквы останутся, то слово считается не верным
            string upperCaseWord = word.ToUpper();
            if (words.Contains(upperCaseWord))
            {
                // Проверяем, что слово состоит из букв головоломки
                foreach (char letter in puzzle.Letters)
                {
                    // Удаляем букву, если она содержится в головоломке
                    if (upperCaseWord.Contains(letter))
                    {
                        // Если слово начинается с буквы, иначе Substring(0, index - 1) выдаст исключение
                        if (upperCaseWord.StartsWith(letter.ToString()))
                            upperCaseWord = upperCaseWord.Substring(1);
                        else
                        {
                            int index = upperCaseWord.IndexOf(letter);
                            upperCaseWord = upperCaseWord.Substring(0, index - 1) + upperCaseWord.Substring(index + 1);
                        }
                    }
                }
            }

            // Если из слова удалены все буквы, то считается что слово найдено.
            // Издаем звуковой сигнал и добавляем слово в список найденных.
            if (String.IsNullOrEmpty(upperCaseWord))
            {
                Console.Beep();
                foundWords.Add(word);
            }
        }
    }
}
