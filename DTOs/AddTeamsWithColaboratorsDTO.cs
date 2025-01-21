using TeamManager.Models;

namespace TeamManager.DTOs
{
    public class AddTeamsWithColaboratorsDTO
    {
        public TeamDTO Team { get; set; }

        public List<int> UserIds { get; set; }
    }
}
