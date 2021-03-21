using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsAppViewModel;

namespace ContactsAppView
{
	public class EditContactWindowService: IWindowService
	{
		private EditContactWindow _editContactWindow;
		public bool DialogResult { get; private set; } = false;

		public RelayCommand OkCommand { get; set; }
		public RelayCommand CancelCommand { get; set; }

		public EditContactWindowService()
		{
			OkCommand = new RelayCommand(obj =>
			{
				DialogResult = true;
				_editContactWindow.Close();
			});

			CancelCommand = new RelayCommand(obj =>
			{
				DialogResult = false;
				_editContactWindow.Close();
			});
		}

		public void ShowDialog(object dataContext)
		{
			_editContactWindow = new EditContactWindow();
			_editContactWindow.ShowDialog();
		}
	}
}
