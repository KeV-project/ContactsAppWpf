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

		/// <summary>
		/// Возвращает и устанавливает номер телефона
		/// </summary>
		public long Number
		{
			get
			{
				return PhoneNumber.Number;
			}
			set
			{
				try
				{
					PhoneNumber.Number = value;
					RemoveError(nameof(Number));
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
