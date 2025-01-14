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

        /// <summary>
        /// Crea un nuevo equipo.
        /// </summary>
        /// <param name="teamDto">Datos del equipo a crear.</param>
        /// <param name="isAutomatic">Indica si la creación es automática o manual.</param>
        /// <returns>Tarea asincrónica.</returns>
        Task AddTeamAsync(TeamDTO teamDto, bool isAutomatic);

        /// <summary>
        /// Actualiza un equipo existente.
        /// </summary>
        /// <param name="teamDto">Datos del equipo a actualizar.</param>
        /// <returns>Tarea asincrónica.</returns>
        Task UpdateTeamAsync(TeamDTO teamDto);

        /// <summary>
        /// Elimina un equipo por su ID.
        /// </summary>
        /// <param name="id">ID del equipo a eliminar.</param>
        /// <returns>Tarea asincrónica.</returns>
        Task DeleteTeamAsync(int id);

        Task<TeamDTO> GetTeamByNameAsync(string teamName);

        Task AddCollaboratorsToTeamAsync(int teamId, List<int> userIds);
        Task<IEnumerable<TfaUser>> GetCollaboratorsByTeamIdAsync(int teamId);



    }

}
