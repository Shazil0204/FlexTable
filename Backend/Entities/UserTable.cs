using System.ComponentModel.DataAnnotations;

namespace Backend.Entities
{
    public class UserTable
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public List<UserColumn> Columns { get; set; } = [];
        public List<UserRow> Rows { get; set; } = [];

        public UserTable(string name)
        {
            Name = name;
            CreatedAt = DateTime.UtcNow;
            // Columns and Rows are already initialized
        }

        private UserTable() // EF Core requires a parameterless constructor
        {
            Name = string.Empty;
            CreatedAt = DateTime.UtcNow;
            // Columns and Rows are already initialized
        }
    }
}