using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ContactsAppModel;

namespace ContactsAppViewModel.ModelViewModels
{
	/// <summary>
	/// Класс <see cref="ProjectViewModel"/>
	/// предназначен для создания viewModel объекта класса
	/// <see cref="ContactsAppModel.Project"/>
	/// </summary>
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
		/// Возвращает false, если проект содержит несохраненные данные,
		/// иначе возвращает trye
		/// </summary>
		public bool IsProjectSaved { get; set; } = false;

		/// <summary>
		/// Хранит список VM контактов пользователя
		/// </summary>
		public ObservableCollection<ContactViewModel>
			ContactViewModels{ get; set; }

		/// <summary>
		/// Позволяет получить или добавить VM контакта в список 
		/// по указанному индуксу
		/// </summary>
		/// <param name="index">Индекс возвращаемой VM контакта
		/// или позиция для добавления VM контакта в список</param>
		/// <returns>Возвращает VM контакта по указанному индексу</returns>
		public ContactViewModel this[int index] => ContactViewModels[index];

		/// <summary>
		/// Хранит VM текущего контакта
		/// </summary>
		private ContactViewModel _selectedContactViewModel;

		/// <summary>
		/// Возвращает и устанавливает VM текущего контакта
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

		/// <summary>
		/// Возвращает и устанавливает строку, содержащую имена и 
		/// фамилии именинников
		/// </summary>
		public string BirthdayNames { get; private set; } = "";

		/// <summary>
		/// Возвращает true, если у кого-то из контактов сегодня день
		/// рождения, иначе возвращает false
		/// </summary>
		public bool HasBirthdays { get; private set; } = false;

		/// <summary>
		/// Инициализирует свойства объекта класса 
		/// <see cref="ProjectViewModel"/>
		/// </summary>
		public ProjectViewModel()
		{
			_project = ProjectManager.ReadProject(_defaultPath);
			ContactViewModels = GetContactViewModels();
			BirthdayNames = GetBirthdayNames();
			OnPropertyChanged(nameof(HasBirthdays));
		}

		/// <summary>
		/// Создает список VM контактов
		/// </summary>
		/// <returns>Возвращает список VM контактов</returns>
		private ObservableCollection<ContactViewModel> 
			GetContactViewModels()
		{
			var contactViewModels = 
				new ObservableCollection<ContactViewModel>();
			for (var i = 0; i < _project.ContactsCount; i++)
			{
				contactViewModels.Add(new ContactViewModel(_project[i]));
			}
			return contactViewModels;
		}

		/// <summary>
		/// Создает список VM контактов, фамилия и имя которых
		/// содержит подстроку
		/// </summary>
		/// <param name="searchString">Подстрока для поиска</param>
		/// <returns>Возвращает список VM контактов, фамилия и имя которых
		/// содержит подстроку</returns>
		private ObservableCollection<ContactViewModel>
			GetSearchContactViewModels(string searchString)
		{
			var contactViewModels = new ObservableCollection<ContactViewModel>();
			foreach (var contactViewModel in ContactViewModels)
            {
                var contactName = contactViewModel.LastName
                                  + " " + contactViewModel.FirstName;
                if(contactName.Contains(searchString))
                {
                    contactViewModels.Add(contactViewModel);
                }
            }
			return contactViewModels;
		}

		/// <summary>
		/// Создает список контактов из списка VM контактов
		/// </summary>
		/// <returns>Возвращает список контактов</returns>
		private List<Contact> GetAllContacts()
        {
            return ContactViewModels.Select(contactViewModel => 
				contactViewModel.Contact).ToList();
        }

		/// <summary>
		/// Создает строку с инициалами контактов, у которых сегодня
		/// день рождения
		/// </summary>
		/// <returns>Возвращает строку с инициалами контактов, 
		/// у которых сегодня день рождения</returns>
		private string GetBirthdayNames()
		{
			var birthdayNames = "";
			foreach (var contactViewModel in ContactViewModels)
            {
                if (contactViewModel.BirthDate.Day != DateTime.Today.Day ||
                    contactViewModel.BirthDate.Month != DateTime.Today.Month) continue;

                birthdayNames += contactViewModel.LastName 
                                 + " " + contactViewModel.FirstName + ", ";
                HasBirthdays = true;
            }
			return birthdayNames.Trim(new char[] { ',', ' ' });
		}

		/// <summary>
		/// Добавляет VM контакта в список
		/// </summary>
		/// <param name="contactViewModel">Новая VM контакта</param>
		public void AddContactViewModel(
			ContactViewModel contactViewModel)
		{
			ContactViewModels.Add(contactViewModel);
			IsProjectSaved = false;
		}

		/// <summary>
		/// Заменят одну VM контакта на другую
		/// </summary>
		/// <param name="currentContactViewModel">Текущая VM контакта</param>
		/// <param name="newContactViewModel">Новая VM контакта</param>
		public void ReplaceContactViewModel(
			ContactViewModel currentContactViewModel, 
			ContactViewModel newContactViewModel)
		{
			ContactViewModels.Remove(currentContactViewModel);
			ContactViewModels.Add(newContactViewModel);
			IsProjectSaved = false;
		}

		/// <summary>
		/// Удаляет текущую VM контакта из списка
		/// </summary>
		public void RemoveSelectedContactViewModel()
		{
			if (SelectedContactViewModel != null)
			{
				ContactViewModels.Remove(SelectedContactViewModel);
				IsProjectSaved = false;
			}
		}

		/// <summary>
		/// Создает список VM контактов, фамилия и имя которых 
		/// содержит подстроку
		/// </summary>
		/// <param name="searchString">Подстрока для поиска</param>
		public void ShowSearchContacts(string searchString)
		{
			if(!IsProjectSaved)
			{
				SaveProject();
			}
			ContactViewModels = searchString == "" 
                ? GetContactViewModels() 
                : GetSearchContactViewModels(searchString);
			OnPropertyChanged(nameof(ContactViewModels));
		}

		/// <summary>
		/// Сохраняет текущую версию проекта
		/// </summary>
		public void SaveProject()
        {
            if (IsProjectSaved) return;

            _project.Contacts = GetAllContacts();
            ProjectManager.SaveProject(_project, _defaultPath);
            IsProjectSaved = true;
        }
	}
}
