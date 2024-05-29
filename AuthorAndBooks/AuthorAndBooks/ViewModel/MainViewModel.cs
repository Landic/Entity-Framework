using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using AuthorAndBooks.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using AuthorAndBooks.Service;

namespace AuthorAndBooks.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
	{
		//private AuthorAndBooksContext dbContext; // решил обойтись от создания объекта так как у меня после редактирование не хотело никак обновляться 
		private Author selectedAuthor;
		private Book selectedBook;
		private readonly IWindowService windowService;

		private ObservableCollection<Author> author;
		private ObservableCollection<Book> books;


		private ICommand addAuthorCommand;
		private ICommand editAuthCommand;
		private ICommand deleteAuthCommand;


		private ICommand addBookCommand;
		private ICommand editBookCommand;
		private ICommand deleteBookCommand;

		private ICommand filterCommand;

		bool filter;

		public ObservableCollection<Author> Author
		{
			get => author;
			set
			{
				author = value;
				OnPropertyChanged(nameof(Author));
			} 
		}

		public ObservableCollection<Book> Books
		{
			get => books;
			set
			{
				books = value;
				OnPropertyChanged(nameof(Books));
			}
		}

		public MainViewModel(IWindowService windowService)
		{
			//dbContext = new AuthorAndBooksContext();
			this.windowService = windowService;
			filter = false;
			LoadAuthors(); // вызов метода для того чтобы с запуска приложения были подгружены авторы
			LoadBooks(); // та же ситуация для книг
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public bool Filter
		{
			get=> filter;
			set
			{
				filter = value;
				OnPropertyChanged(nameof(Filter));
				FilterBooks();
			}
		}

		public Author SelectedAuthor // выбранный автор
		{
			get => selectedAuthor;
			set
			{
				selectedAuthor = value;
				OnPropertyChanged(nameof(SelectedAuthor));
				FilterBooks();
			}
		}

		public Book SelectedBook
		{
			get => selectedBook;
			set
			{
				selectedBook = value;
				OnPropertyChanged(nameof(SelectedBook));
			}
		}

		private void LoadAuthors() // метод для подгрузки авторов
		{
			using (var context = new AuthorAndBooksContext())
			{
				var authors = context.Authors.ToList();
				Author = new ObservableCollection<Author>(authors);
			}
		}

		private void LoadBooks() // метод для подгрузки книг
		{
			using (var context = new AuthorAndBooksContext())
			{
				var books = context.Books.ToList();
				Books = new ObservableCollection<Book>(books);
			}
		}

		private void UpdateAuthor() // метод для обновления комбобокса
		{
			using (var context = new AuthorAndBooksContext())
			{
				var authors = context.Authors.ToList();
				Author = new ObservableCollection<Author>(authors);
			}
		}

		private void UpdateBooks() // метод для обновления листбокса
		{
			using (var context = new AuthorAndBooksContext())
			{
				var books = context.Books.ToList();
				Books = new ObservableCollection<Book>(books);
			}
		}


		// CRUD операции для автора

		private void AddAuthor()
		{
			windowService.ShowAddAuthorWindow();
			UpdateAuthor();
		}


		private void EditAuthor()
		{
			windowService.ShowEditAuthorWindow(SelectedAuthor); // передаем выбраного автора
			UpdateAuthor(); //обновление комбобокса
		}

		private bool CanEditAuth()
		{
			return SelectedAuthor != null; // проверка выбран ли автор
		}

		private void DeleteAuthor() // метод для удаление автора и всех его книг
		{
			windowService.ShowMessageBool($"Вы уверены что хотите удалить автора {SelectedAuthor.Name} и все его книги?", result =>
				{
					if (result)
					{
						using(var context = new AuthorAndBooksContext())
						{
							try
							{
								var author = context.Authors.FirstOrDefault(a => a.Id == SelectedAuthor.Id);
								var books = context.Books.Include(i => i.Author).Where(i => i.Author == author);
								if (author != null)
								{
									if (books != null)
									{
										context.Books.RemoveRange(books);
									}
									context.Authors.Remove(author);
									context.SaveChanges();
									UpdateAuthor();
									UpdateBooks();
								}
							}
							catch(Exception ex)
							{
								windowService.ShowMessage(ex.Message);
							}
						}
					}
				}
			);
		}

		private bool CanDeleteAuthor()
		{
			return SelectedAuthor != null; // проверка выбран ли автор
		}

		public ICommand AddAuthorCommand => addAuthorCommand ??= new RelayCommand(AddAuthor);

		public ICommand EditAuthorCommand => editAuthCommand ??= new RelayCommand(EditAuthor, CanEditAuth);

		public ICommand DeleteAuthorCommand => deleteAuthCommand ??= new RelayCommand(DeleteAuthor, CanDeleteAuthor);


		// CRUD операции для книг

		private void AddBook()
		{
			windowService.ShowAddBookWindow();
			if(filter == false)
			{
				UpdateBooks();
			}
			else
			{
				FilterBooks();
			}			
		}


		private void EditBook()
		{
			windowService.ShowEditBookWindow(SelectedBook); // передаем выбраную книгу
			if (filter == false)
			{
				UpdateBooks();
			}
			else
			{
				FilterBooks();
			}
		}

		private bool CanEditBook()
		{
			return SelectedBook != null;
		}


		private void DeleteBook() // метод для удаление книги
		{
			windowService.ShowMessageBool($"Вы уверены что хотите удалить эту книгу \"{SelectedBook.Name}\"", result =>
			{
				if (result)
				{
					using (var context = new AuthorAndBooksContext())
					{
						try
						{
							var book = context.Books.FirstOrDefault(a => a.Id == SelectedBook.Id);
							if (book != null)
							{
								context.Books.Remove(book);
								context.SaveChanges();
								UpdateBooks();
							}
						}
						catch (Exception ex)
						{
							windowService.ShowMessage(ex.Message);
						}
					}
				}
			}
			);
		}

		private bool CanDeleteBook()
		{
			return SelectedBook != null; // проверка выбрана ли книга
		}

		public ICommand AddBookCommand => addBookCommand ??= new RelayCommand(AddBook);

		public ICommand EditBookCommand => editBookCommand ??= new RelayCommand(EditBook, CanEditBook);

		public ICommand DeleteBookCommand => deleteBookCommand ??= new RelayCommand(DeleteBook, CanDeleteBook);


		// Фильтрация

		private void FilterBooks()
		{
			if (filter == true)
			{
				using (var context = new AuthorAndBooksContext())
				{
					var books = context.Books.Include(i => i.Author).Where(i => i.Author == SelectedAuthor).ToList();
					Books.Clear();
					Books = new ObservableCollection<Book>(books);
				}
			}
			else
			{
				UpdateBooks();
			}
		}

		private bool canFilterBooks()
		{
			return selectedAuthor != null;
		}


		public ICommand FilterCommand => filterCommand ??= new RelayCommand(FilterBooks, canFilterBooks);
	}
}
