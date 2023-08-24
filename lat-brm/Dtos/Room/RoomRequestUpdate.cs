using lat_brm.Models;

namespace lat_brm.Dtos.Room
{
    public class RoomRequestUpdate
    {
        public Guid Guid { get; set; }
        public string? Name { get; set; }
        public int? Floor { get; set; }
        public int? Capacity { get; set; }

        public static implicit operator TbMRoom(RoomRequestUpdate roomRequest)
        {
            return new TbMRoom
            {
                Guid = roomRequest.Guid,
                Name = roomRequest.Name,
                Floor = roomRequest.Floor,
                Capacity = roomRequest.Capacity,
                ModifiedDate = DateTime.Now,
            };

        }
    }
}
