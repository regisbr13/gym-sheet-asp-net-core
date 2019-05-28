using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GymSheet.Models
{
    public class MuscleGroup
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "campo obrigatório")]
        [Display(Name = "Nome")]
        [StringLength(50, ErrorMessage = "use até {1} caracteres")]
        public string Name { get; set; }

        public ICollection<Excercise> Excercises { get; set; }
    }
}
