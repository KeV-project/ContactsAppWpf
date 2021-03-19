using System;

namespace ContactsApp
{
    /// <summary>
    /// Класс <see cref="ValueCorrector"/> предназначен
    /// для корректировки пользовательского ввода
    /// </summary>
    public static class ValueCorrector
    {
        /// <summary>
        /// Метод предназначен для исключения из строки,
        /// содержащей номер телефона, всех посторонних символов
        /// </summary>
        /// <param name="number">Корректируемый номер телефона</param>
        /// <returns>Возвращает номер телефона 
        /// в целочисленном формате</returns>
        public static long ConvertPhoneNumberToInt(string number)
        {
            number = number.Replace(" ", "");
            number = number.Replace("+", "");
            number = number.Replace("(", "");
            number = number.Replace(")", "");
            number = number.Replace("-", "");
            return Convert.ToInt64(number);
        }

        /// <summary>
        /// Метод предназначен для корректирови 
        /// введенных пользователем имени или фамилии
        /// </summary>
        /// <param name="value">Корректируемая строка</param>
        /// <returns>Если строка пустая возвращает исходную строку.
        /// Если строка начинается с буквы, 
        /// возвращает ту же строку, 
        /// первая буква которой - заглавная</returns>
        public static string ToUpperFirstLetter(string value)
        {
            if (value.Length > 0)
            {
                value = Convert.ToString(value[0]).ToUpper()
                    + value.Substring(1);
            }
            return value;
        }
    }
}
