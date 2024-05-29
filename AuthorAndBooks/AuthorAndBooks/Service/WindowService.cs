using AuthorAndBooks.Models;
using AuthorAndBooks.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AuthorAndBooks.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Windows.Forms;

namespace AuthorAndBooks.Service
{
    public class WindowService : IWindowService // сервис для открытие окон (мне он самому не нравится, понимаю что нарушения паттерна но не могу найти информацию)
    {
		public void CloseWindow(Window window) // метод для закрытия окон
		{
			if (window == null)
				throw new ArgumentNullException(nameof(window));

			window.Close();
		}

		public void ShowAddAuthorWindow() // вызов окна для добавления 
        {
            var addAuthorWindow = new AddAuthor();
            addAuthorWindow.ShowDialog();
        }

		public void ShowMessage(string message) // вызов MessageBox
		{
			var messageBoxViewModel = new MessageViewModel(new WindowService())
			{
				Message = message
			};

			var messageBoxWindow = new View.Message
			{
				DataContext = messageBoxViewModel
			};

			messageBoxWindow.ShowDialog();
		}

		public void ShowEditAuthorWindow(Author SelectedAuthor) // вызов окна для редактирования автора
		{
			var editAuthorWindow = new EditAuthor();
			var editAuthorViewModel = new EditAuthorViewModel(new WindowService(), SelectedAuthor); // передаем выбраного автора и новый объект сервиса
			editAuthorWindow.DataContext = editAuthorViewModel;

			editAuthorWindow.ShowDialog(); 
		}

		public void ShowMessageBool(string message, Action<bool> callback) // вызов MessageBox булевский
		{
			var messageBoxViewModel = new MessageBoolViewModel(new WindowService()) // создаем и передаем сообщение которое передали
			{
				Message = message
			};

			messageBoxViewModel.MessageBoxResult += callback; // передаем callback который будет обрабатывать результат

			var messageBoxWindow = new MessageBool
			{
				DataContext = messageBoxViewModel
			};

			messageBoxWindow.ShowDialog();
		}

		public void ShowAddBookWindow()
		{
			var addBookWindow = new AddBook();
			addBookWindow.ShowDialog();
		}

		public void ShowEditBookWindow(Book SelectedBook)
		{
			var editBookWindow = new EditBook();
			var editBookViewModel = new EditBookViewModel(new WindowService(), SelectedBook); // передаем выбраного книгу и новый объект сервиса
			editBookWindow.DataContext = editBookViewModel;
			editBookWindow.ShowDialog();
		}
	}
}
