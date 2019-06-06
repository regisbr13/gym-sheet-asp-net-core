using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymSheet.Models
{
    public class Objective
    {
        public int Id { get; set; }

        [Remote("ObjectiveExist", "Objectives", AdditionalFields = "Id")]
        [Required(ErrorMessage = "campo obrigatório")]
        [Display(Name = "Nome")]
        [StringLength(50, ErrorMessage = "use até {1} caracteres")]
        public string Name { get; set; }

        [Display(Name = "Observação")]
        [StringLength(50, ErrorMessage = "use até {1} caracteres")]
        public string Observation { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
