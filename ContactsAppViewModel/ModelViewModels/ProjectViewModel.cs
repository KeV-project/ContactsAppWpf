using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ContactsAppModel;

namespace ContactsAppViewModel.ModelViewModels
{
	public class ProjectViewModel: ModelViewModelBase
	{
		/// <summary>
		/// Хранит проект с пользовательскими данными приложения
		/// </summary>
		private Project _project;

		/// <summary>
		/// Хранит путь к файлу для сериализации пользовательских данных
		/// </summary>
		private readonly FileInfo _defaultPath = new FileInfo(
			Environment.GetFolderPath(
			Environment.SpecialFolder.ApplicationData) +
            "\\ContactsAppWpf\\" + "ContactsAppWpf.notes");

		/// <summary>
		/// Хранит список VM контактов пользователя
		/// </summary>
		public ObservableCollection<ContactViewModel>
			ContactViewModels
		{ get; set; }

		/// <summary>
		/// Хранит VM текущего контакта
		/// </summary>
		private ContactViewModel _selectedContactViewModel;

		/// <summary>
		/// Возвращает и устанавливает текущий контакт
		/// </summary>
		public ContactViewModel SelectedContactViewModel
		{
			get
			{
				return _selectedContactViewModel;
			}
			set
			{
				_selectedContactViewModel = value;
				OnPropertyChanged(nameof(SelectedContactViewModel));
			}
		}

		public ProjectViewModel()
		{
			_project = ProjectManager.ReadProject(_defaultPath);
			ContactViewModels = GetContactViewModels();
		}

		private ObservableCollection<ContactViewModel> 
			GetContactViewModels()
		{
			ObservableCollection<ContactViewModel> contactViewModels = 
				new ObservableCollection<ContactViewModel>();

			for (int i = 0; i < _project.ContactsCount; i++)
			{
				contactViewModels.Add(new ContactViewModel(_project[i]));
			}

			return contactViewModels;
		}

		public void AddContactViewModel(ContactViewModel contactViewModel)
		{
			ContactViewModels.Add(contactViewModel);
		}
	}
}
