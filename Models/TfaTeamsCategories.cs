using System.ComponentModel.DataAnnotations.Schema;

namespace TeamManager.Models
{
    public class TfaTeamsCategories
    {
        [Column("teamID")]
        public int TeamId { get; set; }

        [Column("categoriesID")]
        public int CategoriesId { get; set; }

        public TfaTeam Team { get; set; }
        public TfaCategory Category { get; set; }
    }
}
