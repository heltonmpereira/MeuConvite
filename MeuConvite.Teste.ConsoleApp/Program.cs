using MeuConvite.Definicao.Entidade;
using MeuConvite.Definicao.Interface;
using MeuConvite.Modelo.Contexto;
using MeuConvite.Modelo.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeuConvite.Teste.ConsoleApp
{
    class Program
    {
        private static MeuConviteContexto Contexto()
        {
            var opt = new DbContextOptionsBuilder();
            //opt.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MeuConviteDb;Integrated Security=True;");
            opt.UseInMemoryDatabase("MeuConviteDb");

            return new MeuConviteContexto(opt.Options);
        }
        private static void TesteDao<T, TIPO_ID>(BaseDao<T, TIPO_ID> dao)
            where T : class, IEntidade<TIPO_ID>
        {
            var obj = (T)Activator.CreateInstance(typeof(T));
            var prop = obj
                .GetType()
                .GetProperties()
                .FirstOrDefault(w => w.Name == "Nome" || w.Name == "Titulo");

            if (prop != null)
                prop.SetValue(obj, $"Teste {obj.GetType().Name}");

            Console.WriteLine();
            Console.WriteLine(new string('-', 40));
            Console.WriteLine(obj.ToString());
            dao.Incluir(obj);
            Console.WriteLine($"Após inclusao\n{obj} - {dao.Navegar().Count} Itens");

            if (prop != null)
                prop.SetValue(obj, $"Teste {obj.GetType().Name} alterado");
            dao.Alterar(obj);
            Console.WriteLine($"Após alteracao\n{obj} - {dao.Navegar().Count} Itens");

            dao.Deletar(obj.Id);
            Console.WriteLine($"Após delecao\n{obj} - {dao.Navegar().Count} Itens");
        }
        private static void TesteContexto()
        {
            var ctx = Contexto();
            TesteDao(new ContatoDao(ctx));
            TesteDao(new ConviteDao(ctx));
            TesteDao(new PresenteDao(ctx));
            TesteDao(new ConfiguracaoDao(ctx));
            TesteDao(new UsuarioDao(ctx));

            TesteConvidado();
        }
        private static void TesteConvidado()
        {
            var ctx = Contexto();

            var contatos = new List<Contato>
            {
                new Contato {Nome = "Contato 2", Email = "contato1@teste.com", Celular = "+55 11 9.1122 3344" },
                new Contato {Nome = "Contato 3", Email = "contato2@teste.com", Celular = "+55 11 9.3344 1122" },
                new Contato {Nome = "Contato 4", Email = "contato3@teste.com", Celular = "+55 11 9.2211 4433" },
            };
            ctx.Contatos.AddRange(contatos);
            ctx.SaveChanges();

            var presentes = new List<Presente>
            {
                new Presente {Titulo = "Presente 2", EhNecessario = true },
                new Presente {Titulo = "Presente 3", EhNecessario = false },
                new Presente {Titulo = "Presente 4", EhNecessario = true },
                new Presente {Titulo = "Presente 5", EhNecessario = false },
            };
            ctx.Presentes.AddRange(presentes);
            ctx.SaveChanges();

            var convites = new List<Convite>
            {
                new Convite {Titulo = "Convite 2", Mensagem = "Mensagem convite 1", Convidados= new List<Convidado>(){
                    new Convidado { Contato = contatos[0], Sugestao = presentes[1]},
                    new Convidado { Contato = contatos[1], Sugestao = presentes[3]}} },
                new Convite {Titulo = "Convite 3", Mensagem = "Mensagem convite 2", Convidados= new List<Convidado>(){
                    new Convidado { Contato = contatos[1], Sugestao = presentes[1]},
                    new Convidado { Contato = contatos[2], Sugestao = presentes[3]}}},
            };
            ctx.Convites.AddRange(convites);
            ctx.SaveChanges();

            ExibeConvite(convites[1]);
            var convite = ctx.Convites.Find(convites[1].Id);
            convite.Convidados.Add(new Convidado { Contato = contatos[0], Sugestao = presentes[0] });
            ctx.Convites.Update(convite);
            ctx.SaveChanges();

            ExibeConvite(convite);

            ctx.Convites.RemoveRange(convites);
            ctx.Contatos.RemoveRange(contatos);
            ctx.Presentes.RemoveRange(presentes);
            ctx.SaveChanges();
        }
        private static void ExibeConvite(Convite convite)
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"Convite:{convite.Id}-{convite.Titulo}");
            Console.WriteLine($"Mensagem: {convite.Mensagem}");

            Console.WriteLine("Convidados:");
            foreach (var item in convite.Convidados)
            {
                Console.WriteLine($"\tContato: {item.Contato} | {item.Sugestao}");
            }
        }

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(args);
                TesteContexto();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType());
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}