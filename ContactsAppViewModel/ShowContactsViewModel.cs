using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using ContactsAppModel;
using ContactsAppViewModel.WindowServices;
using ContactsAppViewModel.Commands;


namespace ContactsAppViewModel
{
    /// <summary>
    /// Класс <see cref="ShowContactsViewModel"/> 
    /// связывает модель и представление через механизм привязки данных. 
    /// </summary>
    public class ShowContactsViewModel: ViewModelBase
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
        /// Хранит текущий выбранный контакт
        /// </summary>
        private Contact _selectedContact;

        /// <summary>
        /// Хранит список контактов пользователя
        /// </summary>
        public ObservableCollection<Contact> Contacts { get; set; }

        /// <summary>
        /// Хранит строку с именами именинников
        /// </summary>
        public string BirthdayNames { get; private set; } = "";

        /// <summary>
        /// Хранит сервис предоставляющий свойста и методы для
        /// работы с дочерним окном
        /// </summary>
        private IWindowService _editContactWindowService;

        private IWindowService _aboutWindowService;

        /// <summary>
        /// Инициализирует проект пользовательских данных и
        /// устанавливает сервис для связи с дочерним окном
        /// </summary>
        /// <param name="editContactWindowService">Сервис дочернего окна</param>
        public ShowContactsViewModel(IWindowService 
            editContactWindowService, IWindowService aboutWindowService)
		{
            _path = new FileInfo(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData) +
                "\\ContactsAppWpf\\" + "ContactsAppWpf.notes");
            _project = ProjectManager.ReadProject(_path);
            Contacts = new ObservableCollection<Contact>();
            for(int i = 0; i < _project.GetContactsCount(); i++)
			{
                Contacts.Add(_project[i]);
                if(_project[i].BirthDate.Month == DateTime.Today.Month
                    && _project[i].BirthDate.Day == DateTime.Today.Day)
				{
                    BirthdayNames += _project[i].LastName + " " +
                       _project[i].FirstName + ", ";
				}
			}
            if (BirthdayNames.Length > 2)
            {
                BirthdayNames = BirthdayNames.Remove(
                    BirthdayNames.Length - 2, 2);
            }
            _editContactWindowService = editContactWindowService;
            _aboutWindowService = aboutWindowService;
        }

        /// <summary>
        /// Возвращает и устанавливает текущий контакт
        /// </summary>
        public Contact SelectedContact
        {
            get
            {
                return _selectedContact;
            }
            set
            {
                _selectedContact = value;
                OnPropertyChanged("SelectedContact");
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
                      EditContactViewModel viewModel = 
                        new EditContactViewModel(new Contact());
                      _editContactWindowService.ShowDialog(viewModel);
                      if(_editContactWindowService.DialogResult)
					  {
                          _project.AddContact(viewModel.EditedContact);
                          Contacts.Add(viewModel.EditedContact);
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
                      if(SelectedContact != null)
					  {
                          EditContactViewModel viewModel =
                            new EditContactViewModel(SelectedContact);
                          _editContactWindowService.ShowDialog(viewModel);
                          if (_editContactWindowService.DialogResult)
                          {
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
                      if(SelectedContact != null)
					  {
                          _project.RemoveContact(SelectedContact);
                          Contacts.Remove(SelectedContact);
                          ProjectManager.SaveProject(_project, _path);
                      }
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
                     AboutViewModel viewModel = new AboutViewModel();
                     _aboutWindowService.ShowDialog(viewModel);
                 }));
            }
		}
    }
}
