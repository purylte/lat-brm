using lat_brm.Models;

namespace lat_brm.Dtos.Room
{
    public class RoomResponse
    {
        public Guid Guid { get; set; }
        public string? Name { get; set; }
        public int? Floor { get; set; }
        public int? Capacity { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    
        public static explicit operator RoomResponse(TbMRoom room)
        {
            return new RoomResponse
            {
                Guid = room.Guid,
                Name = room.Name,
                Floor = room.Floor,
                Capacity = room.Capacity,
                ModifiedDate = room.ModifiedDate,
                CreatedDate = room.CreatedDate,
            };
        }
    }
}
