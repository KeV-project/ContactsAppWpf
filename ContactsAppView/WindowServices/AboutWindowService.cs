using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsAppViewModel.WindowServices;
using ContactsAppViewModel.Commands;

namespace ContactsAppView.WindowServices
{
	public class AboutWindowService: IWindowService
	{
        private AboutWindow _aboutWindow;

        public bool DialogResult { get; }

        public RelayCommand OkCommand { get; set; }

        public RelayCommand CancelCommand { get; set; }

        public void ShowDialog(object dataContext)
        {
            _aboutWindow = new AboutWindow();
            _aboutWindow.DataContext = dataContext;
            _aboutWindow.ShowDialog();
        }
    }
}
