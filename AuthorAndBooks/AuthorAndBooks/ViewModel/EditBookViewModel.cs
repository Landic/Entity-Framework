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
    public class EditBookViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;
		private string name;

		private ICommand editCommand;
		private ICommand cancelCommand;

		private readonly IWindowService windowService;

		private readonly Book originalBook;
		private Author selectedAuthor;
		private ObservableCollection<Author> author;

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

		public ICommand EditBookCommand => editCommand ??= new RelayCommand(EditBook, CanEditBook);
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

		public EditBookViewModel(IWindowService windowService, Book book)
		{
			this.windowService = windowService;
			originalBook = book;
			Name = originalBook.Name; // присваиваем для того чтобы в текстбоксе отображалось название той книги которого хотим отредактировать
			using (var context = new AuthorAndBooksContext())
			{
				var author = context.Authors.ToList();
				Author = new ObservableCollection<Author>(author);
			}
			SelectedAuthor = Author.FirstOrDefault(a => a.Id == originalBook.AuthorId);
		}


		private void Cancel() // метод если передумали редактировать
		{
			windowService.CloseWindow(System.Windows.Application.Current.Windows[1]);
		}

		private void EditBook()
		{
			windowService.ShowMessageBool("Вы уверены что хотите изменить книгу?", result => // вызываем булевский MessageBox и проверяем результат с него
			{
				if (result)
				{
					using (var context = new AuthorAndBooksContext())
					{
						try
						{
							var author = context.Authors.FirstOrDefault(i => i.Id == SelectedAuthor.Id);
							if (author == null)
							{
								windowService.ShowMessage("Автор не найдена!");
							}
							var book = context.Books.FirstOrDefault(b => b.Id == originalBook.Id);
							if (book != null)
							{
								book.Name = Name;
								book.Author = author;
								context.SaveChanges();
								windowService.CloseWindow(System.Windows.Application.Current.Windows[1]);
							}
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

		private bool CanEditBook()
		{
			using (var context = new AuthorAndBooksContext())
			{
				var book = context.Books.Select(i => i.Name).ToList();
				return !string.IsNullOrEmpty(Name) && !book.Contains(Name) && SelectedAuthor != null; // проверка не существует ли такой книги уже и не пустой ли текстбокс и выбран ли автор
			}
		}

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
