using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymSheet.Models
{
    public class MuscleGroup
    {
        public int Id { get; set; }

        [Remote("MuscleGroupExist", "MuscleGroups", AdditionalFields ="Id")]
        [Required(ErrorMessage = "campo obrigatório")]
        [Display(Name = "Nome")]
        [StringLength(50, ErrorMessage = "use até {1} caracteres")]
        public string Name { get; set; }

        public ICollection<Exercise> Excercises { get; set; }
    }
}
