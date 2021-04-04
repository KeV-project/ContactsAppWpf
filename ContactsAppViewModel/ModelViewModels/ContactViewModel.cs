using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ContactsAppModel;

namespace ContactsAppViewModel.ModelViewModels
{
	public class ContactViewModel: ModelViewModelBase
    {
        public Contact Contact { get; private set; }

		private void SetProperty(string property, Action setProperty)
		{
			try
			{
				setProperty();
				RemoveError(property);
			}
			catch(ArgumentException ex)
			{
				AddError(property, ex.Message);
			}

			OnPropertyChanged(property);
		}

        public string FirstName
		{
			get
			{
                return Contact.FirstName;
			}
			set
			{
				SetProperty(nameof(FirstName), () =>
				{
					Contact.FirstName = value;
				});
			}
		}

        public string LastName
		{
			get
			{
                return Contact.LastName;
			}
			set
			{
				SetProperty(nameof(LastName), () =>
				{
					Contact.LastName = value;
				});
			}
		}

        public PhoneNumber Number
		{
			get
			{
                return Contact.Number;
			}
			set
			{
				SetProperty(nameof(Number), () =>
				{
					Contact.Number = value;
				});
			}
		}

        public string Email
		{
			get
			{
                return Contact.Email;
			}
			set
			{
				SetProperty(nameof(Email), () =>
				{
					Contact.Email = value;
				});
			}
		}

        public DateTime BirthDate
		{
			get
			{
                return Contact.BirthDate;
			}
			set
			{
				SetProperty(nameof(BirthDate), () =>
				{
					Contact.BirthDate = value;
				});
			}
		}

        public ContactViewModel(Contact contact)
		{
			Contact = contact;
		}
    }
}
