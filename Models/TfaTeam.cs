﻿using System;
using System.Collections.Generic;

namespace TeamManager.Models;

public partial class TfaTeam
{
    public int TeamId { get; set; }

    public string TeamName { get; set; } = null!;

    public string TeamDescription { get; set; } = null!;

    public int TeamStatusId { get; set; }

    public int? TeamLeadId { get; set; }

    public int? CategoriesId { get; set; }

    public virtual TfaCategory Categories { get; set; } = null!;

    public virtual TfaUser? TeamLead { get; set; }

    public virtual TfaTeamstatus TeamStatus { get; set; } = null!;

    public virtual ICollection<TfaTeamCollaborators> TeamCollaborators { get; set; } = new List<TfaTeamCollaborators>();

    public virtual ICollection<TfaTeamsCategories> TeamsCategories { get; set; } = new List<TfaTeamsCategories>();


}
