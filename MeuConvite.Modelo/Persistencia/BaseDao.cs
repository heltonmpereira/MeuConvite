using MeuConvite.Definicao.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MeuConvite.Modelo.Persistencia
{
    public abstract class BaseDao<T, TIPO_ID> : IDao<T, TIPO_ID>
        where T : class, IEntidade<TIPO_ID>
    {
        private DbContext _contexto;
        private DbSet<T> Tabela => _contexto.Set<T>();

        public BaseDao(DbContext contexto)
        {
            _contexto = contexto;
        }

        public T Deletar(TIPO_ID id)
        {
            var obj = Tabela.Find(id);
            Tabela.Remove(obj);
            _contexto.SaveChanges();

            return obj;
        }
        public T Alterar(T obj)
        {
            Tabela.Update(obj);
            _contexto.SaveChanges();

            return obj;
        }
        public T Incluir(T obj)
        {
            Tabela.Add(obj);
            _contexto.SaveChanges();

            return obj;
        }

        public T BuscaId(TIPO_ID id)
        {
            var resultado = Tabela.Find(id);
            return resultado;
        }
        public ICollection<T> Navegar()
        {
            return Tabela.ToList();
        }
    }
}
