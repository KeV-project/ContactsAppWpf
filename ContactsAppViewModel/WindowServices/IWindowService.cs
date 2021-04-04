using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactsAppViewModel.WindowServices
{
	/// <summary>
	/// Реализует интерфейс сервиса, предоставляющего свойства 
	/// и методы для работы с окном приложения
	/// </summary>
	public interface IWindowService
	{
		/// <summary>
		/// Выполняет запуск окна
		/// </summary>
		/// <param name="dataContext"></param>
		void ShowDialog(object dataContext);
	}
}
