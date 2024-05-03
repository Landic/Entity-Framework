using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Volkov_HW_Entity_3.Model;

namespace Volkov_HW_Entity_3.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<AuthorViewModel> authorsList { get; set; }
        public ObservableCollection<BookViewModel> booksList { get; set; }

        public MainViewModel(IQueryable<Author> authors, IQueryable<Book> books)
        {
            authorsList = new ObservableCollection<AuthorViewModel>(authors.Select(i => new AuthorViewModel(i)));
            booksList = new ObservableCollection<BookViewModel>(books.Select(i => new BookViewModel(i)));
        }
    }
}
