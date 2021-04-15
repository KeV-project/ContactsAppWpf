using System;
using ContactsAppModel;
using ViewModelLib;

namespace ContactsAppViewModel.ModelViewModels
{
	/// <summary>
	/// Класс <see cref="ContactViewModel"/> 
	/// предназначен для создания viewModel объекта класса
	/// <see cref="ContactsAppModel.Contact"/>
	/// </summary>
	public class ContactViewModel: NotifyDataErrorViewModelBase, ICloneable
	{
		/// <summary>
		/// Изменяемый контакт
		/// </summary>
        public Contact Contact { get; private set; }

        /// <summary>
		/// Устанавливает свойство контакта
		/// </summary>
		/// <param name="property">Свойство контакта</param>
		/// <param name="setProperty">Метод, устанавливайющий 
		/// значение свойства</param>
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

			RaisePropertyChanged(property);
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
		/// Хранит viewModel объекта класса 
		/// <see cref="PhoneNumber"/>
		/// </summary>
		private PhoneNumberViewModel _phoneNumberViewModel;

		/// <summary>
		/// Возвращает и устанавливает viewModel объекта класса 
		/// <see cref="PhoneNumber"/>
		/// </summary>
		public PhoneNumberViewModel PhoneNumberViewModel
		{
			get
			{
				if(_phoneNumberViewModel.IsValid)
				{
					RemoveError(nameof(PhoneNumberViewModel));
				}
				else
				{
					AddError(nameof(PhoneNumberViewModel),
						_phoneNumberViewModel.GetErrors(nameof(
							_phoneNumberViewModel.Number)).ToString());
				}
				return _phoneNumberViewModel;
			}
			set
			{
				_phoneNumberViewModel = value;
			}
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
		/// <param name="contact">Редактируемый контакт</param>
		public ContactViewModel(Contact contact)
		{
			Contact = contact;
			PhoneNumberViewModel = new PhoneNumberViewModel(
				Contact.Number);
		}

		/// <summary>
		/// Создает копию текущей viewModel-и
		/// </summary>
		/// <returns>Копия текущей viewModel-и</returns>
		public object Clone()
		{
			return new ContactViewModel((Contact)Contact.Clone());
		}
	}
}
