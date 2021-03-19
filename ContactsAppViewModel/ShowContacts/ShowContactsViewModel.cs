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

namespace ContactsAppViewModel.ShowContacts
{
    public class ShowContactsViewModel: INotifyPropertyChanged
    {
        private Project _project;

        private FileInfo _path;

        private Contact _selectedContact;

        public ObservableCollection<Contact> Contacts { get; set; }

        public ShowContactsViewModel()
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

        private RelayCommand _addContact;
        public RelayCommand AddContact
        {
            get
            {
                return _addContact ??
                  (_addContact = new RelayCommand(obj =>
                  {
                      
                  }));
            }
        }
    }
}
