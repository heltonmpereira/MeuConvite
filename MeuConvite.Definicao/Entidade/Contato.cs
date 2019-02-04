using MeuConvite.Definicao.Interface;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeuConvite.Definicao.Entidade
{
    [Table("Contato")]
    public class Contato : IEntidade<int>
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        [MaxLength(30)]
        public string Celular { get; set; }

        public virtual ICollection<Convidado> Convidados { get; set; }

        public override string ToString()
        {
            return $"Contato: {Id} - {Nome}";
        }
    }
}
