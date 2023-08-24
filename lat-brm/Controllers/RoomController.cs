using lat_brm.Contracts;
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
        private readonly IRoomRepository _roomRepository;

        public RoomController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<TbMRoom> rooms;
            try
            {
                rooms = _roomRepository.GetAll();
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            var roomResponses = rooms.Select(room => (RoomResponse)room);
            return Ok(roomResponses);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            TbMRoom? room;
            try
            {
                room = _roomRepository.GetByGuid(id);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            if (room == null)
            {
                return NotFound("Room not found");
            }
            return Ok((RoomResponse)room);
        }

        [HttpPost]
        public IActionResult Insert(RoomRequestInsert request)
        {
            TbMRoom room;
            try
            {
                room = _roomRepository.Insert(request);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return Ok((RoomResponse)room);
        }

        [HttpPut]
        public IActionResult Update(RoomRequestUpdate request)
        {
            TbMRoom? room;
            try
            {
                room = _roomRepository.GetByGuid(request.Guid);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            if (room == null)
            {
                return NotFound("Room not found");
            }
            TbMRoom requestObj = request;
            requestObj.CreatedDate = room.CreatedDate;
            TbMRoom response;
            try
            {
                response = _roomRepository.Update(requestObj);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return Ok((RoomResponse)response);
        }

        [HttpDelete("{id:Guid}")]
        public IActionResult Delete(Guid id)
        {
            TbMRoom? room;
            try
            {
                room = _roomRepository.GetByGuid(id);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            if (room == null)
            {
                return NotFound("Room not found");
            }

            try
            {
                _roomRepository.Delete(room);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }
    }
}
