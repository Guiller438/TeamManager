using Microsoft.EntityFrameworkCore;
using TeamManager.Data;
using TeamManager.DTOs;
using TeamManager.Models;

namespace TeamManager.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly DbAb0bdeTalentseedsContext _context;

        public TeamRepository(DbAb0bdeTalentseedsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TfaTeam>> GetAllTeamsAsync()
        {
            return await _context.TfaTeams
                        .Include(t => t.TeamLead) // Incluir el líder del equipo
                        .Include(t => t.TeamStatus)
                        .Include(t => t.Categories) // Incluir el estado del equipo
                        .ToListAsync(); // Incluye la relación con TeamStatus
        }

        public async Task<TfaTeam> GetTeamByIdAsync(int id)
        {
            return await _context.TfaTeams.Include(t => t.TeamStatus)
                                            .Include(t => t.TeamLead) // Incluir el líder del equipo
                                          .Include(t => t.TeamStatus)
                                          .Include(t => t.Categories) // Incluir el estado del equipo
                                          .FirstOrDefaultAsync(t => t.TeamId == id);
        }

        public async Task AddTeamAsync(TfaTeam team)
        {
            await _context.TfaTeams.AddAsync(team);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTeamAsync(TfaTeam team)
        {
            _context.TfaTeams.Update(team);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTeamAsync(int id)
        {
            var team = await GetTeamByIdAsync(id);

            if (team != null)
            {
                // Eliminar registros de la tabla intermedia tfa_teams_colaborators
                var teamCollaborators = _context.TfaTeamsCollaborators
                    .Where(tc => tc.ColaboratorTeamID == id);

                _context.TfaTeamsCollaborators.RemoveRange(teamCollaborators);

                // Eliminar registros de la tabla intermedia tfa_teams_categories
                //var teamCategories = _context.
                //    .Where(tc => tc.TeamId == id);

                //_context.TfaTeamsCategories.RemoveRange(teamCategories);

                // Guardar cambios después de eliminar las referencias
                await _context.SaveChangesAsync();

                // Finalmente, eliminar el equipo de la tabla principal
                _context.TfaTeams.Remove(team);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<TfaTeam> GetByNameAsync(string name)
        {
            return await _context.TfaTeams
                        .Include(t => t.TeamLead) // Incluir el líder del equipo
                        .Include(t => t.TeamStatus)
                        .Include(t => t.Categories) // Incluir el estado del equipo
                        .FirstOrDefaultAsync(t => t.TeamName == name); // Filtro para buscar por nombre
        }

        public async Task AddCollaboratorsToTeamAsync(int teamId, List<int> userIds)
        {
            var existingCollaborators = await _context.TfaTeamsCollaborators
                .Where(tc => tc.ColaboratorTeamID == teamId && userIds.Contains(tc.ColaboratorUsersID))
                .Select(tc => tc.ColaboratorUsersID)
                .ToListAsync();

            var newCollaborators = userIds
                .Where(userId => !existingCollaborators.Contains(userId))
                .Select(userId => new TfaTeamCollaborators
                {
                    ColaboratorTeamID = teamId,
                    ColaboratorUsersID = userId
                }).ToList();

            if (newCollaborators.Any())
            {
                _context.TfaTeamsCollaborators.AddRange(newCollaborators);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<IEnumerable<TfaUser>> GetCollaboratorsByTeamIdAsync(int teamId)
        {
            return await _context.TfaTeamsCollaborators
                .Where(tc => tc.ColaboratorTeamID == teamId)
                .Select(tc => tc.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<TfaUser>> GetLeadersandAdmins()
        {
            return await _context.TfaUsers
                .Where(u => u.RolId == 1 || u.RolIdaddional== 2)
                .ToListAsync();
        }

        public async Task<IEnumerable<TfaUser>> GetColaborators()
        {
            return await _context.TfaUsers
                .Where(u => u.RolId == 3)
                .ToListAsync();
        }
    }

}

