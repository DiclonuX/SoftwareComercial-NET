using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class ComercianteDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(200)]
        public string NombreRazonSocial { get; set; } = string.Empty;

        [Phone]
        public string? Telefono { get; set; }

        [EmailAddress]
        public string? CorreoElectronico { get; set; }

        [Required(ErrorMessage = "El municipio es obligatorio")]
        public int IdMunicipio { get; set; }

        public bool Estado { get; set; } = true;
    }
}
