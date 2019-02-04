using MeuConvite.Definicao.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeuConvite.Definicao.Entidade
{
    [Table("Convidado")]
    public class Convidado
    {
        public int ConviteId { get; set; }
        public Convite Convite { get; set; }

        public int ContatoId { get; set; }
        public Contato Contato { get; set; }

        public int PresenteId { get; set; }
        public Presente Sugestao { get; set; }

        public SituacaoConvite Situacao { get; set; }

        public override string ToString()
        {
            return ""
                + $"Convite: {ConviteId}-{Convite.Titulo}\n\t"
                + $"Contato: {ContatoId}-{Contato.Nome}\n\t"
                + $"Presente: {PresenteId}-{Sugestao.Titulo}";
        }
    }
}
