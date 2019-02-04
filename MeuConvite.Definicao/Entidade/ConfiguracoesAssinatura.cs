using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace MeuConvite.Definicao.Entidade
{
    public class ConfiguracoesAssinatura
    {
        public SecurityKey Chave { get; }
        public SigningCredentials Credenciais { get; }

        public ConfiguracoesAssinatura()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Chave = new RsaSecurityKey(provider.ExportParameters(true));
            }

            Credenciais = new SigningCredentials(Chave, SecurityAlgorithms.RsaSha256Signature);
        }
    }
}
