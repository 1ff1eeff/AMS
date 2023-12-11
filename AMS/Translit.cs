using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AMS
{
    /// <summary>
    /// Класс представляющий функционал транслитирации текста.
    /// </summary>
    public class Translit
    {
        /// <summary>
        /// Конструктор класса Translit.
        /// </summary>
        /// <param name="Compat">Режим совместимости.</param>
        public Translit(bool Compat)
        {
            // Устанавливаем режим совместимости.

            Compatibility = Compat;
        }

        // Объявляем и инициализируем словарь содержащий пару ключ-значение: символ-строка,
        // т.к. не все русские буквы можно представить одним латинским символом.

        private readonly Dictionary<char, string> _translitDict = new Dictionary<char, string>();

        // Объявляем и инициализируем логическую переменную хранящую значение режима совместимости.

        private bool _compatibility = false;

        // Метод доступа к полю "режим совместимости". 
        public bool Compatibility
        {
            // Геттер.

            get
            {
                // Покидаем функцию и возвращаем значение режима совместимости.

                return _compatibility;
            }

            // Сеттер.

            set
            {
                // Если в функцию передано значение "истина".

                if (value)
                {
                    // Устанавливаем словарь в режиме совместимости.

                    FormDictCompat();

                    // Режим совместимости включен.

                    _compatibility = true;
                }

                // Если в функцию передано значение "ложь".

                else
                {
                    // Устанавливаем стандартный словарь.

                    FormDictStandart();

                    // Режим совместимости отключен.

                    _compatibility = false;
                }
            }
        }

        /// <summary>
        /// Устанавливает стандартный словарь.
        /// </summary>
        private void FormDictStandart()
        {
            // Очищаем словарь.

            _translitDict.Clear();

            // Заглавные буквы – стандартная транслитерация.
            _translitDict.Add('А', "A");
            _translitDict.Add('Б', "B");
            _translitDict.Add('В', "V");
            _translitDict.Add('Г', "G");
            _translitDict.Add('Д', "D");
            _translitDict.Add('Е', "E");
            _translitDict.Add('Ё', "Yo");
            _translitDict.Add('Ж', "Zh");
            _translitDict.Add('З', "Z");
            _translitDict.Add('И', "I");
            _translitDict.Add('Й', "J");
            _translitDict.Add('К', "K");
            _translitDict.Add('Л', "L");
            _translitDict.Add('М', "M");
            _translitDict.Add('Н', "N");
            _translitDict.Add('О', "O");
            _translitDict.Add('П', "P");
            _translitDict.Add('Р', "R");
            _translitDict.Add('С', "S");
            _translitDict.Add('Т', "T");
            _translitDict.Add('У', "U");
            _translitDict.Add('Ф', "F");
            _translitDict.Add('Х', "Kh");
            _translitDict.Add('Ц', "Ts");
            _translitDict.Add('Ч', "Ch");
            _translitDict.Add('Ш', "Sh");
            _translitDict.Add('Щ', "Shch");
            _translitDict.Add('Ъ', "''");
            _translitDict.Add('Ы', "Y");
            _translitDict.Add('Ь', "'");
            _translitDict.Add('Э', "E");
            _translitDict.Add('Ю', "Ju");
            _translitDict.Add('Я', "Ja");

            // Строчные буквы – стандартная транслитерация.
            _translitDict.Add('а', "a");
            _translitDict.Add('б', "b");
            _translitDict.Add('в', "v");
            _translitDict.Add('г', "g");
            _translitDict.Add('д', "d");
            _translitDict.Add('е', "e");
            _translitDict.Add('ё', "yo");
            _translitDict.Add('ж', "zh");
            _translitDict.Add('з', "z");
            _translitDict.Add('и', "i");
            _translitDict.Add('й', "j");
            _translitDict.Add('к', "k");
            _translitDict.Add('л', "l");
            _translitDict.Add('м', "m");
            _translitDict.Add('н', "n");
            _translitDict.Add('о', "o");
            _translitDict.Add('п', "p");
            _translitDict.Add('р', "r");
            _translitDict.Add('с', "s");
            _translitDict.Add('т', "t");
            _translitDict.Add('у', "u");
            _translitDict.Add('ф', "f");
            _translitDict.Add('х', "kh");
            _translitDict.Add('ц', "ts");
            _translitDict.Add('ч', "ch");
            _translitDict.Add('ш', "sh");
            _translitDict.Add('щ', "shch");
            _translitDict.Add('ъ', "''");
            _translitDict.Add('ы', "y");
            _translitDict.Add('ь', "'");
            _translitDict.Add('э', "e");
            _translitDict.Add('ю', "ju");
            _translitDict.Add('я', "ja");
        }

        /// <summary>
        /// Устанавливает словарь в режиме совместимости.
        /// </summary>
        private void FormDictCompat()
        {
            // Очищаем словарь.

            _translitDict.Clear();

            // Заглавные буквы – режим совместимости.
            _translitDict.Add('А', "A");
            _translitDict.Add('Б', "B");
            _translitDict.Add('В', "V");
            _translitDict.Add('Г', "G");
            _translitDict.Add('Д', "D");
            _translitDict.Add('Е', "E");
            _translitDict.Add('Ё', "Yo");
            _translitDict.Add('Ж', "Zh");
            _translitDict.Add('З', "Z");
            _translitDict.Add('И', "I");
            _translitDict.Add('Й', "J");
            _translitDict.Add('К', "K");
            _translitDict.Add('Л', "L");
            _translitDict.Add('М', "M");
            _translitDict.Add('Н', "N");
            _translitDict.Add('О', "O");
            _translitDict.Add('П', "P");
            _translitDict.Add('Р', "R");
            _translitDict.Add('С', "S");
            _translitDict.Add('Т', "T");
            _translitDict.Add('У', "U");
            _translitDict.Add('Ф', "F");
            _translitDict.Add('Х', "Kh");
            _translitDict.Add('Ц', "Ts");
            _translitDict.Add('Ч', "Ch");
            _translitDict.Add('Ш', "Sh");
            _translitDict.Add('Щ', "Shch");
            _translitDict.Add('Ъ', "_");
            _translitDict.Add('Ы', "Y");
            _translitDict.Add('Ь', "_");
            _translitDict.Add('Э', "E");
            _translitDict.Add('Ю', "Ju");
            _translitDict.Add('Я', "Ja");

            // Строчные буквы – режим совместимости.
            _translitDict.Add('а', "a");
            _translitDict.Add('б', "b");
            _translitDict.Add('в', "v");
            _translitDict.Add('г', "g");
            _translitDict.Add('д', "d");
            _translitDict.Add('е', "e");
            _translitDict.Add('ё', "yo");
            _translitDict.Add('ж', "zh");
            _translitDict.Add('з', "z");
            _translitDict.Add('и', "i");
            _translitDict.Add('й', "j");
            _translitDict.Add('к', "k");
            _translitDict.Add('л', "l");
            _translitDict.Add('м', "m");
            _translitDict.Add('н', "n");
            _translitDict.Add('о', "o");
            _translitDict.Add('п', "p");
            _translitDict.Add('р', "r");
            _translitDict.Add('с', "s");
            _translitDict.Add('т', "t");
            _translitDict.Add('у', "u");
            _translitDict.Add('ф', "f");
            _translitDict.Add('х', "kh");
            _translitDict.Add('ц', "ts");
            _translitDict.Add('ч', "ch");
            _translitDict.Add('ш', "sh");
            _translitDict.Add('щ', "shch");
            _translitDict.Add('ъ', "_");
            _translitDict.Add('ы', "y");
            _translitDict.Add('ь', "_");
            _translitDict.Add('э', "e");
            _translitDict.Add('ю', "ju");
            _translitDict.Add('я', "ja");
            _translitDict.Add(' ', "_");
        }

        /// <summary>
        /// Транслитерирует строку.
        /// </summary>
        /// <param name="Rus">Транслитерируемая строка.</param>
        /// <returns>Транслитерированная строка.</returns>
        public string TranslitString(string Rus)
        {
            // Объявляем и инициализируем изменяемую строку символов, для хранения транслитерированной строки.

            StringBuilder sb = new StringBuilder();

            // Если включен режим совместимости.

            if (_compatibility)
            {
                // Если строка не содержит русских букв или пробелов.

                if (!ContainsRusOrSpace(Rus))
                {
                    return Rus;
                }
            }

            // Если режим совместимости отключен.

            else
            {
                // Если строка не содержит русских букв.

                if (!ContainsRus(Rus))
                {
                    return Rus;
                }
            }

            // Анализируем строку.

            for (int i = 0; i < Rus.Length; i++)
            {
                // Объявляем строковую переменную для хранения буфера транслитерирования.

                string sBuf;

                // Если буква содержится в словаре.

                if (_translitDict.ContainsKey(Rus[i]))
                {
                    // Помещаем транслитерированную букву в буферную переменную.

                    sBuf = _translitDict[Rus[i]];
                }

                // Если буквы нет в словаре.

                else
                {
                    // Помещаем букву в буферную переменную.

                    sBuf = Rus[i].ToString();
                }

                // Добавляем транслитерированную букву в изменяемую строку символов.

                sb.Append(sBuf);
            }

            // Покидаем функцию и возвращаем транслитерированную строку.

            return sb.ToString();
        }

        /// <summary>
        /// Строка содержит русские буквы.
        /// </summary>
        /// <param name="TestString"></param>
        /// <returns>True, если строка содержит русские буквы.</returns>
        public static bool ContainsRus(string TestString)
        {
            // Если строка содержит русские буквы возвращаем True.

            return Regex.IsMatch(TestString, @"[а-я]", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Строка содержит русские буквы или пробелы.
        /// </summary>
        /// <param name="TestString"></param>
        /// <returns>True, если строка содержит русские буквы или пробелы.</returns>
        public static bool ContainsRusOrSpace(string TestString)
        {
            // Если строка содержит русские буквы или пробелы.

            return Regex.IsMatch(TestString, @"[а-я]|\s", RegexOptions.IgnoreCase);
        }
    }
}
