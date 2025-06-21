namespace Backend.DTOs.UserRow
{
    public class UpdateUserRowRequestDTO
    {
        public string? Name { get; set; }
        public string? Data { get; set; } // JSON string

        public UpdateUserRowRequestDTO(string? name, string? data)
        {
            Name = name;
            Data = data;
        }

        public UpdateUserRowRequestDTO() // EF Core requires a parameterless constructor
        {
            Name = string.Empty;
            Data = string.Empty;
        }
    }
}