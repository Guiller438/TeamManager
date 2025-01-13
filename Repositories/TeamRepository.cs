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


    }
}
