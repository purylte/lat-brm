using lat_brm.Contracts.Repositories;
using lat_brm.Contracts.Services;
using lat_brm.Dtos.Room;
using lat_brm.Models;
using Microsoft.AspNetCore.Mvc;

namespace lat_brm.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public bool Delete(Guid id)
        {
            var room = _roomRepository.GetByGuid(id);
            if (room is null) return false;
            try
            {
                _roomRepository.Delete(room);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public IEnumerable<RoomResponse> GetAll()
        {
            var rooms = _roomRepository.GetAll();
            var result = rooms.Select(room => (RoomResponse)room);
            return result;
        }

        public RoomResponse? GetById(Guid id)
        {
            var room = _roomRepository.GetByGuid(id);
            if (room is null) return null;
            return (RoomResponse)room;
        }

        public RoomResponse? Insert(RoomRequestInsert request)
        {
            TbMRoom room;
            try
            {
                room = _roomRepository.Insert(request);
            }
            catch
            {
                return null;
            }

            return (RoomResponse)room;
        }

        public RoomResponse? Update(RoomRequestUpdate request)
        {
            var room = _roomRepository.GetByGuid(request.Guid);
            if (room is null)
            {
                return null;
            }

            TbMRoom requestObj = request;
            requestObj.CreatedDate = room.CreatedDate;
            TbMRoom updatedRoom;
            try
            {
                updatedRoom = _roomRepository.Update(requestObj);
            }
            catch
            {
                return null;
            }
            return (RoomResponse)updatedRoom;
        }
    }


}
