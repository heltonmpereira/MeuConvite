using MeuConvite.Definicao.Entidade;
using Microsoft.EntityFrameworkCore;

namespace MeuConvite.Modelo.Persistencia
{
    public class UsuarioDao : BaseDao<Usuario, string>
    {
        public UsuarioDao(DbContext contexto) : base(contexto) { }
    }
}
