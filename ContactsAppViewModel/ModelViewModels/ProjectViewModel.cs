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

		public bool IsProjectSaved { get; set; } = false;

		/// <summary>
		/// Хранит список VM контактов пользователя
		/// </summary>
		public ObservableCollection<ContactViewModel>
			ContactViewModels{ get; set; }

		public ContactViewModel this[int index] => ContactViewModels[index];

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

		private ObservableCollection<ContactViewModel>
			GetSearchContactViewModels(string searchString)
		{
			ObservableCollection<ContactViewModel> contactViewModels =
				new ObservableCollection<ContactViewModel>();
			for (int i = 0; i < ContactViewModels.Count; i++)
			{
				string contactName = ContactViewModels[i].LastName
					+ " " + ContactViewModels[i].FirstName;
				if(contactName.Contains(searchString))
				{
					contactViewModels.Add(ContactViewModels[i]);
				}
			}
			return contactViewModels;
		}

			private List<Contact> GetAllContacts()
		{
			List<Contact> contacts = new List<Contact>();
			for(int i = 0; i < ContactViewModels.Count; i++)
			{
				contacts.Add(ContactViewModels[i].Contact);
			}
			return contacts;
		}

		public void AddContactViewModel(
			ContactViewModel contactViewModel)
		{
			ContactViewModels.Add(contactViewModel);
			IsProjectSaved = false;
		}

		public void ReplaceContactViewModel(
			ContactViewModel currentContactViewModel, 
			ContactViewModel newContactViewModel)
		{
			ContactViewModels.Remove(currentContactViewModel);
			ContactViewModels.Add(newContactViewModel);
			IsProjectSaved = false;
		}

		public void RemoveSelectedContactViewModel()
		{
			if (SelectedContactViewModel != null)
			{
				ContactViewModels.Remove(SelectedContactViewModel);
				IsProjectSaved = false;
			}
		}

		public void ShowSearchContacts(string searchString)
		{
			if(!IsProjectSaved)
			{
				SaveProject();
			}
			if(searchString == "")
			{
				ContactViewModels = GetContactViewModels();
			}
			else
			{
				ContactViewModels = GetSearchContactViewModels(searchString);
			}
			OnPropertyChanged(nameof(ContactViewModels));
		}

		public void SaveProject()
		{
			if(!IsProjectSaved)
			{
				_project.Contacts = GetAllContacts();
				ProjectManager.SaveProject(_project, _defaultPath);
				IsProjectSaved = true;
			}
		}
	}
}
