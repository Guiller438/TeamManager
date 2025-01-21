using TeamManager.DTOs;
using TeamManager.Models;

namespace TeamManager.Services
{
    public interface ITeamServices
    {
        /// <summary>
        /// Obtiene todos los equipos con sus relaciones opcionales.
        /// </summary>
        /// <returns>Lista de equipos con detalles.</returns>
        Task<IEnumerable<TeamDTO>> GetAllTeamsAsync();

        /// <summary>
        /// Obtiene un equipo por su ID.
        /// </summary>
        /// <param name="id">ID del equipo.</param>
        /// <returns>Detalles del equipo o null si no existe.</returns>
        Task<TeamDTO> GetTeamByIdAsync(int id);
        Task<TfaTeam> AddTeamAsync(TeamDTO teamDto, bool isAutomatic);

        Task UpdateTeamAsync(TeamDTO teamDto);
        Task DeleteTeamAsync(int id);

        Task<TeamDTO> GetTeamByNameAsync(string teamName);

        Task AddCollaboratorsToTeamAsync(int teamId, List<int> userIds);
        Task<IEnumerable<TfaUser>> GetCollaboratorsByTeamIdAsync(int teamId);

        Task<IEnumerable<TfaUser>> GetLeadersandAdmins();

        Task<IEnumerable<TfaUser>> GetColaborators();



    }

}
