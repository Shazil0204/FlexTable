using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs
{
    public class FailureResponseDTO : BaseResponseDTO
    {
        [Required]
        public string Message { get; set; }
        public string? Details { get; set; }
        public string? Exception { get; set; }

        public FailureResponseDTO(string message, string? details = null) : base(false)
        {
            Message = message;
            Details = details;
        }
    }
}