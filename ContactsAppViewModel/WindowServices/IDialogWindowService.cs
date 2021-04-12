using GalaSoft.MvvmLight.Command;

namespace ContactsAppViewModel.WindowServices
{
	/// <summary>
	/// Реализует интерфейс сервиса, предоставляющего свойства 
	/// и методы для работы с диалоговым окном приложения
	/// </summary>
	public interface IDialogWindowService: IWindowService
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
