﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
