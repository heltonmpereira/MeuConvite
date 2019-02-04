using MeuConvite.Definicao.Entidade;
using Microsoft.EntityFrameworkCore;

namespace MeuConvite.Modelo.Persistencia
{
    public class ConfiguracaoDao : BaseDao<Configuracao, string>
    {
        public ConfiguracaoDao(DbContext contexto) : base(contexto) { }
    }
}
