using TeamManager.Models;

namespace TeamManager.Repositories
{
    public interface ITeamStatusRepository
    {
        Task<IEnumerable<TfaTeamstatus>> GetAllStatusesAsync();
        Task<TfaTeamstatus> GetStatusByIdAsync(int id);
        Task AddStatusAsync(TfaTeamstatus teamStatus);
        Task UpdateStatusAsync(TfaTeamstatus teamStatus);
        Task DeleteStatusAsync(int id);
    }
}
