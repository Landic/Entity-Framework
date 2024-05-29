using AuthorAndBooks.Models;
using AuthorAndBooks.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AuthorAndBooks.ViewModel
{
    public class AddBookViewModel : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler? PropertyChanged;
		private string name;

		private ICommand addCommand;
		private ICommand cancelCommand;

		private readonly IWindowService windowService;

		private Author selectedAuthor;
		private ObservableCollection<Author> author;

		public ICommand AddCommand => addCommand ??= new RelayCommand(AddBook, CanAddBook);
		public ICommand CancelCommand => cancelCommand ??= new RelayCommand(Cancel);

		public Author SelectedAuthor // выбранный автор
		{
			get => selectedAuthor;
			set
			{
				selectedAuthor = value;
				OnPropertyChanged(nameof(SelectedAuthor));
			}
		}

		public ObservableCollection<Author> Author
		{
			get => author;
			set
			{
				author = value;
				OnPropertyChanged(nameof(Author));
			}
		}

		public string Name
		{
			get => name;
			set
			{
				name = value;
				OnPropertyChanged(nameof(Name));
			}
		}

		public AddBookViewModel(IWindowService windowService)
		{
			this.windowService = windowService;
			using(var context = new AuthorAndBooksContext())
			{
				var author = context.Authors.ToList();
				Author = new ObservableCollection<Author>(author);
			}
		}


		private void Cancel()
		{
			windowService.CloseWindow(System.Windows.Application.Current.Windows[1]);
		}

		private void AddBook() // метод для добавление новую книгу
		{
			using (var context = new AuthorAndBooksContext())
			{
				try
				{
					var author = context.Authors.FirstOrDefault(i => i.Id == SelectedAuthor.Id);
					var book = new Book();
					book.Name = Name;
					book.Author = author;
					context.Add(book);
					context.SaveChanges();
					windowService.CloseWindow(System.Windows.Application.Current.Windows[1]); // закрытие окна
					windowService.ShowMessage($"Добавлен книга - {Name} к автору {SelectedAuthor.Name}"); // показываем пользователю сообщение что такая та книга добавлена к такому то автору
				}
				catch (Exception ex)
				{
					windowService.ShowMessage(ex.Message);
					return;
				}
			}

		}

		private bool CanAddBook()
		{
			using (var context = new AuthorAndBooksContext())
			{
				var book = context.Books.Select(i => i.Name).ToList();
				return !string.IsNullOrEmpty(Name) && !book.Contains(Name) && SelectedAuthor != null; // проверка не пустой для текстбокс и не существует ли такой книги в базу и выбран ли автор
			}
		}

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
