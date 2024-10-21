﻿using ConsulTI_back_end.Entities;
using ConsulTI_back_end.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsulTI_back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly Services.EmpresaServices _empresaServices;
        
        public EmpresaController(Services.EmpresaServices empresaServices)
        {
            _empresaServices = empresaServices;
        }

        [HttpPost]
        public IActionResult Criar([FromBody] ViewModel.EmpresaCriarViewModel empresaVM)
        {
            Entities.Empresa empresa = new Entities.Empresa();
            empresa.nome_fantasia = empresaVM.nome_fantasia;
            empresa.razao_social = empresaVM.razao_social;
            empresa.cnpj = empresaVM.cnpj;

            var sucesso = _empresaServices.Salvar(empresa);

            if (!sucesso)
            {
                return UnprocessableEntity();
            }
            else
            {
                empresa = _empresaServices.Obter(empresa.id);
                return Created($"/api/Empresa/{empresa.id}", empresa);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var empresa = _empresaServices.Obter(id);

            if (empresa==null)
            {
                return NotFound();
            }
            else
            {
                return Ok(empresa);
            }
        }

        [HttpGet]
        public IActionResult ObterTodos()
        {
            var empresas = _empresaServices.ObterTodos();

            if (empresas == null)
            {
                return UnprocessableEntity();
            }
            else
            {
                return Ok(empresas);
            }
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarEmpresa([FromBody]EmpresaModificarViewModel modificado ,int id)
        {
            try
            {
                var empresa = _empresaServices.Obter(id);
                if(empresa == null)
                {
                    return NotFound();
                }
                else
                {
                    if (modificado.nome_fantasia != "") { empresa.nome_fantasia = modificado.nome_fantasia; } //if Ternário
                    if (modificado.razao_social != "") { empresa.razao_social = modificado.razao_social; }
                    if (modificado.cnpj != "") { empresa.cnpj = modificado.cnpj; }

                    var sucesso = _empresaServices.Atualizar(empresa);
                    if (sucesso)
                    {
                        return Ok(new { empresa, modified = true });
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
        public IActionResult DeletarEmpresa(int id)
        {
            try
            {
                var empresa = _empresaServices.Obter(id);
                var sucesso = _empresaServices.Deletar(id);
                if (sucesso)
                {
                    return Ok(new
                    {
                        empresa,
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
