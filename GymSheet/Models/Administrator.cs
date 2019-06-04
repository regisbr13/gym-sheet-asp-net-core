using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymSheet.Models
{
    public class Administrator
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="campo obrigatório")]
        [Display(Name = "Nome")]
        [StringLength(50, ErrorMessage ="use até {1} caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "campo obrigatório")]
        [Display(Name = "Email")]
        [StringLength(50, ErrorMessage = "use até {1} caracteres")]
        [EmailAddress(ErrorMessage ="entre com um email válido")]
        public string Email { get; set; }

        [NotMapped]
        [Display(Name = "Confirmar email")]
        [Compare("Email", ErrorMessage = "emails não correspodem")]
        [EmailAddress(ErrorMessage = "email inválido")]
        public string EmailConf { get; set; }

        [Required(ErrorMessage = "campo obrigatório")]
        [Display(Name = "Senha")]
        [StringLength(10, ErrorMessage = "use até {1} caracteres")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        [Display(Name = "Confirmar senha")]
        [Compare("Password", ErrorMessage = "senhas não correspodem")]
        [DataType(DataType.Password)]
        public string PasswordConf { get; set; }
    }
}
