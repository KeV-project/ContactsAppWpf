using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsAppModel;

namespace ContactsAppViewModel.ModelViewModels
{
	/// <summary>
	/// /// Класс <see cref="PhoneNumberViewModel"/> организует уровень
	/// защиты данных приложения от неккоректного ввода
	/// </summary>
	public class PhoneNumberViewModel: ModelViewModelBase
	{
		/// <summary>
		/// Хранит изменяемый объект
		/// </summary>
		public PhoneNumber PhoneNumber { get; set; }

		private string _number;

		/// <summary>
		/// Возвращает и устанавливает номер телефона
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
		/// Инифиализирует объект класса <see cref="PhoneNumberViewModel"/>
		/// </summary>
		/// <param name="phoneNumber">Объект класса 
		/// <see cref="PhoneNumber"/></param>
		public PhoneNumberViewModel(PhoneNumber phoneNumber)
		{
			PhoneNumber = phoneNumber;
		}
	}
}
