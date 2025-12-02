using apiAutenticacao.Data;
using apiAutenticacao.Models;
using apiAutenticacao.Models.DTO;
using apiAutenticacao.Models.Response;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using static BCrypt.Net.BCrypt;



namespace apiAutenticacao.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseLogin> Login(LoginDTO dadosUsuario)
        {

            Usuario? usuarioEncontrado = await _context.Usuarios.FirstOrDefaultAsync(usuario => usuario.Email == dadosUsuario.Email);

            if (usuarioEncontrado != null)
            {
                bool isValidPassword = Verify(dadosUsuario.Senha, usuarioEncontrado.Senha);

                if (isValidPassword)
                {
                    return new ResponseLogin
                    {
                        Erro = false,
                        Message = " Login realizado com sucesso ",
                        Usuario = usuarioEncontrado
                    };
                }
                return new ResponseLogin
                {
                    Erro = true,
                    Message = " Login não realizado. email ou senha incorretos "
                };
            }
            return new ResponseLogin
            {
                Erro = true,
                Message = " Usuario não encontrado "
            };
        }

        public async Task<ResponseCadastro> CadastrarUsuarioAsync(CadastroUsuarioDTO dadosUsuario)
        {
            Usuario? usuarioExistente = await _context.Usuarios.
               FirstOrDefaultAsync(usuario => usuario.Email == dadosUsuario.Email);

            if (usuarioExistente != null)
            {
                return new ResponseCadastro
                {

                    Erro = true,
                    Message = " Este email ja esta cadastradao no sistema"


                };


            }
            Usuario Usuario = new Usuario
            {
                Nome = dadosUsuario.Nome,
                Email = dadosUsuario.Email,
                Senha = HashPassword(dadosUsuario.Senha),
                ConfirmarSenha = dadosUsuario.ConfirmarSenha

            };
            _context.Usuarios.Add(Usuario);
            await _context.SaveChangesAsync();

            return new ResponseCadastro
            {
                Erro = false,
                Message = " Usuario Cadastrado com sucesso",
                Usuario = Usuario

            };
        }

        public async Task<ResponseNovaSenha> AlterarSenhaAsync(AlterarSenhaDTO dadosUsuario)
        {
            Usuario? usuarioExistente = await _context.Usuarios.
              FirstOrDefaultAsync(usuario => usuario.Email == dadosUsuario.Email);

            if (usuarioExistente != null)
            {
                return new ResponseNovaSenha
                {
                    Erro = false,
                    Message = " Usuario existe!"
                };
            }

            bool isValidPassword = Verify(dadosUsuario.SenhaAtual, usuarioExistente.Senha);

            if (isValidPassword)
            {
                return new ResponseNovaSenha
                {
                    Erro = true,
                    Message = "Senha alterada com sucesso "
                };
              }

            return new ResponseNovaSenha
            {
                Erro = false,
                Message = " Senha incorreta ",
                Usuario = usuarioExistente
            };
        }

    }

}

















