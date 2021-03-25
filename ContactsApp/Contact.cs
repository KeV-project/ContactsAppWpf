﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ContactsAppModel
{
    /// <summary>
    /// Класс <see cref="Contact"/> предназначен для создания контактов 
    /// </summary>
    public class Contact : ModelBase, ICloneable, IComparable<Contact>
    {
        /// <summary>
        /// Cодержит имя контакта
        /// </summary>
        private string _firstName;

        /// <summary>
        /// Возвращает и создает имя контакта
        /// Имя контакта должна состоять не более чем из 50 символов,
        /// но не менее чем из 1
        /// </summary>
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                const int minLength = 1;
                const int maxLength = 50;
				try
				{
                    ValueValidator.AssertCorrectName(value,
                        minLength, maxLength, "имя контакта");
                    RemoveError("FirstName");
                }
                catch(ArgumentException e)
				{
                    AddError("FirstName", e.Message);
				}

                _firstName = ValueCorrector.ToUpperFirstLetter(value);

                OnPropertyChanged("FirstName");
            }
        }

        /// <summary>
        /// Содердит фамилию контакта
        /// </summary>
        private string _lastName;

        /// <summary>
        /// Возвращает и создает фамилию контакта.
        /// Фамилия контакта должна состоять не более чем из 50 символов.
        /// Данное поле необязательно для заполнения
        /// </summary>
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                const int minLength = 0;
                const int maxLength = 50;
                try
                {
                    ValueValidator.AssertCorrectName(value,
                        minLength, maxLength, "фамилия контакта");
                    RemoveError("LastName");
                }
                catch (ArgumentException e)
                {
                    AddError("LastName", e.Message);
                }
               
                _lastName = ValueCorrector.ToUpperFirstLetter(value);

                OnPropertyChanged("LastName");
            }
        }

        /// <summary>
        /// Возвращает и задает номер телефона контакта
        /// </summary>
        public PhoneNumber Number { get; set; }

        /// <summary>
        /// Содержит адрес электронной почты контакта
        /// </summary>
        private string _email;

        /// <summary>
        /// Возвращает и создает e-mail контакта
        /// Адрес электронной почты должен состоять не более чем из 50 символов.
        /// Поле необязательное для заполнения
        /// </summary>
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                value = value.Replace(" ", "");

                const int minLength = 0;
                const int maxLength = 50;
                try
                {
                    ValueValidator.AssertLengthInRange(value,
                        minLength, maxLength, "e-mail контакта");
                    RemoveError("Email");
                }
                catch (ArgumentException e)
                {
                    AddError("Email", e.Message);
                }
                
                _email = value;

                OnPropertyChanged("Email");
            }
        }
        /// <summary>
        /// Содержит дату рождения контакта
        /// </summary>
        private DateTime _birthDate;

        /// <summary>
        /// Возвращает и создает дату рождения контакта
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
                DateTime minDate = new DateTime(1900, 12, 31, 23, 59, 59);
                DateTime maxDate = new DateTime(DateTime.Today.Year,
                    DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);
                try
				{
                    ValueValidator.AssertCorrectDate(value, minDate, maxDate,
                     "рождения");
                    RemoveError("BirthDate");
                }
                catch(ArgumentException e)
				{
                    AddError("BirthDate", e.Message);
				}

                _birthDate = value;

                OnPropertyChanged("Birthday");
            }
        }

        /// <summary>
        /// Инициализирует объект класса <see cref="Contact">
        /// </summary>
        public Contact()
        {
            FirstName = "";
            LastName = "";
            Number = new PhoneNumber();
            Email = "";
            BirthDate = DateTime.Today;
        }

        /// <summary>
        /// Инициализирует объект класса <see cref="Contact">
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
        /// <returns> Возвращает копию объекта</returns>
        public object Clone()
        {
            PhoneNumber number = new PhoneNumber(Number.Number);
            DateTime birthDate = new DateTime(BirthDate.Year,
                BirthDate.Month, BirthDate.Day);
            return new Contact(FirstName, LastName, number, Email, birthDate);
        }

        /// <summary>
        /// Метод сравнивает два объекта класса по фамилии 
        /// </summary>
        /// <param name="contact">Объект, который будет сравниваться с
        /// текущем объектов</param>
        /// <returns>Возвращает значение меньше 0, если фамиия 
        /// текущего контакта предшествует фамилиии сравниваемого
        /// Возвращает 0, если позиции объектов в порядке сортировки 
        /// по фамилии совпадают
        /// Возвразает значение больше 0, если фамилия сравниваемого
        /// контакта предшествует фамилии текущего</returns>
        public int CompareTo(Contact contact)
        {
            return this.LastName.CompareTo(contact.LastName);
        }
    }
}
