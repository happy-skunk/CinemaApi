using CinemaApi.DTOs.Actor;
using CinemaApi.Services.Actor;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActorService _actorService;

        public ActorController(IActorService actorService)
        {
            _actorService = actorService;
        }

        [HttpPost]
        public async Task<IActionResult> AddActor([FromBody] ActorCreateDto dto)
        {
            var id = await _actorService.CreateActorAsync(dto);
            return CreatedAtAction(nameof(GetActor), new { id }, dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetActors()
        {
            var actors = await _actorService.GetAllActorsAsync();
            return Ok(actors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetActor(int id)
        {
            var actor = await _actorService.GetActorByIdAsync(id);
            if (actor == null) return NotFound();
            return Ok(actor);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateActor([FromBody] ActorUpdateDto dto)
        {
            await _actorService.UpdateActorAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActor(int id)
        {
            var deleted = await _actorService.DeleteActorAsync(id);
            if (deleted == null) return NotFound();
            return Ok(deleted);
        }
    }
}
