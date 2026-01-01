using System;
using System.Collections.Generic;

namespace MilitaryApp.ReverseEnggData.Models;

public partial class Military
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int KingId { get; set; }

    public virtual Horse? Horse { get; set; }

    public virtual King King { get; set; } = null!;

    public virtual ICollection<Quote> Quotes { get; set; } = new List<Quote>();

    public virtual ICollection<Battle> Battles { get; set; } = new List<Battle>();
}
