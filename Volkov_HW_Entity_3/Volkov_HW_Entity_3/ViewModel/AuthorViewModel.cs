using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volkov_HW_Entity_3.Model;

namespace Volkov_HW_Entity_3.ViewModel
{
    public class AuthorViewModel : BaseViewModel
    {
        private Author author;

        public AuthorViewModel(Author author)
        {
            this.author = author;
        }

        public string Fio
        {
            get
            {
                return author.Fio;
            }
            set
            {
                author.Fio = value;
                OnPropertyChanged(nameof(Fio));
            }
        }
    }
}
