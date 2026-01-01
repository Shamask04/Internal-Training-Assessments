using System;
using System.Collections.Generic;

namespace MilitaryApp.ReverseEnggData.Models;

public partial class Battle
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual ICollection<Military> Militaries { get; set; } = new List<Military>();
}
