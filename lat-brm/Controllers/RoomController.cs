using lat_brm.Contracts.Repositories;
using lat_brm.Contracts.Services;
using lat_brm.Dtos.Room;
using lat_brm.Models;
using lat_brm.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lat_brm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            this._roomService = roomService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var rooms = _roomService.GetAll();
            return Ok(rooms);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            var room = _roomService.GetById(id);
            if (room is null)
            {
                return NotFound("Room not found");
            }
            return Ok(room);
        }

        [HttpPost]
        public IActionResult Insert(RoomRequestInsert request)
        {
            var room = _roomService.Insert(request);
            if (room is null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok(room);
        }

        [HttpPut]
        public IActionResult Update(RoomRequestUpdate request)
        {
            var room = _roomService.Update(request);
            if (room is null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok(room);
        }

        [HttpDelete("{id:Guid}")]
        public IActionResult Delete(Guid id)
        {
            var isDeleted = _roomService.Delete(id);
            if (!isDeleted)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }
    }
}
