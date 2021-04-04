using System;
using ContactsAppModel;

namespace ContactsAppViewModel.ModelViewModels
{
	public class ContactViewModel: ModelViewModelBase
    {
        public Contact Contact { get; private set; }

		private PhoneNumberViewModel _phoneNumberViewModel;

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
				//TODO: Ниже дублируется проверка
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

        public PhoneNumberViewModel PhoneNumberViewModel 
		{ 
			get
			{
				return _phoneNumberViewModel;
			}
			private set
			{
				_phoneNumberViewModel = value;
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
			PhoneNumberViewModel = new PhoneNumberViewModel(
				Contact.Number);
		}
    }
}
