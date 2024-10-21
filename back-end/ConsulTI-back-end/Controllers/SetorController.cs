using ConsulTI_back_end.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsulTI_back_end.Controllers
{
    [Route("api/setor")]
    [ApiController]
    public class SetorController : ControllerBase
    {
        private readonly Services.SetorServices _setorServices;

        public SetorController(Services.SetorServices setorServices)
        {
            _setorServices = setorServices;
        }

        [HttpPost]
        public IActionResult Criar([FromBody] ViewModel.SetorCriarViewModel setorVM)
        {
            Entities.Setor setor = new Entities.Setor();
            setor.descricao = setorVM.descricao;

            var sucesso = _setorServices.Salvar(setor);

            if (!sucesso)
            {
                return UnprocessableEntity();
            }
            else
            {
                setor = _setorServices.Obter(setor.id);
                return Created($"/api/Setor/{setor.id}", setor);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var setor = _setorServices.Obter(id);

            if (setor == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(new
                {
                    id = setor.id,
                    descricao = setor.descricao
                });
            }
        }

        [HttpGet]
        public IActionResult ObterTodos()
        {
            var setors = _setorServices.ObterTodos();

            if (setors == null)
            {
                return UnprocessableEntity();
            }
            else
            {
                return Ok(setors);
            }
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarSetor([FromBody] SetorModificarViewModel modificado, int id)
        {
            try
            {
                var setor = _setorServices.Obter(id);
                if (setor == null)
                {
                    return NotFound();
                }
                else
                {
                    if (modificado.descricao != "") { setor.descricao = modificado.descricao; } //if Ternário
                    var sucesso = _setorServices.Atualizar(setor);
                    if (sucesso)
                    {
                        return Ok(new { setor, modified = true });
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    }
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("atualizar/{id}")]
        public IActionResult AtualizarSetorCORS([FromBody] SetorModificarViewModel modificado, int id)
        {

            try //CORS não estava deixando eu realizar a operação de PUT, por isso criei um post para atualizar
            {
                var setor = _setorServices.Obter(id);
                if (setor == null)
                {
                    return NotFound();
                }
                else
                {
                    if (modificado.descricao != "") { setor.descricao = modificado.descricao; } //if Ternário
                    var sucesso = _setorServices.Atualizar(setor);
                    if (sucesso)
                    {
                        return Ok(new { setor, modified = true });
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    }
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarSetor(int id)
        {
            try
            {
                var setor = _setorServices.Obter(id);
                var sucesso = _setorServices.Deletar(id);
                if (sucesso)
                {
                    return Ok(new
                    {
                        setor,
                        deleted = true
                    });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
