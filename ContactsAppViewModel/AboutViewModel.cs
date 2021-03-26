using System.Diagnostics;
using ContactsAppViewModel.Commands;

namespace ContactsAppViewModel
{
	public class AboutViewModel: ViewModelBase
	{
		public string AppName
		{
			get
			{
				return "ContactsApp";
			}
		}

		public string Version
		{
			get
			{
				return "2.0.0";
			}
		}

		public string Author
		{
			get
			{
				return "Ekaterina Kabanova";
			}
		}

		public string Email
		{
			get
			{
				return "katovskaya009@gmail.com";
			}
		}

		public string Copyright
		{
			get
			{
				return "2020 Ekaterina Kabanova ©";
			}
		}

		private RelayCommand _openRepositoryCommand;

		public RelayCommand OpenRepositoryCommand
		{
			get
			{
				return _openRepositoryCommand ??
				 (_openRepositoryCommand = new RelayCommand(obj =>
				 {
					 Process.Start("https://github.com/KeV-project/ContactsAppWpf");
				 }));
			}
		}
	}
}
