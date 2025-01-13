using TeamManager.Controllers;
using TeamManager.DTOs;
using TeamManager.Models;

namespace TeamManager.Repositories
{
    public interface ITeamRepository
    {
        Task<IEnumerable<TfaTeam>> GetAllTeamsAsync();
        Task<TfaTeam> GetTeamByIdAsync(int id);
        Task AddTeamAsync(TfaTeam team);
        Task UpdateTeamAsync(TfaTeam team);
        Task DeleteTeamAsync(int id);
        Task<TfaTeam> GetByNameAsync(string name); // Asegúrate de que esté incluido


    }
}
