using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymSheet.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "campo obrigatório")]
        [Display(Name = "Nome")]
        [StringLength(50, ErrorMessage = "use até {1} caracteres")]
        public string Name { get; set; }

        [Display(Name = "Foto")]
        public string ImgPath { get; set; }

        [Required(ErrorMessage = "campo obrigatório")]
        [Display(Name = "Telefone")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "campo obrigatório")]
        [Display(Name = "Turno")]
        [StringLength(10, ErrorMessage = "use até {1} caracteres")]
        public string Turno { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
