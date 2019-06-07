using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymSheet.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "campo obrigatório")]
        [Display(Name = "Nome completo")]
        [StringLength(100, ErrorMessage = "use até {1} caracteres")]
        public string Name { get; set; }

        [Display(Name = "Idade")]
        [Range(14, 100, ErrorMessage ="idade válida")]
        public int Age { get; set; }

        [Display(Name = "Peso")]
        [Range(20, 200, ErrorMessage = "peso inválido")]
        [Required(ErrorMessage ="campo obrigatório")]
        public double Weight { get; set; }

        [Display(Name = "Altura")]
        [Range(1, 2.5, ErrorMessage = "altura inválida")]
        [Required(ErrorMessage = "campo obrigatório")]
        public double Height { get; set; }

        [Display(Name = "Frequência semanal")]
        [Range(1, 7, ErrorMessage = "frequência inválida")]
        public int WeeklyFrequency { get; set; }

        public int TeacherId { get; set; }

        [Display(Name = "Professor")]
        public Teacher Teacher { get; set; }

        public int ObjectiveId { get; set; }

        [Display(Name = "Objetivo")]
        public Objective Objective { get; set; }

        public ICollection<Sheet> Sheets { get; set; }
    }
}
