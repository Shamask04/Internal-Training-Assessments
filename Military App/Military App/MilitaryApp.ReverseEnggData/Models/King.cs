using System;
using System.Collections.Generic;

namespace MilitaryApp.ReverseEnggData.Models;

public partial class King
{
    public int Id { get; set; }

    public string KingName { get; set; } = null!;

    public virtual ICollection<Military> Militaries { get; set; } = new List<Military>();
}
