using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsAppViewModel
{
	public interface IWindowService
	{
		bool DialogResult { get; }
		RelayCommand OkCommand { get; set; }
		RelayCommand CancelCommand { get; set; }
		void ShowDialog(object dataContext);
	}
}
