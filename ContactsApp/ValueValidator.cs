using System;
using System.Linq;

namespace ContactsAppModel
{
    /// <summary>
    /// Класс <see cref="ValueValidator"/> предназначен для 
    /// проверки корректности значений перед их использованием
    /// </summary>
    public static class ValueValidator
    {
        /// <summary>
        /// Метод предназначен для проверки числа 
        /// на вхождение в определенных диапазон
        /// </summary>
        /// <param name="number">Проверяемое число</param>
        /// <param name="minLimit">Минимальное допустимое 
        /// значение числа</param>
        /// <param name="maxLimit">Максимальное допустимое 
        /// значение числа</param>
        /// <returns>Значение показывает, 
        /// входит ли число в допустимый диапазон </returns>
        private static bool IsValueInRange(double number,
            double minLimit, double maxLimit)
        {
            return minLimit <= number && maxLimit >= number;
        }

        /// <summary>
        /// Метод предназначен для проверки длины строки на
        /// допустимое количество
        /// </summary>
        /// <param name="value">Проверяемая строка</param>
        /// <param name="minLength">Минимальная допустимая 
        /// длина строки</param>
        /// <param name="maxLength">Максимальная допустимая 
        /// длина строки</param>
        /// <returns>Значение показывает, 
        /// является ли длина строки допустимой для использования</returns>
        private static bool IsLengthInRange(string value, int minLength,
            int maxLength)
        {
            return minLength <= value.Length
                && maxLength >= value.Length;
        }

        /// <summary>
        /// Метод предназначен для проверки строки
        /// на содержание только букв
        /// </summary>
        /// <param name="value">Проверяемая строка</param>
        /// <returns>Значение показывает 
        /// состоит ли строка только из букв</returns>
        private static bool IsOnlyLettersInString(string value)
        {
            value = value.ToLower();
            return value.All(symbol =>
                    symbol >= 'a' && symbol <= 'z' ||
                    symbol >= 'а' && symbol <= 'я');
        }

        /// <summary>
        /// Метод предназначен для проверки даты 
        /// на вхождение в определенный временной диапазон
        /// </summary>
        /// <param name="date">Проверяемая дата</param>
        /// <param name="minDate">Минимальная дата</param>
        /// <param name="maxDate">Максимальная дата</param>
        /// <returns>Значение показыает, 
        /// входит ли дата в допустимый временной диапазон</returns>
        private static bool IsCorrectDate(DateTime date,
            DateTime minDate, DateTime maxDate)
        {
            return minDate <= date && date <= maxDate;
        }

        /// <summary>
        /// Метод предназначен для генерации исключения
        /// в случае, если число не входит в допустимый диапазон
        /// </summary>
        /// <param name="value">Проверяемое число</param>
        /// <param name="minLimit">Минимальное допустимое 
        /// значение числа</param>
        /// <param name="maxLimit">Максимальное допустимое
        /// значение числа</param>
        /// <param name="context">Поле объекта, которое будет 
        /// инициализировано проверяемым значением 
        /// в именительном падеже</param>
        public static void AssertValueInRange(double value,
            double minLimit, double maxLimit, string context)
        {
            if (!IsValueInRange(value, minLimit, maxLimit))
            {
                throw new ArgumentException("ИСКЛЮЧЕНИЕ: Число "
                    + value
                    + "\nне входит в допустимый дипапазон ["
                    + minLimit + ", " + maxLimit + "]"
                    + "\nи не может определять " + context);
            }
        }

        /// <summary>
        /// Метод предназначен для генерации исключения
        /// в случае, если количество символов в строке не входит
        /// в допустимый диапазон
        /// </summary>
        /// <param name="value">Проверяемая строка</param>
        /// <param name="minLength">Минимальное количество 
        /// символов в строке</param>
        /// <param name="maxLength">Максимальное количество 
        /// символов в строке</param>
        /// <param name="context">Поле объекта, которое будет 
        /// инициализировано проверяемым значением 
        /// в именительном падеже</param>
        public static void AssertLengthInRange(string value,
            int minLength, int maxLength, string context)
        {
            if (!IsLengthInRange(value, minLength, maxLength))
            {
                throw new ArgumentException("ИСКЛЮЧЕНИЕ: \""
                    + value + "\"\n превышает допустимую длину ["
                    + minLength + ", " + maxLength + "]"
                    + "\nи не может определять " + context);
            }
        }

        /// <summary>
        /// Метод предназначен для генерации исключения
        /// в случае, если номер телефона не соответствует 
        /// российским стандартам телефонных номеров
        /// </summary>
        /// <param name="number">Проферяемое число</param>
        /// <param name="context">Поле объекта, которое будет 
        /// инициализировано проверяемым значением 
        /// в именительном падеже</param>
        public static void AssertRussianPhoneNumber(long number,
           string context)
        {
            const int minNumbers = 11;
            const int maxNumbers = 11;
            AssertLengthInRange(Convert.ToString(number),
                minNumbers, maxNumbers, context);

            if (number / 10000000000 != 7)
            {
                throw new ArgumentException("ИСКЛЮЧЕНИЕ: " +
                    "номер телефона \"" + number + "\"\n" +
                    "должен начинаться с цифры 7");
            }
        }

        /// <summary>
        /// Метод предназначен для генерации исключения
        /// в случае, если имя или фамилия содержат более 50 символов
        /// или содержат символы кроме букв
        /// </summary>
        /// <param name="name">Проферяемая строка</param>
        /// <param name="minLength">Минимальное количество символов</param>
        /// <param name="maxLength">Максимальное количество символов</param>
        /// <param name="context">Поле объекта, которое будет 
        /// инициализировано проверяемым значением 
        /// в именительном падеже</param>
        public static void AssertCorrectName(string name,
            int minLength, int maxLength, string context)
        {
            AssertLengthInRange(name, minLength, maxLength, context);

            if (!IsOnlyLettersInString(name))
            {
                throw new ArgumentException("ИСКЛЮЧЕНИЕ: строка "
                                 + "\"" + name
                                 + "\",\nопределяющая " + context
                                 + ",\nможет соделжать только буквы");
            }
        }

        /// <summary>
        /// Метод предназначен для генерации исключения
        /// в случае, если дата не входит в допустимый 
        /// временной промежуток
        /// </summary>
        /// <param name="newDate">Проверяемая дата</param>
        /// <param name="minDate">Минимальная дата</param>
        /// <param name="maxDate">Максималная дата</param>
        /// <param name="context">Поле объекта, которое 
        /// будет инициализировано проверяемым значением 
        /// в именительном падеже</param>
        public static void AssertCorrectDate(DateTime newDate,
            DateTime minDate, DateTime maxDate, string context)
        {
            if (!IsCorrectDate(newDate, minDate, maxDate))
            {
                throw new ArgumentException("ИСКЛЮЧЕНИЕ: "
                    + "выбранная дата " + context + " \""
                    + newDate + "\"\nне может быть раньше "
                    + minDate + " и позже " + maxDate);
            }
        }
    }
}
