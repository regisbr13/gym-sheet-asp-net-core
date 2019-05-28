using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymSheet.Models
{
    public class Excercise
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "campo obrigatório")]
        [Display(Name = "Nome")]
        [StringLength(50, ErrorMessage = "use até {1} caracteres")]
        public string Name { get; set; }

        public int MuscleGroupId { get; set; }

        public MuscleGroup MuscleGroup { get; set; }

        public ICollection<ExcerciseList> ExcerciseLists { get; set; }
    }
}
