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


namespace ContactsAppViewModel
{
    /// <summary>
    /// Класс <see cref="ShowContactsViewModel"/> 
    /// связывает модель и представление через механизм привязки данных. 
    /// </summary>
    public class ShowContactsViewModel: VeiwModelBase
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
        /// Хранит сервис предоставляющий свойста и методы для
        /// работы с дочерним окном
        /// </summary>
        private IWindowService _windowService;

        /// <summary>
        /// Инициализирует проект пользовательских данных и
        /// устанавливает сервис для связи с дочерним окном
        /// </summary>
        /// <param name="editContactWindowService">Сервис дочернего окна</param>
        public ShowContactsViewModel(IWindowService 
            editContactWindowService)
		{
            _path = new FileInfo(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData) +
                "\\ContactsAppWpf\\" + "ContactsAppWpf.notes");
            _project = ProjectManager.ReadProject(_path);
            Contacts = new ObservableCollection<Contact>();
            for(int i = 0; i < _project.GetContactsCount(); i++)
			{
                Contacts.Add(_project[i]);
			}
            _windowService = editContactWindowService;
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
                      _windowService.ShowDialog(viewModel);
                      if(_windowService.DialogResult)
					  {
                          _project.AddContact(viewModel.EditedContact);
                          Contacts.Add(viewModel.EditedContact);
                          ProjectManager.SaveProject(_project, _path);
                      }
                  }));
            }
        }
    }
}
