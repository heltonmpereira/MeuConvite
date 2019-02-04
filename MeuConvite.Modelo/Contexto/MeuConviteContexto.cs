using MeuConvite.Definicao.Entidade;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MeuConvite.Modelo.Contexto
{
    public class MeuConviteContexto : DbContext
    {
        public MeuConviteContexto(DbContextOptions options) : base(options) { }

        //Habilite esta rotina para executar as "migrations"
        //Add-Migration ["Nome_Migration"] -Project Servidor\MeuConvite.Modelo -StartupProject Servidor\MeuConvite.Modelo
        //public MeuConviteContexto() { }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MeuConviteDb;Integrated Security=True;");
        //    base.OnConfiguring(optionsBuilder);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(string)))
            {
                if (property.GetMaxLength() == null)
                    property.SetMaxLength(256);
            }

            modelBuilder.Entity<Convidado>().HasKey(cv => new { cv.ConviteId, cv.ContatoId });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Convite> Convites { get; set; }
        public DbSet<Presente> Presentes { get; set; }
        public DbSet<Convidado> Convidados { get; set; }
        public DbSet<Configuracao> Configuracoes { get; set; }
    }
}
