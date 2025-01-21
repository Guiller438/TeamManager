using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManager.DTOs;
using TeamManager.Services;

namespace TeamManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamServices _teamService;

        public TeamController(ITeamServices teamService)
        {
            _teamService = teamService;
        }

        [HttpGet("getAllTeams")]
        public async Task<IActionResult> GetAllTeams()
        {
            var teams = await _teamService.GetAllTeamsAsync();
            return Ok(teams);
        }

        [HttpGet("getTeamById/{id}")]
        public async Task<IActionResult> GetTeamById(int id)
        {
            var team = await _teamService.GetTeamByIdAsync(id);
            if (team == null) return NotFound();

            return Ok(team);
        }

        [HttpPost("solicitarEquipo")]
        public async Task<IActionResult> SolicitarEquipos([FromBody] AddTeamsWithColaboratorsDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var isAutomatic = false;
            // Agregar el equipo primero
            var teamId = await _teamService.AddTeamAsync(request.Team, isAutomatic);

            // Agregar colaboradores si la lista no está vacía
            if (request.UserIds != null && request.UserIds.Any())
            {
                await _teamService.AddCollaboratorsToTeamAsync(teamId.TeamId, request.UserIds);
            }

            return CreatedAtAction(nameof(GetTeamById), new { id = teamId }, new
            {
                Message = "Team created successfully with collaborators.",
                TeamId = teamId
            });
        }

        [HttpPost("crearEquipoAdministrador")]
        public async Task<IActionResult> crearEquipoAdministrador([FromBody] AddTeamsWithColaboratorsDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var isAutomatic = true;
            // Agregar el equipo primero
            var teamId = await _teamService.AddTeamAsync(request.Team, isAutomatic);

            // Agregar colaboradores si la lista no está vacía
            if (request.UserIds != null && request.UserIds.Any())
            {
                await _teamService.AddCollaboratorsToTeamAsync(teamId.TeamId, request.UserIds);
            }

            return CreatedAtAction(nameof(GetTeamById), new { id = teamId }, new
            {
                Message = "Team created successfully with collaborators.",
                TeamId = teamId
            });


        }

        [HttpPut("updateTeam/{id}")]
        public async Task<IActionResult> UpdateTeam(int id, [FromBody] TeamDTO teamDto)
        {
            if (!ModelState.IsValid || id != teamDto.TeamId) return BadRequest();

            await _teamService.UpdateTeamAsync(teamDto);
            return NoContent();
        }

        [HttpDelete("deleteTeam/{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            await _teamService.DeleteTeamAsync(id);
            return NoContent();
        }


        [HttpGet("teampornombre/GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            var team = await _teamService.GetTeamByNameAsync(name);
            return Ok(team);
        }

        // Endpoint para obtener colaboradores de un equipo
        [HttpGet("collaborators/{teamId}")]
        public async Task<IActionResult> GetCollaboratorsByTeamId(int teamId)
        {
            var collaborators = await _teamService.GetCollaboratorsByTeamIdAsync(teamId);

            if (!collaborators.Any())
            {
                return NotFound(new { Message = $"No collaborators found for team ID {teamId}." });
            }

            return Ok(collaborators);
        }

        [HttpGet("leadersandadmins")]
        public async Task<IActionResult> GetLeadersandAdmins()
        {
            var leadersandadmins = await _teamService.GetLeadersandAdmins();
            return Ok(leadersandadmins);
        }

        [HttpGet("colaborators")]

        public async Task<IActionResult> GetColaborators()
        {
            var colaborators = await _teamService.GetColaborators();
            return Ok(colaborators);
        }

    }
}
