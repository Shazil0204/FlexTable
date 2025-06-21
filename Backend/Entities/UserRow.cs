using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities
{
    public class UserRow
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Table")]
        public int TableId { get; set; }
        public UserTable? Table { get; set; }
        [Required]
        public string Data { get; set; } // JSON string
        [Required]
        public DateTime CreatedAt { get; set; }

        public UserRow(int tableId, string data)
        {
            TableId = tableId;
            Data = data;
            CreatedAt = DateTime.UtcNow;
        }

        private UserRow() // EF Core requires a parameterless constructor
        {
            TableId = 0;
            Data = string.Empty;
            CreatedAt = DateTime.UtcNow;
        }
    }
}