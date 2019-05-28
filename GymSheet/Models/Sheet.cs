using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GymSheet.Models
{
    public class Sheet
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "campo obrigatório")]
        [Display(Name = "Nome")]
        [StringLength(15, ErrorMessage = "use até {1} caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "campo obrigatório")]
        [Display(Name = "Data de cadastro")]
        [DisplayFormat(DataFormatString ="0:dd/MM/yyyy", ApplyFormatInEditMode =true)]
        public DateTime Register { get; set; }

        [Required(ErrorMessage = "campo obrigatório")]
        [Display(Name = "Válida até")]
        [DisplayFormat(DataFormatString = "0:dd/MM/yyyy", ApplyFormatInEditMode = true)]
        public DateTime Validate { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }

        public ICollection<ExcerciseList> ExcerciseLists { get; set; }
    }
}
