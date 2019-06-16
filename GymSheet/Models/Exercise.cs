using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymSheet.Models
{
    public class Exercise
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "campo obrigatório")]
        [Display(Name = "Nome")]
        [StringLength(50, ErrorMessage = "use até {1} caracteres")]
        [Remote("ExerciseExist", "Exercises", AdditionalFields = "Id")]
        public string Name { get; set; }

        [Required(ErrorMessage = "é preciso cadastrar um grupo muscular primeiro")]
        public int MuscleGroupId { get; set; }

        [Display(Name = "Grupo muscular")]
        public MuscleGroup MuscleGroup { get; set; }

        public ICollection<ExerciseList> ExerciseLists { get; set; }
    }
}
