using ContactsAppViewModel.Commands;

namespace ContactsAppViewModel.WindowServices
{
	public interface IWindowDialogService: IWindowService
	{
		/// <summary>
		/// Хранит результат завершения работы окна
		/// </summary>
		bool DialogResult { get; }

		/// <summary>
		/// Возвращает и устанавливает команду успешного закрытия окна
		/// </summary>
		RelayCommand OkCommand { get; set; }

		/// <summary>
		/// Возвращает и устанавливает команду закрытия окна
		/// </summary>
		RelayCommand CancelCommand { get; set; }

	}
}
