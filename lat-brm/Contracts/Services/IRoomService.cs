using lat_brm.Dtos.Room;

namespace lat_brm.Contracts.Services
{
    public interface IRoomService
    {
        IEnumerable<RoomResponse> GetAll();
        RoomResponse? GetById(Guid id);
        RoomResponse? Insert(RoomRequestInsert request);
        RoomResponse? Update(RoomRequestUpdate request);
        bool Delete(Guid id);
    }
}
