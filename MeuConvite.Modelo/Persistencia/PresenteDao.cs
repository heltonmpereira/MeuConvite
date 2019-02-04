using MeuConvite.Definicao.Entidade;
using Microsoft.EntityFrameworkCore;

namespace MeuConvite.Modelo.Persistencia
{
    public class PresenteDao : BaseDao<Presente, int>
    {
        public PresenteDao(DbContext contexto) : base(contexto) { }
    }
}
