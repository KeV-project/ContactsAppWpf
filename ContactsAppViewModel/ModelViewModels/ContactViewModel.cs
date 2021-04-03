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
        private Contact _contact;

        public string FirstName
		{
			get
			{
                return _contact.FirstName;
			}
			set
			{
				try
				{
                    _contact.FirstName = value;
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
                return _contact.LastName;
			}
			set
			{
				try
				{
                    _contact.LastName = value;
                    RemoveError(nameof(LastName));
				}
                catch(ArgumentException ex)
				{
                    AddError(nameof(LastName), ex.Message);
				}
			}
		}

        public PhoneNumber Number
		{
			get
			{
                return _contact.Number;
			}
			set
			{
				try
				{
                    _contact.Number = value;
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
                return _contact.Email;
			}
			set
			{
				try
				{
                    _contact.Email = value;
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
                return _contact.BirthDate;
			}
			set
			{
				try
				{
                    _contact.BirthDate = value;
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
            _contact = contact;
		}
    }
}
