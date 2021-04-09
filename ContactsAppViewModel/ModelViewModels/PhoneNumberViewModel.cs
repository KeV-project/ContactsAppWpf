using System;
using ContactsAppModel;

namespace ContactsAppViewModel.ModelViewModels
{
	/// <summary>
	/// Класс <see cref="PhoneNumberViewModel"/> 
	/// предназначен для создания viewModel класса 
	/// <see cref="ContactsAppModel.PhoneNumber"/>
	/// </summary>
	public class PhoneNumberViewModel: ModelViewModelBase
	{
		/// <summary>
		/// Хранит изменяемый объект
		/// </summary>
		public PhoneNumber PhoneNumber { get; set; }

		/// <summary>
		/// Хранит представление свойста <see cref="PhoneNumber.Number"/>
		/// </summary>
		private string _number;

		/// <summary>
		/// Возвращает и устанавливает представление 
		/// номера телефона контакта
		/// </summary>
		public string Number
		{
			get
			{
				if(PhoneNumber.Number == 70000000000 &&
					(_number == null || _number == "70000000000"))
				{
					return "";
				}
				return _number;
			}
			set
			{
				try
				{
					_number = value.Trim();
					if(_number == "")
					{
						_number = "70000000000";
					}
					if (long.TryParse(_number, out long number))
					{
						PhoneNumber.Number = number;
						RemoveError(nameof(Number));
					}
					else
					{
						throw new ArgumentException("Номер телефона может " 
							+ "содержать только цифры");
					}
				}
				catch(ArgumentException ex)
				{
					AddError(nameof(Number), ex.Message);
				}

				OnPropertyChanged(nameof(Number));
			}
		}

		/// <summary>
		/// Инициализирует объект класса <see cref="PhoneNumberViewModel"/>
		/// </summary>
		/// <param name="phoneNumber">Объект класса 
		/// <see cref="ContactsAppModel.PhoneNumber"/></param>
		public PhoneNumberViewModel(PhoneNumber phoneNumber)
		{
			PhoneNumber = phoneNumber;
		}
	}
}
