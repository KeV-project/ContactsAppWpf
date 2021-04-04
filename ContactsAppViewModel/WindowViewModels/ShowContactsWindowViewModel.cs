﻿using System;
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
using ContactsAppViewModel.ModelViewModels;


namespace ContactsAppViewModel.WindowViewModels
{
    /// <summary>
    /// Класс <see cref="ShowContactsViewModel"/> 
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
        /// Хранит текущий выбранный контакт
        /// </summary>
        private ContactViewModel _selectedContactViewModel;

        /// <summary>
        /// Хранит список контактов пользователя
        /// </summary>
        public ObservableCollection<ContactViewModel> 
            ContactViewModels{ get; set; }

        /// <summary>
        /// Хранит строку с именами именинников
        /// </summary>
        public string BirthdayNames { get; private set; } = "";

        /// <summary>
        /// Хранит сервис предоставляющий свойста и методы для
        /// работы с дочерним окном
        /// </summary>
        private IWindowDialogService _editContactWindowService;

        //TODO: XML комментарии?
        private IWindowService _aboutWindowService;

        //TODO: XML комментарии стоят не для всех аргументов
        /// <summary>
        /// Инициализирует проект пользовательских данных и
        /// устанавливает сервис для связи с дочерним окном
        /// </summary>
        /// <param name="editContactWindowService">Сервис дочернего окна</param>
        public ShowContactsWindowViewModel(IWindowDialogService 
            editContactWindowService, IWindowService aboutWindowService)
		{
            _path = new FileInfo(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData) +
                "\\ContactsAppWpf\\" + "ContactsAppWpf.notes");
            _project = ProjectManager.ReadProject(_path);

            ContactViewModels = new ObservableCollection<ContactViewModel>();

            AddContacts();

            _editContactWindowService = editContactWindowService;
            _aboutWindowService = aboutWindowService;
        }

        private void AddContacts()
        {
            for (int i = 0; i < _project.ContactsCount; i++)
            {
                ContactViewModels.Add(new ContactViewModel(_project[i]));
                if (_project[i].BirthDate.Month == DateTime.Today.Month
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
                          ContactViewModels.Add(editContactViewModel.
                              ContactViewModel);
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
    }
}
