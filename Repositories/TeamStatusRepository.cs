using Microsoft.EntityFrameworkCore;
using TeamManager.Data;
using TeamManager.Models;

namespace TeamManager.Repositories
{
    public class TeamStatusRepository : ITeamStatusRepository
    {
        private readonly DbAb0bdeTalentseedsContext _context;

        public TeamStatusRepository(DbAb0bdeTalentseedsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TfaTeamstatus>> GetAllStatusesAsync()
        {
            return await _context.TfaTeamstatuses.ToListAsync(); // Recupera todos los estados
        }

        public async Task<TfaTeamstatus> GetStatusByIdAsync(int id)
        {
            return await _context.TfaTeamstatuses.FindAsync(id); // Encuentra un estado por su ID
        }

        public async Task AddStatusAsync(TfaTeamstatus teamStatus)
        {
            await _context.TfaTeamstatuses.AddAsync(teamStatus);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStatusAsync(TfaTeamstatus teamStatus)
        {
            _context.TfaTeamstatuses.Update(teamStatus);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStatusAsync(int id)
        {
            var status = await GetStatusByIdAsync(id);
            if (status != null)
            {
                _context.TfaTeamstatuses.Remove(status);
                await _context.SaveChangesAsync();
            }
        }
    }
}
