namespace apiAutenticacao.Models.Response
{
    public class ResponseNovaSenha
    {
        public bool Erro { get; set; }

        public string Message { get; set; } = string.Empty;

        public Usuario? Usuario { get; set; }
        
    }
}