using MeuConvite.Definicao.Interface;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeuConvite.Definicao.Entidade
{
    [Table("Configuracao")]
    public class Configuracao : IEntidade<string>
    {
        [Column("Chave")]
        public string Id { get; set; }
        public string Valor { get; set; }
        public string Secao { get; set; }
        public string Descricao { get; set; }

        public override string ToString()
        {
            return $"{Id}={Valor}";
        }
    }
}
