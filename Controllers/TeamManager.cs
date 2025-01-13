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

        [HttpGet]
        public async Task<IActionResult> GetAllTeams()
        {
            var teams = await _teamService.GetAllTeamsAsync();
            return Ok(teams);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeamById(int id)
        {
            var team = await _teamService.GetTeamByIdAsync(id);
            if (team == null) return NotFound();

            return Ok(team);
        }

        [HttpPost("{isAutomatic}")]
        public async Task<IActionResult> AddTeam([FromBody] TeamDTO teamDto, bool isAutomatic)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _teamService.AddTeamAsync(teamDto, isAutomatic);
            return CreatedAtAction(nameof(GetTeamById), new { id = teamDto.TeamId }, teamDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeam(int id, [FromBody] TeamDTO teamDto)
        {
            if (!ModelState.IsValid || id != teamDto.TeamId) return BadRequest();

            await _teamService.UpdateTeamAsync(teamDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            await _teamService.DeleteTeamAsync(id);
            return NoContent();
        }

        [HttpPost("manual")]
        public async Task<IActionResult> CreateTeamManual([FromBody] TeamDTO teamDto, bool isAutomatic)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            

            try
            {
                // Llamar al servicio para crear el equipo
                await _teamService.AddTeamAsync(teamDto, isAutomatic: true);

                // Retornar una respuesta sin el ID del equipo
                return Ok(new { message = "Equipo creado exitosamente." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex, "Error al crear el equipo.");
                return StatusCode(500, "Ocurrió un error interno del servidor.");
            }
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            var team = await _teamService.GetTeamByNameAsync(name);
            return Ok(team);
        }

    }
}
