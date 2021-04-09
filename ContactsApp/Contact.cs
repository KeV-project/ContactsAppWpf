﻿using System;

namespace ContactsAppModel
{
    /// <summary>
    /// Класс <see cref="Contact"/> хранит информацию о контакте
    /// </summary>
    public class Contact: ICloneable, IEquatable<Contact>
    {
        /// <summary>
        /// Cодержит имя контакта
        /// </summary>
        private string _firstName;

        /// <summary>
        /// Возвращает и устанавливает имя контакта
        /// Имя контакта должно состоять не более чем из 50 символов
        /// </summary>
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = ValueCorrector.ToUpperFirstLetter(value);

                const int minLength = 0;
                const int maxLength = 50;
                ValueValidator.AssertCorrectName(value,
                       minLength, maxLength, "имя контакта");
            }
        }

        /// <summary>
        /// Содержит фамилию контакта
        /// </summary>
        private string _lastName;

        /// <summary>
        /// Возвращает и устанавливает фамилию контакта.
        /// Фамилия контакта должна состоять не более чем из 50 символов
        /// </summary>
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = ValueCorrector.ToUpperFirstLetter(value);

                const int minLength = 0;
                const int maxLength = 50;
                ValueValidator.AssertCorrectName(value,
                        minLength, maxLength, "фамилию контакта");
            }
        }

        /// <summary>
        /// Возвращает и задает номер телефона контакта
        /// </summary>
        public PhoneNumber Number { get; set; }

        /// <summary>
        /// Хранит адрес электронной почты контакта
        /// </summary>
        private string _email;

        /// <summary>
        /// Возвращает и устанавливает e-mail контакта
        /// Адрес электронной почты должен состоять 
        /// не более чем из 50 символов
        /// </summary>
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                if(_email.Length > 0)
				{
                    ValueValidator.AssertCorrectEmail(value);
                }
            }
        }
        /// <summary>
        /// Содержит дату рождения контакта
        /// </summary>
        private DateTime _birthDate;

        /// <summary>
        /// Возвращает и устанавливает дату рождения контакта
        /// Дата рождения не может быть раньше 1900 года и
        /// позже текущей даты 
        /// </summary>
        public DateTime BirthDate
        {
            get
            {
                return _birthDate;
            }
            set
            {
                _birthDate = value;

                DateTime minDate = new DateTime(1900, 12, 31, 23, 59, 59);
                DateTime maxDate = new DateTime(DateTime.Today.Year,
                    DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);
                ValueValidator.AssertCorrectDate(value, minDate, maxDate,
                     "рождения");
            }
        }

        /// <summary>
        /// Инициализирует объект класса <see cref="Contact"/>
        /// значениями по умолчанию
        /// </summary>
        public Contact(): this("", "", new PhoneNumber(), 
            "", DateTime.Today)
        {
            
        }

        /// <summary>
        /// Инициализирует объект класса <see cref="Contact"/>
        /// </summary>
        /// <param name="firstName">Имя контакта</param>
        /// <param name="lastName">Фамилия контакта</param>
        /// <param name="number">Номер телефона контакта</param>
        /// <param name="email">Адрес электронной почты контакта</param>
        /// <param name="birthDate">Дата рождения контакта</param>
        public Contact(string firstName, string lastName,
            PhoneNumber number, string email, DateTime birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            Number = number;
            Email = email;
            BirthDate = birthDate;
        }

        /// <summary>
        /// Позволяет создать объект класса, 
        /// скопировав значения полей другого объекта
        /// </summary>
        /// <returns>Возвращает копию объекта</returns>
        public object Clone()
        {
            PhoneNumber number = new PhoneNumber(Number.Number);
            DateTime birthDate = new DateTime(BirthDate.Year,
                BirthDate.Month, BirthDate.Day);
            return new Contact(FirstName, LastName, 
                number, Email, birthDate);
        }

		/// <summary>
		/// Определяет равенство двух объектов
		/// класса <see cref="Contact"/>
		/// </summary>
		/// <param name="other">Сравниваемый объект</param>
		/// <returns>Возвращает true, если объекты равны.
		/// Иначе возвращает false</returns>
		public bool Equals(Contact other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;

			return GetHashCode().Equals(other.GetHashCode());
		}

		/// <summary>
		/// Определяет равенство двух объектов.
		/// </summary>
		/// <param name="obj">Сравниваемый объект</param>
		/// <returns>Возвращает true, если объекты равны.
		/// Иначе возвращает false</returns>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;

			return Equals((Contact)obj);
		}

		/// <summary>
		/// Метод предназначен для генерации хеша
		/// </summary>
		/// <returns>Возвращает хеш объекта
		/// класса <see cref="Contact"/></returns>
		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (_firstName != null 
                    ? _firstName.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (_lastName != null 
                    ? _lastName.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (_email != null 
                    ? _email.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ _birthDate.GetHashCode();
				hashCode = (hashCode * 397) ^ (Number != null 
                    ? Number.GetHashCode() : 0);
				return hashCode;
			}
		}
	}
}
