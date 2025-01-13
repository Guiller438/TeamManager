using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManager.DTOs;
using TeamManager.Services;

namespace TeamManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class TeamStatusManager : ControllerBase
    {
        private readonly ITeamStatusServices _teamStatusService;

        public TeamStatusManager(ITeamStatusServices teamStatusService)
        {
            _teamStatusService = teamStatusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStatuses()
        {
            var statuses = await _teamStatusService.GetAllStatusesAsync();
            return Ok(statuses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStatusById(int id)
        {
            var status = await _teamStatusService.GetStatusByIdAsync(id);
            if (status == null) return NotFound();

            return Ok(status);
        }

        [HttpPost]
        public async Task<IActionResult> AddStatus([FromBody] TeamStatusDTO teamStatusDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _teamStatusService.AddStatusAsync(teamStatusDto);
            return CreatedAtAction(nameof(GetStatusById), new { id = teamStatusDto.StatusId }, teamStatusDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] TeamStatusDTO teamStatusDto)
        {
            if (!ModelState.IsValid || id != teamStatusDto.StatusId) return BadRequest();

            await _teamStatusService.UpdateStatusAsync(teamStatusDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            await _teamStatusService.DeleteStatusAsync(id);
            return NoContent();
        }

        [HttpPost("seed")]
        public async Task<IActionResult> SeedStatuses()
        {
            // Crear dos estados quemados
            var status1 = new TeamStatusDTO
            {       
                StatusName = "Activo"
            };

            var status2 = new TeamStatusDTO
            {
                StatusName = "Inactivo"
            };

            // Añadir los estados usando el servicio
            await _teamStatusService.AddStatusAsync(status1);
            await _teamStatusService.AddStatusAsync(status2);

            return Ok("Datos quemados añadidos exitosamente.");
        }
    }
}
