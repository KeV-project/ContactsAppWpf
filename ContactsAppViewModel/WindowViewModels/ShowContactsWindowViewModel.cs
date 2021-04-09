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
        public ProjectViewModel ProjectViewModel { get; set; }

        /// <summary>
        /// Хранит сервис предоставляющий свойста и методы для
        /// работы с дочерним окном для редактирования контакта
        /// </summary>
        private IDialogWindowService _editContactWindowService;

        /// <summary>
        /// Хранит сервис предоставляющий свойста и методы для
        /// работы с окном About
        /// </summary>
        private IWindowService _aboutWindowService;

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
            ProjectViewModel = new ProjectViewModel();

            _editContactWindowService = editContactWindowService;
            _aboutWindowService = aboutWindowService;
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

					  if (_editContactWindowService.DialogResult)
					  {
						  ProjectViewModel.AddContactViewModel(
							  editContactViewModel.EditedContactViewModel);
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
					  if (ProjectViewModel.SelectedContactViewModel != null)
					  {
						  EditContactWindowViewModel viewModel =
							new EditContactWindowViewModel(
								ProjectViewModel.SelectedContactViewModel);
						  _editContactWindowService.ShowDialog(viewModel);

						  if (_editContactWindowService.DialogResult)
						  {
							  ProjectViewModel.ReplaceContactViewModel(
								  viewModel.CurrentContactViewModel, 
								  viewModel.EditedContactViewModel);
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
					  ProjectViewModel.RemoveSelectedContactViewModel();
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
				 (_findContactCommand = new RelayCommand(searchString =>
				 {
					 //TODO: Naming +
					 ProjectViewModel.ShowSearchContacts(
						 searchString.ToString());
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
					 ProjectViewModel.SaveProject();
					 Environment.Exit(0);
                 }));
            }
        }
    }
}
