using System.Collections.Generic;

namespace MeuConvite.Definicao.Interface
{
    public interface IDao<T, TIPO_ID>
    {
        T Deletar(TIPO_ID id);
        T Alterar(T obj);
        T Incluir(T obj);

        T BuscaId(TIPO_ID id);
        ICollection<T> Navegar();
    }
}
