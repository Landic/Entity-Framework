using System;
using System.Collections.Generic;

namespace Volkov_HW_Entity_3.Model;

public partial class Author
{
    public int Id { get; set; }

    public string Fio { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
