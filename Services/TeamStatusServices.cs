using TeamManager.DTOs;
using TeamManager.Models;
using TeamManager.Repositories;

namespace TeamManager.Services
{
    public class TeamStatusServices : ITeamStatusServices
    {
        private readonly ITeamStatusRepository _teamStatusRepository;

        public TeamStatusServices(ITeamStatusRepository teamStatusRepository)
        {
            _teamStatusRepository = teamStatusRepository;
        }

        public async Task<IEnumerable<TeamStatusDTO>> GetAllStatusesAsync()
        {
            var statuses = await _teamStatusRepository.GetAllStatusesAsync();
            return statuses.Select(s => new TeamStatusDTO
            {
                StatusId = s.StatusId,
                StatusName = s.StatusName
            });
        }

        public async Task<TeamStatusDTO> GetStatusByIdAsync(int id)
        {
            var status = await _teamStatusRepository.GetStatusByIdAsync(id);
            if (status == null) return null;

            return new TeamStatusDTO
            {
                StatusId = status.StatusId,
                StatusName = status.StatusName
            };
        }

        public async Task AddStatusAsync(TeamStatusDTO teamStatusDto)
        {
            var status = new TfaTeamstatus
            {
                StatusName = teamStatusDto.StatusName
            };

            await _teamStatusRepository.AddStatusAsync(status);
        }

        public async Task UpdateStatusAsync(TeamStatusDTO teamStatusDto)
        {
            var status = new TfaTeamstatus
            {
                StatusId = teamStatusDto.StatusId,
                StatusName = teamStatusDto.StatusName
            };

            await _teamStatusRepository.UpdateStatusAsync(status);
        }

        public async Task DeleteStatusAsync(int id)
        {
            await _teamStatusRepository.DeleteStatusAsync(id);
        }
    }
}
