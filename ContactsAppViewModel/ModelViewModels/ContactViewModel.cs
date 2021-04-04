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

        public string FirstName
		{
			get
			{
                return Contact.FirstName;
			}
			set
			{
				try
				{
					Contact.FirstName = value;
                    RemoveError(nameof(FirstName));
				}
                catch(ArgumentException ex)
				{
                    AddError(nameof(FirstName), ex.Message);
				}
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
				try
				{
					Contact.LastName = value;
                    RemoveError(nameof(LastName));
				}
                catch(ArgumentException ex)
				{
                    AddError("LastName", ex.Message);
				}
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
				try
				{
					Contact.Number = value;
                    RemoveError(nameof(Number));
				}
                catch(ArgumentException ex)
				{
                    AddError(nameof(Number), ex.Message);
				}
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
				try
				{
					Contact.Email = value;
                    RemoveError(nameof(Email));
				}
                catch(ArgumentException ex)
				{
                    AddError(nameof(Email), ex.Message);
				}
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
				try
				{
					Contact.BirthDate = value;
                    RemoveError(nameof(BirthDate));
				}
                catch(ArgumentException ex)
				{
                    AddError(nameof(BirthDate), ex.Message);
				}
			}
		}

        public ContactViewModel(Contact contact)
		{
			Contact = contact;
		}
    }
}
