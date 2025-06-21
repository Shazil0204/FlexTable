using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs
{
    public abstract class BaseResponseDTO
    {
        [Required]
        public virtual bool Success { get; set; }

        private protected BaseResponseDTO(bool success = true)
        {
            Success = success;
        }
    }
}