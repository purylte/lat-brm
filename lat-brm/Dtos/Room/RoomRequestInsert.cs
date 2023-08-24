using lat_brm.Models;

namespace lat_brm.Dtos.Room
{
    public class RoomRequestInsert
    {
        public string? Name { get; set; }
        public int? Floor { get; set; }
        public int? Capacity { get; set; }
    
        public static implicit operator TbMRoom(RoomRequestInsert roomRequest)
        {
            return new TbMRoom
            {
                Guid = Guid.NewGuid(),
                Name = roomRequest.Name,
                Floor = roomRequest.Floor,
                Capacity = roomRequest.Capacity,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
    
        }
    }

    
}
