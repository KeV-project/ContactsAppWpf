using ContactsAppViewModel.Commands;
using ContactsAppViewModel.ModelViewModels;

namespace ContactsAppViewModel.WindowViewModels
{
	/// <summary>
	/// Класс <see cref="EditContactViewModel"/>
	/// связывает модель и представление через механизм привязки данных.
	/// </summary>
	public class EditContactWindowViewModel: ViewModelBase
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
		/// ViewModel редактируемого контакта
		/// </summary>
		public ContactViewModel ContactViewModel { get; private set; }

		/// <summary>
		/// Инициализирует редактируемый контакт
		/// </summary>
		/// <param name="contact"></param>
		public EditContactWindowViewModel(ContactViewModel contactViewModel)
		{
			ContactViewModel = contactViewModel;
		}
    }
}
