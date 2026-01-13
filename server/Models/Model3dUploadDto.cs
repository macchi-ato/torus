using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class Model3dUploadDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public required IFormFile File { get; set; }
    }
}
