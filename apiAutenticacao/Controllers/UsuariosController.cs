using apiAutenticacao.Data;
using apiAutenticacao.Models;
using apiAutenticacao.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static BCrypt.Net.BCrypt;
using BCrypt.Net;
using apiAutenticacao.Services;
using apiAutenticacao.Models.Response;

namespace apiAutenticacao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly AuthService _authService;

        public UsuariosController(AppDbContext context, AuthService authService)
        {
            _authService = authService;
            _context = context;
        }

        [HttpPost("cadastrar")]
        public async Task<ActionResult> CadastrarUsuarioAsync([FromBody] CadastroUsuarioDTO dadosUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResponseCadastro response = await _authService.CadastrarUsuarioAsync(dadosUsuario);

            if (response.Erro)
            {

                return BadRequest(response.Erro);
            }

            return Ok(response);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dadosUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResponseLogin response = await _authService.Login(dadosUsuario);

            if (response.Erro)
            {

                return BadRequest(response.Erro);
            }

            return Ok(response);
        }
        [HttpPut("AlterarSenha")]

        public async Task<IActionResult> AlterarSenha([FromBody] AlterarSenhaDTO dadosAlterarSenha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Usuario? usuario = await _context.Usuarios.FirstOrDefaultAsync(usuario => usuario.Email == dadosAlterarSenha.Email);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }
            bool isValidPassword = Verify(dadosAlterarSenha.SenhaAtual, usuario.Senha);
            if (!isValidPassword)
            {
                return BadRequest("Senha atual incorreta.");
            }
            usuario.Senha = HashPassword(dadosAlterarSenha.NovaSenha);
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
            return Ok("Senha alterada com sucesso.");
        }
    }
}
