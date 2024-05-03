using System;
using System.Collections.Generic;

namespace Volkov_HW_Entity_3.Model;

public partial class Book
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int AuthorId { get; set; }

    public virtual Author Author { get; set; } = null!;
}
