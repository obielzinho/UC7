using System.ComponentModel.DataAnnotations;
using System.Security;

namespace apiAutenticacao.Models.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage =" O email é obirgatorio")]
        [EmailAddress(ErrorMessage =" O emial é invalido ")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = " A senha é obrigatoria")]
        public string Senha { get; set; } = string.Empty;
    }
}
