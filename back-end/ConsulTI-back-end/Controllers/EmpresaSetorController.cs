using ConsulTI_back_end.Entities;
using ConsulTI_back_end.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsulTI_back_end.Controllers
{
    [Route("api/empresasetor")]
    [ApiController]
    public class EmpresaSetorController : ControllerBase
    {
        private readonly Services.EmpresaServices _empresaServices;
        private readonly Services.SetorServices _setorServices;
        private readonly Services.EmpresaSetorServices _empresasetorServices;

        public EmpresaSetorController(Services.EmpresaServices empresaServices, Services.SetorServices setorServices, Services.EmpresaSetorServices empresasetorServices)
        {
            _setorServices = setorServices;
            _empresaServices = empresaServices;
            _empresasetorServices = empresasetorServices;
        }

        [HttpPost("/api/vincular")]
        public IActionResult Criar([FromBody] ViewModel.EmpresaSetorCriarViewModel empresasetorVM)
        {
            Entities.EmpresaSetor empresasetor = new Entities.EmpresaSetor();
            empresasetor.empresa_id = empresasetorVM.empresa_id;
            empresasetor.setor_id = empresasetorVM.setor_id;

            var sucesso = _empresasetorServices.VincularEmpresaSetor(empresasetor.empresa_id, empresasetor.setor_id);

            if (!sucesso)
            {
                return UnprocessableEntity();
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet("/api/getvincempresas/{id}")] //Saber quais setores estão vinculados à determinada empresa
        public IActionResult ObterEmpresas(int id)
        {
            var empresav = _empresasetorServices.ObterEmpresasId(id);
            foreach(var item in empresav)
            {
                item.empresa = _empresaServices.Obter(id);
                item.setor = _setorServices.Obter(item.setor_id);
            }

            if (empresav == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(empresav);
            }
        }

        [HttpGet("/api/getvincsetores/{id}")] //Saber quais empresas estão vinculadas à determinado setor
        public IActionResult ObterSetores(int id)
        {
            var setorv = _empresasetorServices.ObterSetorId(id);
            foreach (var item in setorv)
            {
                item.empresa = _empresaServices.Obter(item.empresa_id);
                item.setor = _setorServices.Obter(id);
            }

            if (setorv == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(setorv);
            }
        }
    }
}
