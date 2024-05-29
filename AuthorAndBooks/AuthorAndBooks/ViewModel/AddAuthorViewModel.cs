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
using System.Windows.Forms;
using System.Windows.Input;

namespace AuthorAndBooks.ViewModel
{
	public class AddAuthorViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;
		private string name;
		private ICommand addCommand;
		private ICommand cancelCommand;
		private readonly IWindowService windowService;

		public ICommand AddCommand => addCommand ??= new RelayCommand(AddAuth, CanAddAuth);
		public ICommand CancelCommand => cancelCommand ??= new RelayCommand(Cancel);

		public string Name
		{
			get => name;
			set
			{
				name = value;
				OnPropertyChanged(nameof(Name));
				CommandManager.InvalidateRequerySuggested();
			}
		}

		public AddAuthorViewModel(IWindowService windowService)
		{
			this.windowService = windowService;
		}


		private void Cancel()
		{
			windowService.CloseWindow(System.Windows.Application.Current.Windows[1]);
		}

		private void AddAuth() // метод для добавление нового автора 
		{
			using (var context = new AuthorAndBooksContext())
			{
				try
				{
					var author = new Author();
					author.Name = name;
					context.Add(author);
					context.SaveChanges();
					windowService.CloseWindow(System.Windows.Application.Current.Windows[1]); // закрытие окна
					windowService.ShowMessage($"Добавлен автор - {name}"); // показываем пользователю сообщение что такой то автор добавлен
				}
				catch (Exception ex)
				{
					windowService.ShowMessage(ex.Message);
					return;
				}
			}

		}

		private bool CanAddAuth()
		{
			using (var context = new AuthorAndBooksContext())
			{
				var author = context.Authors.Select(i => i.Name).ToList(); 
				return !string.IsNullOrEmpty(Name) && !author.Contains(Name); // проверка не пустой для текстбокс и не существует ли такого автора в базе
			}
		}

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
