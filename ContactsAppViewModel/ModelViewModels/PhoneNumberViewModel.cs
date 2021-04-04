using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsAppModel;

namespace ContactsAppViewModel.ModelViewModels
{
	public class PhoneNumberViewModel: ModelViewModelBase
	{
		public PhoneNumber PhoneNumber { get; set; }

		public long Number
		{
			get
			{
				return PhoneNumber.Number;
			}
			set
			{
				try
				{
					PhoneNumber.Number = value;
					RemoveError(nameof(Number));
				}
				catch(ArgumentException ex)
				{
					AddError(nameof(Number), ex.Message);
				}

				OnPropertyChanged(nameof(Number));
			}
		}

		public PhoneNumberViewModel(PhoneNumber phoneNumber)
		{
			PhoneNumber = phoneNumber;
		}
	}
}
