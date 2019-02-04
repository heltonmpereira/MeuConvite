using MeuConvite.Definicao.Interface;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeuConvite.Definicao.Entidade
{
    [Table("Convite")]
    public class Convite : IEntidade<int>
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        [Column(TypeName = "text")]
        public string Mensagem { get; set; }

        public virtual ICollection<Convidado> Convidados { get; set; }

        public override string ToString()
        {
            return $"Convite: {Id}-{Titulo}";
        }
    }
}
