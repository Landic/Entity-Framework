using AuthorAndBooks.Models;
using AuthorAndBooks.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AuthorAndBooks.ViewModel
{
    public class EditAuthorViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;
		private string name;
		private ICommand editCommand;
		private ICommand cancelCommand;
		private readonly IWindowService windowService;
		private readonly Author originalAuthor;


		public ICommand EditAuthorCommand => editCommand ??= new RelayCommand(EditAuthor, CanEditAuth);
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

		public EditAuthorViewModel(IWindowService windowService, Author author)
		{
			this.windowService = windowService;
			originalAuthor = author;
			Name = originalAuthor.Name; // присваиваем для того чтобы в текстбоксе отображалось имя того автора которого хотим отредактировать
		}


		private void Cancel() // метод если передумали редактировать
		{
			windowService.CloseWindow(System.Windows.Application.Current.Windows[1]);
		}

		private void EditAuthor() 
		{
			windowService.ShowMessageBool("Вы уверены что хотите изменить автора", result => // вызываем булевский MessageBox и проверяем результат с него
			{
				if (result)
				{
					using (var context = new AuthorAndBooksContext())
					{
						try
						{
							var author = context.Authors.FirstOrDefault(i => i.Id == originalAuthor.Id);
							if (author == null)
							{
								windowService.ShowMessage("Автор не найдена!");
							}
							author.Name = Name;
							context.SaveChanges();
							windowService.CloseWindow(System.Windows.Application.Current.Windows[1]);
						}
						catch (Exception ex)
						{
							windowService.ShowMessage(ex.Message);
							return;
						}
					}
				}
			});
		}

		private bool CanEditAuth()
		{
			using (var context = new AuthorAndBooksContext())
			{
				var author = context.Authors.Select(i => i.Name).ToList();
				return !string.IsNullOrEmpty(Name) && !author.Contains(Name); // проверка не существует ли такого автора уже и не пустой ли текстбокс
			}
		}

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
