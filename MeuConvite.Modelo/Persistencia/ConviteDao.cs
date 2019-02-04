using MeuConvite.Definicao.Entidade;
using Microsoft.EntityFrameworkCore;

namespace MeuConvite.Modelo.Persistencia
{
    public class ConviteDao : BaseDao<Convite, int>
    {
        public ConviteDao(DbContext contexto) : base(contexto) { }
    }
}
