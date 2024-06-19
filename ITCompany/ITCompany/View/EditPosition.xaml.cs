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
using ITCompany.Service;
using ITCompany.ViewModel;

namespace ITCompany.View
{
    /// <summary>
    /// Interaction logic for AddAuthor.xaml
    /// </summary>
    public partial class EditPosition : Window
    {
        public EditPosition()
        {
			InitializeComponent();
            DataContext = new EditPositionViewModel(new WindowService());
        }
    }
}
