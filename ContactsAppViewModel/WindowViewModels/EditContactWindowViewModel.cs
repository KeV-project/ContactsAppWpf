using ContactsAppViewModel.Commands;
using ContactsAppViewModel.ModelViewModels;

namespace ContactsAppViewModel.WindowViewModels
{
	/// <summary>
	/// Класс <see cref="EditContactWindowViewModel"/>
	/// связывает модель и представление через механизм привязки данных
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
		/// ViewModel текущего контакта
		/// </summary>
		public ContactViewModel CurrentContactViewModel { get; }

		/// <summary>
		/// ViewModel редактируемого контакта
		/// </summary>
		public ContactViewModel EditedContactViewModel { get; }

		/// <summary>
		/// Инициализирет объект класса 
		/// <see cref="EditContactWindowViewModel"/>
		/// </summary>
		/// <param name="contactViewModel">viewModel контакта</param>
		public EditContactWindowViewModel(ContactViewModel contactViewModel)
		{
			CurrentContactViewModel = contactViewModel;
			EditedContactViewModel = (ContactViewModel)
				contactViewModel.Clone();
		}
    }
}
