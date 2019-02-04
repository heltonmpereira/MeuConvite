using MeuConvite.Definicao.Interface;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeuConvite.Definicao.Entidade
{
    [Table("Presente")]
    public class Presente : IEntidade<int>
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public bool EhNecessario { get; set; }

        public override string ToString()
        {
            return $"Presente: {Id}-{Titulo}";
        }
    }
}
