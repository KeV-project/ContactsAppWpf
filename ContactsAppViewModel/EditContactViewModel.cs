using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsAppModel;
using ContactsAppViewModel.Commands;

namespace ContactsAppViewModel
{
	/// <summary>
	/// Класс <see cref="EditContactViewModel"/>
	/// связывает модель и представление через механизм привязки данных.
	/// </summary>
	public class EditContactViewModel: VeiwModelBase
	{
		/// <summary>
		/// Команда успешного закрытия окна
		/// </summary>
		public RelayCommand OkCommand { get; set; }

		/// <summary>
		/// Команда закрытия окна
		/// </summary>
		public RelayCommand CancelCommand { get; set; }

		/// <summary>
		/// Редактируемый контакт
		/// </summary>
		public Contact EditedContact { get; private set; }

		/// <summary>
		/// Инициализирует редактируемый контакт
		/// </summary>
		/// <param name="contact"></param>
		public EditContactViewModel(Contact contact)
		{
			EditedContact = contact;
		}
    }
}
