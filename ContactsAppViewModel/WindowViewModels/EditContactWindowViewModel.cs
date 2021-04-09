using ContactsAppViewModel.Commands;
using ContactsAppViewModel.ModelViewModels;

namespace ContactsAppViewModel.WindowViewModels
{
    //TODO: Несоответствие XML
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
		public ContactViewModel CurrentContactViewModel { get; private set; }

		public ContactViewModel EditedContactViewModel { get; private set; }

		
		public EditContactWindowViewModel(ContactViewModel contactViewModel)
		{
			CurrentContactViewModel = contactViewModel;
			EditedContactViewModel = (ContactViewModel)
				contactViewModel.Clone();
		}
    }
}
