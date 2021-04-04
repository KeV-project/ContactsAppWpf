using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsAppModel;
using ContactsAppViewModel.Commands;
using ContactsAppViewModel.ModelViewModels;

namespace ContactsAppViewModel.WindowViewModels
{
	/// <summary>
	/// Класс <see cref="EditContactViewModel"/>
	/// связывает модель и представление через механизм привязки данных.
	/// </summary>
	public class EditContactViewModel: ViewModelBase
	{
		/// <summary>
		/// Команда успешного закрытия окна
		/// </summary>
		public RelayCommand OkCommand { get; set; }

		/// <summary>
		/// Команда закрытия окна
		/// </summary>
		public RelayCommand CancelCommand { get; set; }

		public ContactViewModel ContactViewModel { get; private set; }

		/// <summary>
		/// Инициализирует редактируемый контакт
		/// </summary>
		/// <param name="contact"></param>
		public EditContactViewModel(ContactViewModel contactViewModel)
		{
			ContactViewModel = contactViewModel;
		}
    }
}
