using MeuConvite.Definicao.Entidade;
using Microsoft.EntityFrameworkCore;

namespace MeuConvite.Modelo.Persistencia
{
    public class ContatoDao : BaseDao<Contato, int>
    {
        public ContatoDao(DbContext contexto) : base(contexto) { }
    }
}
