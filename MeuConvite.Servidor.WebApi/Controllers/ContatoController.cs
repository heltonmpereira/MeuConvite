using System.Collections.Generic;
using System.Linq;
using MeuConvite.Definicao.Entidade;
using MeuConvite.Modelo.Persistencia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeuConvite.Servidor.WebApi.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Contato>> Get([FromServices] ContatoDao dao)
        {
            var retorno = dao.Navegar();
            return retorno.ToList();
        }
        [HttpGet("{id}")]
        public ActionResult<Contato> Get(
            int id,
            [FromServices] ContatoDao dao)
        {
            return dao.BuscaId(id);
        }

        [HttpPost]
        public void Post(
            [FromBody] Contato obj,
            [FromServices] ContatoDao dao)
        {
            dao.Incluir(obj);
        }
        [HttpPut("{id}")]
        public void Put(
            [FromBody] Contato obj,
            [FromServices] ContatoDao dao)
        {
            dao.Alterar(obj);
        }
        [HttpDelete("{id}")]
        public void Delete(
            int id,
            [FromServices] ContatoDao dao)
        {
            dao.Deletar(id);
        }
    }
}