using System;
using System.Windows;
using ContactsAppViewModel.WindowViewModels;
using ContactsAppView.WindowServices;

namespace ContactsAppView
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class ShowContactsWindow : Window
	{
		public ShowContactsWindow()
		{
			DataContext = new ShowContactsWindowViewModel(
				new EditContactWindowService(), 
				new AboutWindowService());

			Closed += ShowContactsWindow_Closed;

			InitializeComponent();
		}

		private void ShowContactsWindow_Closed(object sender, EventArgs e)
		{
			((ShowContactsWindowViewModel)DataContext).
				ProjectViewModel.SaveProject();
		}
	}
}
