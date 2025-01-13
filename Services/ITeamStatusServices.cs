using TeamManager.DTOs;

namespace TeamManager.Services
{
    public interface ITeamStatusServices
    {
        Task<IEnumerable<TeamStatusDTO>> GetAllStatusesAsync();
        Task<TeamStatusDTO> GetStatusByIdAsync(int id);
        Task AddStatusAsync(TeamStatusDTO teamStatusDto);
        Task UpdateStatusAsync(TeamStatusDTO teamStatusDto);
        Task DeleteStatusAsync(int id);
    }
}
