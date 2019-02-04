using MeuConvite.Definicao.Interface;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeuConvite.Definicao.Entidade
{
    [Table("Usuario")]
    public class Usuario : IEntidade<string>
    {
        [Column("Usuario")]
        public string Id { get; set; }
        public string Senha { get; set; }

        public override string ToString()
        {
            return $"Usuario: {Id}";
        }
    }
}
