using System.ComponentModel.DataAnnotations;

namespace GymSheet.Models
{
    public class ExcerciseList
    {
        public int Id { get; set; }

        [Display(Name = "Frequência")]
        [Range(1, 10, ErrorMessage = "frequência inválida")]
        public int Frequency { get; set; }

        [Display(Name = "Repetições")]
        [Range(1, 100, ErrorMessage = "número de repetições inválido")]
        public int Repetitions { get; set; }

        [Display(Name = "Carga")]
        [Range(1, 200, ErrorMessage = "carga inválida")]
        public int Charge { get; set; }

        public int SheetId { get; set; }

        public Sheet Sheet { get; set; }

        public int ExcerciseId { get; set; }

        public Excercise Excercise { get; set; }
    }
}
