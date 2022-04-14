using Microsoft.AspNetCore.Mvc;
using CadastrarTarefas.WebApp.Models;
using CadastrarTarefas.Core.Commands;
using CadastrarTarefas.Services.Handlers;
using CadastrarTarefas.Infrastructure;

namespace CadastrarTarefas.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {

        [HttpPost]
        public IActionResult EndpointCadastraTarefa(CadastraTarefaVM model)
        {
            //var cmdObtemCateg = new ObtemCategoriaPorId(model.IdCategoria);
            //var categoria = new ObtemCategoriaPorIdHandler().Execute(cmdObtemCateg);
            //if (categoria == null)
            //{
            //    return NotFound("Categoria não encontrada");
            //}

            //var comando = new CadastraTarefa(model.Titulo, categoria, model.Prazo);
            //var handler = new CadastraTarefaHandler(_repo);
            //handler.Execute(comando);
            return Ok();
        }
    }
}