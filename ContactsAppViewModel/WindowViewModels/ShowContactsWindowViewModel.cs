using System;
using System.IO;
using System.Collections.ObjectModel;
using ContactsAppModel;
using ContactsAppViewModel.WindowServices;
using ContactsAppViewModel.Commands;
using ContactsAppViewModel.ModelViewModels;


namespace ContactsAppViewModel.WindowViewModels
{
    /// <summary>
    /// Класс <see cref="ShowContactsWindowViewModel"/> 
    /// связывает модель и представление через механизм привязки данных. 
    /// </summary>
    public class ShowContactsWindowViewModel: ViewModelBase
    {
        /// <summary>
        /// Хранит проект с пользовательскими данными приложения
        /// </summary>
        private Project _project;

        /// <summary>
        /// Хранит путь к файлу с пользовательскими данными приложения
        /// </summary>
        private FileInfo _path;

        /// <summary>
        /// Хранит VM текущего выбранного контакта
        /// </summary>
        private ContactViewModel _selectedContactViewModel;

        /// <summary>
        /// Хранит список VM контактов пользователя
        /// </summary>
        public ObservableCollection<ContactViewModel> 
            ContactViewModels{ get; set; }

        /// <summary>
        /// Возвращает и устанавливет строку с именами именинников
        /// </summary>
        public string BirthdayNames { get; private set; } = "";

        /// <summary>
        /// Возвращает и устанавливет строку для поиска контакта
        /// </summary>
        public string SoughtContactName { get; private set; }

        /// <summary>
        /// Хранит сервис предоставляющий свойста и методы для
        /// работы с дочерним окном для редактирования контакта
        /// </summary>
        private IDialogWindowService _editContactWindowService;

        //TODO: XML комментарии? +
        /// <summary>
        /// Хранит сервис предоставляющий свойста и методы для
        /// работы с окном About
        /// </summary>
        private IWindowService _aboutWindowService;

        //TODO: XML комментарии стоят не для всех аргументов
        /// <summary>
        /// Инициализирует проект пользовательских данных и
        /// устанавливает сервисы для связи с дочерним окном
        /// </summary>
        /// <param name="editContactWindowService">
        /// Сервис для взаимодействия с окном редактирования контакта</param>
        /// /// <param name="aboutWindowService">
        /// Сервис окна About</param>
        public ShowContactsWindowViewModel(IDialogWindowService 
            editContactWindowService, IWindowService aboutWindowService)
		{
            _path = new FileInfo(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData) +
                "\\ContactsAppWpf\\" + "ContactsAppWpf.notes");
            _project = ProjectManager.ReadProject(_path);

            ContactViewModels = GetAllContactViewModels();

            _editContactWindowService = editContactWindowService;
            _aboutWindowService = aboutWindowService;
        }

        /// <summary>
        /// Возвращает список VM всех контактов
        /// </summary>
        /// <returns>Список VM всех контактов</returns>
        private ObservableCollection<ContactViewModel> 
            GetAllContactViewModels()
		{
            ObservableCollection<ContactViewModel> contactViewModels =
                new ObservableCollection<ContactViewModel>();
            for (int i = 0; i < _project.ContactsCount; i++)
            {
                contactViewModels.Add(new ContactViewModel(_project[i]));
            }
            return contactViewModels;
        }

