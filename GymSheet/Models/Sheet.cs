using Microsoft.AspNetCore.Mvc;
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

        [Remote("SheetExist", "Sheets", AdditionalFields = "Id")]
        [Required(ErrorMessage = "campo obrigatório")]
        [Display(Name = "Nome")]
        [StringLength(15, ErrorMessage = "use até {1} caracteres")]
        public string Name { get; set; }

        [Display(Name = "Cadastro")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Register { get; set; }

        [Display(Name = "Validade")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Validate { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }

        public ICollection<ExcerciseList> ExcerciseLists { get; set; }
    }
}
