using System.ComponentModel.DataAnnotations;

namespace apiAutenticacao.Models.DTO
{
    public class CadastroUsuarioDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, MinimumLength =2)]

        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "O email informado não é válido")]

        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatório")]
        [StringLength(100, MinimumLength = 6, ErrorMessage =" A senha deve ter entre 6 a 100 caracteres")]
        public string Senha { get; set; } = string.Empty;

        [Required(ErrorMessage = "O nome é obrigatório")]
        [Compare("Senha", ErrorMessage = " as senhas não conferem")]
        public string ConfirmarSenha { get; set; } = string.Empty;
    }
}
