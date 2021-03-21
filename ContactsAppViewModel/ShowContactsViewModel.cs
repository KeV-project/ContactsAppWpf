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
    public class ShowContactsViewModel: INotifyPropertyChanged
    {
        private Project _project;

        private FileInfo _path;

        private Contact _selectedContact;

        public ObservableCollection<Contact> Contacts { get; set; }

        private IWindowService _windowService;

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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private RelayCommand _addContactCommand;
        public RelayCommand AddContactCommand
        {
            get
            {
                return _addContactCommand ??
                  (_addContactCommand = new RelayCommand(obj =>
                  {
                      EditContactViewModel viewModel = 
                      new EditContactViewModel();
                      _windowService.ShowDialog(viewModel);
                      if(_windowService.DialogResult)
					  {

					  }
                  }));
            }
        }
    }
}
