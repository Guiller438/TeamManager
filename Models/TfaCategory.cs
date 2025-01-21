using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamManager.Models;

public partial class TfaCategory
{
    [Key]
    [Column("categoryID")]
    public int CategoryID { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? CategoryDescription { get; set; }

    public int? CategoryPoints { get; set; }

    public DateOnly? CategoryDeadLine { get; set; }

    public virtual ICollection<TfaTeam> TfaTeams { get; set; } = new List<TfaTeam>();

    public virtual ICollection<TfaTeamsCategories> TeamsCategories { get; set; } = new List<TfaTeamsCategories>();

}
