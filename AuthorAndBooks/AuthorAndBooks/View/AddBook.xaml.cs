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
using System.Windows.Shapes;
using AuthorAndBooks.Service;
using AuthorAndBooks.ViewModel;

namespace AuthorAndBooks.View
{
	/// <summary>
	/// Interaction logic for AddBook.xaml
	/// </summary>
	public partial class AddBook : Window
	{
		public AddBook()
		{
			InitializeComponent();
			DataContext = new AddBookViewModel(new WindowService());
		}
	}
}
