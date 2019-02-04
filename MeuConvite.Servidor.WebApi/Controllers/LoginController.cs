using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using MeuConvite.Definicao.Entidade;
using MeuConvite.Modelo.Persistencia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeuConvite.Servidor.WebApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public object Post(
            [FromBody] Usuario usuario,
            [FromServices] UsuarioDao usersDAO,
            [FromServices] ConfiguracoesAssinatura configuracoesAssinatura,
            [FromServices] ConfiguracoesToken configuracoesToken)
        {
            //var credenciaisValidas = false;
            var retorno = new
            {
                authenticated = false,
                message = "Falha ao autenticar"
            };

            if (usuario == null || string.IsNullOrWhiteSpace(usuario.Id)) return retorno;

            var userBase = usersDAO.BuscaId(usuario.Id);
            var credenciaisValidas = userBase?.Senha == usuario.Senha;
            if (!credenciaisValidas) return retorno;

            var identity = new ClaimsIdentity(
                new GenericIdentity(usuario.Id, "Login"),
                new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Id)
                }
            );

            var dataCriacao = DateTime.Now;
            var dataExpiracao = dataCriacao + TimeSpan.FromSeconds(configuracoesToken.Segundos);
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Issuer = configuracoesToken.Emissor,
                Audience = configuracoesToken.Espectador,
                SigningCredentials = configuracoesAssinatura.Credenciais,
                Subject = identity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            });
            var token = handler.WriteToken(securityToken);

            return new
            {
                authenticated = true,
                created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                message = "OK"
            };
        }

        [HttpPost("registrar")]
        public Usuario Registrar(
            [FromBody] Usuario usuario,
            [FromServices] UsuarioDao usersDAO)
        {
            return usersDAO.Incluir(usuario);
        }
    }
}