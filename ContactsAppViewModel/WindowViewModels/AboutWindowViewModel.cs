using System.Diagnostics;
using ContactsAppViewModel.Commands;

namespace ContactsAppViewModel.WindowViewModels
{
	/// <summary>
	/// Класс <see cref="AboutWindowViewModel"/> связывает модель и 
	/// представление через механизм привязки данных
	/// </summary>
	public class AboutWindowViewModel: ViewModelBase
	{
		/// <summary>
		/// Возвращает имя приложения
		/// </summary>
		public string AppName => "ContactsApp";

        /// <summary>
		/// Возвращает версию приложения
		/// </summary>
		public string Version => "2.0.0";

        /// <summary>
		/// Возвращает имя разработчика приложения
		/// </summary>
		public string Author => "Ekaterina Kabanova";

        /// <summary>
		/// Возвращает адрес электронной почты разработчика
		/// </summary>
		public string Email => "katovskaya009@gmail.com";

        /// <summary>
		/// Возвращает сведения об авторских правах
		/// </summary>
		public string Copyright => "2020 Ekaterina Kabanova ©";

        /// <summary>
		/// Хранит команду открытия репозитория на Git Hub
		/// </summary>
		private RelayCommand _openRepositoryCommand;

		/// <summary>
		/// Возвращает команду открытия репозитория на Git Hub
		/// </summary>
		public RelayCommand OpenRepositoryCommand
		{
			get
			{
				return _openRepositoryCommand ??
				 (_openRepositoryCommand = new RelayCommand(obj =>
				 {
					 Process.Start(
						 "https://github.com/KeV-project/ContactsAppWpf");
				 }));
			}
		}
	}
}
