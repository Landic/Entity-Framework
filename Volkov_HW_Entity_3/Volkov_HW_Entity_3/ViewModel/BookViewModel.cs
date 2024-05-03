using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volkov_HW_Entity_3.Model;

namespace Volkov_HW_Entity_3.ViewModel
{
    public class BookViewModel : BaseViewModel
    {
        private Book book;
        public BookViewModel(Book book)
        {
            this.book = book;
        }

        public string NameBook
        {
            get { return book.Name; }
            set { book.Name = value; OnPropertyChanged(nameof(NameBook)); }
        }

        public string AuthorBook
        {
            get { return book.Author.Fio; }
        }
    }
}
