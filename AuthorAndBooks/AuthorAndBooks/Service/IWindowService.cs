using AuthorAndBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AuthorAndBooks.Service
{
    public interface IWindowService
    {
        void ShowAddAuthorWindow();

        void ShowEditAuthorWindow(Author SelectedAuthor);

        void ShowAddBookWindow();

		void ShowEditBookWindow(Book SelectedBook);

		void CloseWindow(Window window);

        void ShowMessage(string message);
        void ShowMessageBool(string message, Action<bool> callback);
	}
}
