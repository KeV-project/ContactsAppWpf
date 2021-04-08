using System;

namespace ContactsAppModel
{
    /// <summary>
    /// Класс <see cref="PhoneNumber"/> хранит информацию 
    /// о номере телефона контакта
    /// </summary>
    public class PhoneNumber: IEquatable<PhoneNumber>
	{
        /// <summary>
        /// Поле предназначено для хранения номера телефона контакта
        /// </summary>
        private long _number;

        /// <summary>
        /// Возвращает и создает номер телефона контакта
        /// Номер телефона должен начинаться с цифры "7" и состоять из 11 цифр
        /// </summary>
        public long Number
        {
            get
            {
                return _number;
            }
            set
            { 
                _number = value;
                ValueValidator.AssertRussianPhoneNumber(value,
                        "номер телефона");
            }
        }

        /// <summary>
        /// Инициализирует свойства объекта класса <see cref="PhoneNumber"/>
        /// значениями по умолчанию
        /// </summary>
        public PhoneNumber() : this(70000000000){}

        /// <summary>
        /// Инициализирует объект класса <see cref="PhoneNumber">
        /// </summary>
        /// <param name="number">Номер телефона контакта</param>
        public PhoneNumber(long number)
        {
            Number = number;
        }

        /// <summary>
		/// Определяет равенство двух объектов.
		/// </summary>
		/// <param name="obj">Сравниваемый объект</param>
		/// <returns>Возвращает true, если объекты равны.
		/// Иначе возвращает false.</returns>
		public override bool Equals(object obj)
		{
			return Equals(obj as PhoneNumber);
		}

        /// <summary>
		/// Определяет равенство двух объектов.
		/// класса <see cref="PhoneNumber"/>
		/// </summary>
		/// <param name="other">Сравниваемый объект</param>
		/// <returns>Возвращает true, если объекты равны.
		/// Иначе возвращает false.</returns>
		public bool Equals(PhoneNumber other)
		{
			return other != null &&
				   _number == other._number &&
				   Number == other.Number;
		}


        /// <summary>
        /// Метод предназначен для генерации хеша
        /// </summary>
        /// <returns>Возвращает хеш объекта
        /// класса <see cref="PhoneNumber"/></returns>
        public override int GetHashCode()
		{
			int hashCode = 1729877619;
			hashCode = hashCode * -1521134295 + _number.GetHashCode();
			hashCode = hashCode * -1521134295 + Number.GetHashCode();
			return hashCode;
		}
	}
}
