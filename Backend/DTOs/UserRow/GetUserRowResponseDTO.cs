namespace Backend.DTOs.UserRow
{
    public class GetUserRowResponseDTO : BaseResponseDTO
    {
        public int Id { get; set; }
        public string Data { get; set; }
        public DateTime CreatedAt { get; set; }
        public GetUserRowResponseDTO(int id, string data, DateTime createdAt)
        {
            Id = id;
            Data = data;
            CreatedAt = createdAt;
        }
    }
}