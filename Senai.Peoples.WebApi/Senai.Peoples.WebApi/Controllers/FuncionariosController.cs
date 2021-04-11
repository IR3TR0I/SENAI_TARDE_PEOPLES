using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Peoples.WebApi.Domains;
using Senai.Peoples.WebApi.InterFaces;
using Senai.Peoples.WebApi.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Senai.Peoples.WebApi.Controllers
{
    public class FuncionariosController
    {
        [Produces("application/json")]

        // /api/Funcionarios
        [Route("api/[controller]")]
        [ApiController]
        public class FuncionarioController : ControllerBase
        {
            private IFuncionariosRepository Funcionario { get; set; }

            public FuncionarioController()
            {
                Funcionario = new FuncionarioRepository();
            }

            [HttpGet]
            public IActionResult ListarTodos()
            {
                List<FuncionariosDomain> FunLista = Funcionario.ListarTodos();
                return Ok(FunLista);
            }

            [HttpPost]
            public IActionResult Cadastrar(FuncionariosDomain Funcionarios)
            {
                Funcionario.Cadastrar(Funcionarios);
                return StatusCode(201);
            }

            [HttpDelete("{Id}")]
            public IActionResult Deletar(int id)
            {
                Funcionario.Deletar(id);
                return StatusCode(204);
            }

            [HttpGet("{Id}")]
            public IActionResult BuscarId(int id)
            {
                FuncionariosDomain FunBuscado = Funcionario.BuscarPorId(id);

                if (FunBuscado == null)
                {
                    return NotFound("Nenhum funcionario encontrado!");
                }

                return Ok(FunBuscado);
            }
            [HttpPut("{Id}")]
            public IActionResult PutIdUrl(int id, FuncionariosDomain FunNovo)
            {
                FuncionariosDomain FunBuscado = Funcionario.BuscarPorId(id);

                if (FunBuscado == null)
                {
                    return NotFound("Erro ao atualizar o Funcionario");
                }

                try
                {
                    Funcionario.AtualizarIdUrl(id, FunNovo);

                    return NoContent();
                }
                catch (Exception codErro)
                {
                    return BadRequest(codErro);
                }

            }
            [HttpPut]
            public IActionResult UpdateIdBody(FuncionariosDomain FunNovo)
            {
                FuncionariosDomain FunBuscado = Funcionario.BuscarPorId(FunNovo.IdFuncionario);

                if (FunBuscado == null)
                {
                    return BadRequest("Erro ao atualizar um funcionario!");
                }

                try
                {
                    Funcionario.AtualizarIdCorpo(FunNovo);
                    return NoContent();
                }
                catch (Exception codErro)
                {
                    return BadRequest(codErro);
                }
            }
        }
    }
}
