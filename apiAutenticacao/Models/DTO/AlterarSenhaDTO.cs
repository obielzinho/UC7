using System.ComponentModel.DataAnnotations;
using static BCrypt.Net.BCrypt;

namespace apiAutenticacao.Models.DTO
{
    public class AlterarSenhaDTO
    {

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "O email informado não é válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatório")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = " A senha deve ter entre 6 a 100 caracteres")]
        public string SenhaAtual { get; set; }

        [Required(ErrorMessage = "A senha é obrigatório")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = " A senha deve ter entre 6 a 100 caracteres")]
        public string NovaSenha { get; set; } = string.Empty;

        [Required(ErrorMessage = "O nome é obrigatório")]
        [Compare("NovaSenha", ErrorMessage = " as senhas não conferem")]
        public string ConfirmacaoNovaSenha { get; set; }
    }
}
