using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsAppModel;

namespace ContactsAppViewModel
{
	/// <summary>
	/// Класс <see cref="EditContactViewModel"/>
	/// связывает модель и представление через механизм привязки данных.
	/// </summary>
	public class EditContactViewModel: VeiwModelBase
	{
		public RelayCommand OkCommand { get; set; }

		public RelayCommand CancelCommand { get; set; }
		public Contact EditedContact { get; private set; }

		public EditContactViewModel(Contact contact)
		{
			EditedContact = contact;
		}
    }
}
