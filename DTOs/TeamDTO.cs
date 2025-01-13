using System.ComponentModel.DataAnnotations;

namespace TeamManager.DTOs
{
    public class TeamDTO
    {
        public int TeamId { get; set; }

        [Required(ErrorMessage = "El nombre del equipo es obligatorio.")]
        public string TeamName { get; set; }

        [Required(ErrorMessage = "La descripción del equipo es obligatoria.")]
        public string TeamDescription { get; set; }

        [Required(ErrorMessage = "El estado del equipo es obligatorio.")]
        public int TeamStatusId { get; set; } // 1: Manual, 2: Automático (aprobado)

        [Required(ErrorMessage = "El líder del equipo es obligatorio.")]
        public int? TeamLeadId { get; set; } // Debe ser obligatorio

        public int? CategoriesId { get; set; } // Puede ser nulo

        public string TeamStatusName { get; set; } // Nombre del estado
        public string TeamLeadName { get; set; }   // Nombre del líder
        public string Category {  get; set; }   //Nombre Categoria
    }
}
