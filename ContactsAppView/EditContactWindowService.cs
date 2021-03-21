using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsAppViewModel;

namespace ContactsAppView
{
	/// <summary>
	/// Класс <see cref="EditContactWindowService"/> 
	/// предоставляет viewModel свойства и методы для 
	/// работы с окном <see cref="EditContactWindow"/>
	/// </summary>
	public class EditContactWindowService: IWindowService
	{
		/// <summary>
		/// Хранит объект класса Window
		/// </summary>
		private EditContactWindow _editContactWindow;

		/// <summary>
		/// Возвращает результат завершения работы окна
		/// </summary>
		public bool DialogResult { get; private set; } = false;

		/// <summary>
		/// Хранит команду успешного закрытия окна
		/// </summary>
		public RelayCommand OkCommand { get; set; }

		/// <summary>
		/// Хранит команду закрытия окна
		/// </summary>
		public RelayCommand CancelCommand { get; set; }

		/// <summary>
		/// Выполняет реализацию команд
		/// </summary>
		public EditContactWindowService()
		{
			OkCommand = new RelayCommand(obj =>
			{
				DialogResult = true;
				_editContactWindow.Close();
			});

			CancelCommand = new RelayCommand(obj =>
			{
				DialogResult = false;
				_editContactWindow.Close();
			});
		}

		/// <summary>
		/// Запускает окно
		/// </summary>
		/// <param name="dataContext">Редактируемый контакт</param>
		public void ShowDialog(object dataContext)
		{
			_editContactWindow = new EditContactWindow();
			_editContactWindow.ShowDialog();
		}
	}
}
