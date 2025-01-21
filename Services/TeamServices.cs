using Microsoft.EntityFrameworkCore;
using TeamManager.Data;
using TeamManager.DTOs;
using TeamManager.Models;
using TeamManager.Repositories;

namespace TeamManager.Services
{
    public class TeamServices : ITeamServices   
    {
        private readonly ITeamRepository _teamRepository;
        private readonly DbAb0bdeTalentseedsContext _context;

        public TeamServices(ITeamRepository teamRepository, DbAb0bdeTalentseedsContext context)
        {
            _context = context;
            _teamRepository = teamRepository;
        }

        public async Task<IEnumerable<TeamDTO>> GetAllTeamsAsync()
        {
            // Obtener equipos incluyendo las relaciones necesarias
            var teams = await _teamRepository.GetAllTeamsAsync();

            // Mapear los equipos al DTO incluyendo los nombres relacionados
            return teams.Select(t => new TeamDTO
            {
                TeamId = t.TeamId,
                TeamName = t.TeamName,
                TeamDescription = t.TeamDescription,
                TeamStatusId = t.TeamStatusId,  
                TeamLeadId = t.TeamLeadId,
                TeamStatusName = t.TeamStatus?.StatusName ?? "Estado desconocido", // Manejo de nulos
                TeamLeadName = t.TeamLead?.UserName ?? "Líder desconocido",        // Manejo de nulos
                Category = t.Categories?.CategoryName ?? "Categoría desconocida", // Manejo de nulos
                CategoriesId = t.CategoriesId
            });

        }


        public async Task<TeamDTO> GetTeamByIdAsync(int id)
        {
            var team = await _teamRepository.GetTeamByIdAsync(id);

            if (team == null) return null;

            return new TeamDTO
            {
                TeamId = team.TeamId,
                TeamName = team.TeamName,
                TeamDescription = team.TeamDescription,
                TeamStatusId = team.TeamStatusId,
                TeamLeadId = team.TeamLeadId,
                CategoriesId = team.CategoriesId,
                TeamStatusName = team.TeamStatus?.StatusName ?? "Estado desconocido",
                TeamLeadName = team.TeamLead?.UserName ?? "Líder desconocido",
                Category = team.Categories?.CategoryName ?? "Categoría desconocida"

            };
        }
        public async Task<TfaTeam> AddTeamAsync(TeamDTO teamDto, bool isAutomatic)
        {
            // Validar líder obligatorio
            if (teamDto.TeamLeadId == null || teamDto.TeamLeadId <= 0)
            {
                throw new ArgumentException("Un equipo no puede ser creado sin un líder.");
            }

            // Manejo de bool
            if (!isAutomatic)
            {
                teamDto.TeamStatusId = 2;
            }
            else
            {
                teamDto.TeamStatusId = 1;
            }

            var team = new TfaTeam
            {
                TeamName = teamDto.TeamName,
                TeamDescription = teamDto.TeamDescription,
                TeamStatusId = teamDto.TeamStatusId,
                TeamLeadId = teamDto.TeamLeadId,
                CategoriesId = teamDto.CategoriesId // Puede ser null
            };
            
            // Agregar a la base de datos
            _context.TfaTeams.Add(team);
            await _context.SaveChangesAsync();

            return team;

        }


        public async Task UpdateTeamAsync(TeamDTO teamDto)
        {
            // Validar líder obligatorio
            if (teamDto.TeamLeadId == null || teamDto.TeamLeadId <= 0)
            {
                throw new ArgumentException("Un equipo no puede ser actualizado sin un líder.");
            }

            var team = new TfaTeam
            {
                TeamId = teamDto.TeamId,
                TeamName = teamDto.TeamName,
                TeamDescription = teamDto.TeamDescription,
                TeamStatusId = teamDto.TeamStatusId,
                TeamLeadId = teamDto.TeamLeadId,
                CategoriesId = teamDto.CategoriesId // Puede ser null
            };

            await _teamRepository.UpdateTeamAsync(team);
        }

        public async Task DeleteTeamAsync(int id)
        {
            await _teamRepository.DeleteTeamAsync(id);
        }
        
        public async Task<TeamDTO> GetTeamByNameAsync(string teamName)
        {
            var team = await _teamRepository.GetByNameAsync(teamName);
            if (team == null)
            {
                throw new Exception("Team not found");
            }
            return new TeamDTO { TeamId = team.TeamId, 
                                 TeamName = team.TeamName,
                                 TeamStatusName = team.TeamStatus?.StatusName ?? "Estado desconocido",
                                 TeamLeadName = team.TeamLead?.UserName ?? "Líder desconocido",
                                 Category = team.Categories?.CategoryName ?? "Categoría desconocida"
            };
        }

        public async Task AddCollaboratorsToTeamAsync(int teamId, List<int> userIds)
        {
            if (userIds == null || !userIds.Any())
            {
                throw new ArgumentException("The list of user IDs cannot be null or empty.", nameof(userIds));
            }

            await _teamRepository.AddCollaboratorsToTeamAsync(teamId, userIds);
        }

        // Obtener colaboradores de un equipo
        public async Task<IEnumerable<TfaUser>> GetCollaboratorsByTeamIdAsync(int teamId)
        {
            var collaborators = await _teamRepository.GetCollaboratorsByTeamIdAsync(teamId);
            if (!collaborators.Any())
            {
                throw new KeyNotFoundException($"No collaborators found for team ID {teamId}.");
            }

            return collaborators;
        }

        public async Task<IEnumerable<TfaUser>> GetLeadersandAdmins()
        {
            return await _teamRepository.GetLeadersandAdmins();
        }

        public async Task<IEnumerable<TfaUser>> GetColaborators()
        {
            return await _teamRepository.GetColaborators();
        }


    }
}


