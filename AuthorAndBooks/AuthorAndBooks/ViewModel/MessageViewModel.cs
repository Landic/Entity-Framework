using AuthorAndBooks.Models;
using AuthorAndBooks.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AuthorAndBooks.ViewModel
{
    public class MessageViewModel : INotifyPropertyChanged
	{
		private string message;
		private ICommand okCommand;
		private readonly IWindowService windowService;


		public string Message
		{
			get => message;
			set
			{
				message = value;
				OnPropertyChanged(nameof(Message));
			}
		}

		public ICommand OkCommand => okCommand ??= new RelayCommand(Ok);

		public MessageViewModel(IWindowService windowService)
		{
			this.windowService = windowService;
		}

		private void Ok()
		{
			windowService.CloseWindow(System.Windows.Application.Current.Windows[1]);
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
