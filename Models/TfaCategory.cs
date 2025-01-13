using System;
using System.Collections.Generic;

namespace TeamManager.Models;

public partial class TfaCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? CategoryDescription { get; set; }

    public int? CategoryPoints { get; set; }

    public DateOnly? CategoryDeadLine { get; set; }

    public virtual ICollection<TfaTeam> TfaTeams { get; set; } = new List<TfaTeam>();
}