        /// <summary>
        /// Возвращает список VM контактов, подходящих под
        /// поисковый запрос
        /// </summary>
        /// <returns>Возвращает список VM контактов, подходящих под
        /// поисковый запрос</returns>
        private ObservableCollection<ContactViewModel>
            GetSoughtContactViewModels()
		{
            ObservableCollection<ContactViewModel> soughtContactViewModels =
               new ObservableCollection<ContactViewModel>();
            for (int i = 0; i < _project.ContactsCount; i++)
            {
                string currentContactName = _project[i].LastName
                    + " " + _project[i].FirstName;
                if(currentContactName.Contains(SoughtContactName))
				{
                    soughtContactViewModels.Add(new ContactViewModel(
                        _project[i]));
                }
            }
            return soughtContactViewModels;
        }

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
                //TODO: nameof +
                OnPropertyChanged(nameof(SelectedContactViewModel));
            }
        }

        /// <summary>
        /// Хранит команду добавления нового контакта
        /// </summary>
        private RelayCommand _addContactCommand;

        /// <summary>
        /// Возвращает команду добавления нового контакта
        /// </summary>
        public RelayCommand AddContactCommand
        {
            get
            {
                return _addContactCommand ??
                  (_addContactCommand = new RelayCommand(obj =>
                  {
                      EditContactWindowViewModel editContactViewModel = 
                        new EditContactWindowViewModel(new ContactViewModel(
                            new Contact()));

                      _editContactWindowService.ShowDialog(
                          editContactViewModel);

                      if(_editContactWindowService.DialogResult)
					  {
                          _project.AddContact(editContactViewModel.
                              ContactViewModel.Contact);
                          ContactViewModels = GetAllContactViewModels();
                          OnPropertyChanged(nameof(ContactViewModels));
                          ProjectManager.SaveProject(_project, _path);
                      }
                  }));
            }
        }

        /// <summary>
        /// Хранит команду редактирования контакта
        /// </summary>
        private RelayCommand _editContactCommand;

        /// <summary>
        /// Возвращает команду редактирования нового контакта
        /// </summary>
        public RelayCommand EditContactCommand
        {
            get
            {
                return _editContactCommand ??
                  (_editContactCommand = new RelayCommand(obj =>
                  {
                      if(SelectedContactViewModel != null)
					  {
                          EditContactWindowViewModel viewModel =
                            new EditContactWindowViewModel(
                                SelectedContactViewModel);
                          _editContactWindowService.ShowDialog(viewModel);

                          if (_editContactWindowService.DialogResult)
                          {
                              _project.SortContacts();
                              ContactViewModels = GetAllContactViewModels();
                              OnPropertyChanged(nameof(ContactViewModels));
                              ProjectManager.SaveProject(_project, _path);
                          }
                      }
                  }));
            }
        }

        /// <summary>
        /// Хранит команду удаления контакта
        /// </summary>
        private RelayCommand _removeContactCommand;

        /// <summary>
        /// Возвращает команду удаления нового контакта
        /// </summary>
        public RelayCommand RemoveContactCommand
        {
            get
            {
                return _removeContactCommand ??
                  (_removeContactCommand = new RelayCommand(obj =>
                  {
                      if(SelectedContactViewModel != null)
					  {
                          _project.RemoveContact(SelectedContactViewModel.
                              Contact);
                          ContactViewModels.Remove(SelectedContactViewModel);
                          ProjectManager.SaveProject(_project, _path);
                      }
                  }));
            }
        }

        /// <summary>
        /// Хранит команду для поиска контакта по подстроке
        /// </summary>
        private RelayCommand _findContactCommand;

        /// <summary>
        /// Возвращает команду для поиска контакта
        /// </summary>
        public RelayCommand FindContactCommand
        {
            get
            {
                return _findContactCommand ??
                 (_findContactCommand = new RelayCommand(soughtContactName =>
                 {
                     SoughtContactName = soughtContactName.ToString();
                     if(SoughtContactName != "")
					 {
                         ContactViewModels = GetSoughtContactViewModels();
					 }
					 else
					 {
                         ContactViewModels = GetAllContactViewModels();
					 }
                     OnPropertyChanged(nameof(ContactViewModels));
                 }));
            }
        }

        /// <summary>
        /// Хранит команду запуска окна About
        /// </summary>
        private RelayCommand _aboutCommand;

        /// <summary>
        /// Возвращает команду запуска окна About
        /// </summary>
        public RelayCommand AboutCommand
		{
			get
			{
                return _aboutCommand ??
                 (_aboutCommand = new RelayCommand(obj =>
                 {
                     AboutWindowViewModel viewModel = 
                        new AboutWindowViewModel();
                     _aboutWindowService.ShowDialog(viewModel);
                 }));
            }
		}

        /// <summary>
        /// Храни команду выхода из приложения
        /// </summary>
        private RelayCommand _closeApplicationCommand;

        /// <summary>
        /// Возвращает команду выхода из приложения
        /// </summary>
        public RelayCommand CloseApplicationCommand
		{
            get
            {
                return _closeApplicationCommand ??
                 (_closeApplicationCommand = new RelayCommand(obj =>
                 {
                     Environment.Exit(0);
                 }));
            }
        }
    }
}
