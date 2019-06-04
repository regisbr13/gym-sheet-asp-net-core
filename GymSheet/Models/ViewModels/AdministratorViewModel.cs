using System.ComponentModel.DataAnnotations;

namespace GymSheet.Models.ViewModels
{
    public class AdministratorViewModel
    {
        [Required(ErrorMessage = "campo obrigatório")]
        [Display(Name = "Email")]
        [StringLength(50, ErrorMessage = "use até {1} caracteres")]
        [EmailAddress(ErrorMessage = "entre com um email válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "campo obrigatório")]
        [Display(Name = "Senha")]
        [StringLength(10, ErrorMessage = "use até {1} caracteres")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
