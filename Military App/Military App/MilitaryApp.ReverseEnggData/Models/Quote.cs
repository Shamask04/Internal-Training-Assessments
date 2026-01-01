using System;
using System.Collections.Generic;

namespace MilitaryApp.ReverseEnggData.Models;

public partial class Quote
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;

    public int MilitaryId { get; set; }

    public virtual Military Military { get; set; } = null!;
}
