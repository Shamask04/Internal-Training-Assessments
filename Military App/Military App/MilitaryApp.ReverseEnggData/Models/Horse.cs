using System;
using System.Collections.Generic;

namespace MilitaryApp.ReverseEnggData.Models;

public partial class Horse
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int MilitaryId { get; set; }

    public virtual Military Military { get; set; } = null!;
}
