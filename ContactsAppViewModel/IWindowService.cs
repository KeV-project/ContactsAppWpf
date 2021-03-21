using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsAppViewModel
{
	/// <summary>
	/// Реализует интерфейс сервиса, предоставляющего свойства 
	/// и методы для работы с окном приложения
	/// </summary>
	public interface IWindowService
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

		/// <summary>
		/// Выполняет запуск окна
		/// </summary>
		/// <param name="dataContext"></param>
		void ShowDialog(object dataContext);
	}
}
