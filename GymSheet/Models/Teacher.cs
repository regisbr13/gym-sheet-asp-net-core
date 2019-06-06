using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymSheet.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        [Remote("TeacherExist", "Teachers", AdditionalFields = "Id")]
        [Required(ErrorMessage = "campo obrigatório")]
        [Display(Name = "Nome")]
        [StringLength(50, ErrorMessage = "use até {1} caracteres")]
        public string Name { get; set; }

        [Display(Name = "Foto")]
        public string ImgPath { get; set; }

        [Required(ErrorMessage = "campo obrigatório")]
        [Display(Name = "Turno")]
        [StringLength(15, ErrorMessage = "use até {1} caracteres")]
        public string Period { get; set; }

        [Required(ErrorMessage = "campo obrigatório")]
        [Display(Name = "Telefone")]
        public string Phone { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
