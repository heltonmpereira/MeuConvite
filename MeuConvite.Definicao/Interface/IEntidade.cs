using System.ComponentModel.DataAnnotations;

namespace MeuConvite.Definicao.Interface
{
    public interface IEntidade<T>
    {
        [Key]
        T Id { get; set; }
    }
}
