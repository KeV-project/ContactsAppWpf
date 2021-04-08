﻿using System;
using ContactsAppModel;

namespace ContactsAppViewModel.ModelViewModels
{
	/// <summary>
	/// Класс <see cref="ContactViewModel"/> организует уровень
	/// защиты данных приложения от неккоректного ввода
	/// </summary>
	public class ContactViewModel: ModelViewModelBase, 
		IComparable<ContactViewModel>
    {
		/// <summary>
		/// Изменяемый контакт
		/// </summary>
        public Contact Contact { get; private set; }

        /// <summary>
		/// Устанавливает свойство контакта
		/// </summary>
		/// <param name="property"></param>
		/// <param name="setProperty"></param>
		private void SetProperty(string property, Action setProperty)
		{
			try
			{
				setProperty();
				RemoveError(property);
			}
			catch(ArgumentException ex)
			{
				AddError(property, ex.Message);
			}

			OnPropertyChanged(property);
		}

		/// <summary>
		/// Возвращает и устанавливает имя контакта
		/// </summary>
        public string FirstName
		{
			get
			{
                return Contact.FirstName;
			}
			set
			{
				SetProperty(nameof(FirstName), () =>
				{
					Contact.FirstName = value;
				});
			}
		}

		/// <summary>
		/// Возвращает и устанавливает фамилию контакта
		/// </summary>
        public string LastName
		{
			get
			{
                return Contact.LastName;
			}
			set
			{
				SetProperty(nameof(LastName), () =>
				{
					Contact.LastName = value;
				});
			}
		}

		/// <summary>
		/// Возвращает и устанавливает VM для объекта 
		/// класса <see cref="PhoneNumberViewModel"/>
		/// </summary>
		public PhoneNumberViewModel PhoneNumberViewModel
        {
            get;
        }

        /// <summary>
		/// Возвращает и устанавливает адрес электронной почты контакта
		/// </summary>
        public string Email
		{
			get
			{
                return Contact.Email;
			}
			set
			{
				SetProperty(nameof(Email), () =>
				{
					Contact.Email = value;
				});
			}
		}

		/// <summary>
		/// Возвращает и устанавливает дату рождения контакта
		/// </summary>
        public DateTime BirthDate
		{
			get
			{
                return Contact.BirthDate;
			}
			set
			{
				SetProperty(nameof(BirthDate), () =>
				{
					Contact.BirthDate = value;
				});
			}
		}

		/// <summary>
		/// Инициализирует объект класса <see cref="ContactViewModel"/>
		/// </summary>
		/// <param name="contact"></param>
		public ContactViewModel(Contact contact)
		{
			Contact = contact;
			PhoneNumberViewModel = new PhoneNumberViewModel(
				Contact.Number);
		}

		/// <summary>
		/// Метод предназначен для определения относительного порядка
		/// следования двух элементов
		/// </summary>
		/// <param name="contactViewModel">Сравниваемый объект</param>
		/// <returns>Возвращает значение меньше 0, если фамилия 
		/// контакта текущей VM предшествует фамилиии контакта 
		/// сравниваемой VM. Возвращает 0, если позиции объектов в 
		/// порядке сортировки по фамилии контакта совпадают.
		/// Возвращает значение больше 0, если фамилия контакта сравниваемой 
		/// VM предшествует фамилии контакта текущей VM</returns>
		public int CompareTo(ContactViewModel contactViewModel)
		{
			return LastName.CompareTo(contactViewModel.LastName);
		}
	}
}
