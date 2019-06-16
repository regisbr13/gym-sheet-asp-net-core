using System.ComponentModel.DataAnnotations;

namespace GymSheet.Models
{
    public class ExerciseList
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="campo obrigatório")]
        [Display(Name = "Frequência")]
        [Range(1, 10, ErrorMessage = "frequência inválida")]
        public int Frequency { get; set; }

        [Required(ErrorMessage = "campo obrigatório")]
        [Display(Name = "Repetições")]
        [Range(1, 100, ErrorMessage = "número de repetições inválido")]
        public int Repetitions { get; set; }

        [Required(ErrorMessage = "campo obrigatório")]
        [Display(Name = "Carga")]
        [Range(1, 200, ErrorMessage = "carga inválida")]
        public int Charge { get; set; }

        public int SheetId { get; set; }

        public Sheet Sheet { get; set; }

        public int ExerciseId { get; set; }

        public Exercise Exercise { get; set; }
    }
}
