using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.Enum;

namespace Backend.Entities
{
    public class UserColumn
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Table")]
        public int TableId { get; set; }
        public UserTable? Table { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public ColumnDataType DataType { get; set; }
        [Required]
        public bool IsRequired { get; set; }

        public UserColumn(int tableId, string name, ColumnDataType dataType, bool isRequired)
        {
            TableId = tableId;
            Name = name;
            DataType = dataType;
            IsRequired = isRequired;
        }

        private UserColumn() // EF Core requires a parameterless constructor
        {
            TableId = 0;
            Name = string.Empty;
            DataType = ColumnDataType.Text;
            IsRequired = false;
        }
    }
}