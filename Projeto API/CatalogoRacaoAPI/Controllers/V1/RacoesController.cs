using Microsoft.AspNetCore.Http;
using CatalogoRacaoAPI.Exceptions;
using CatalogoRacaoAPI.InputModel;
using CatalogoRacaoAPI.Services;
using CatalogoRacaoAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoRacaoAPI.Controllers.V1
{ 
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RacoesController : ControllerBase
    {
        private readonly IRacaoService _racaoService;

        public RacoesController(IRacaoService racaoService)
        {
            _racaoService = racaoService;
        }

        /// <summary>
        /// Buscar todos as rações de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar as rações sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantidade">Indica a quantidade de registros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de rações</response>
        /// <response code="204">Caso não haja rações</response>   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RacaoViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var racao = await _racaoService.Obter(pagina, quantidade);

            if (racao.Count() == 0)
                return NoContent();

            return Ok(racao);
        }

        /// <summary>
        /// Buscar uma racao pelo seu Id
        /// </summary>
        /// <param name="idRacao">Id da ração buscada</param>
        /// <response code="200">Retorna a ração filtrada</response>
        /// <response code="204">Caso não haja ração com este id</response>   
        [HttpGet("{idJogo:guid}")]
        public async Task<ActionResult<RacaoViewModel>> Obter([FromRoute] Guid idRacao)
        {
            var racao = await _racaoService.Obter(idRacao);

            if (racao == null)
                return NoContent();

            return Ok(racao);
        }

        /// <summary>
        /// Inserir uma ração no catálogo
        /// </summary>
        /// <param name="racaoInputModel">Dados da ração a ser inseridos</param>
        /// <response code="200">Caso inserido com sucesso</response>
        /// <response code="422">Caso já exista uma com mesmo nome para a mesma marca</response>   
        [HttpPost]
        public async Task<ActionResult<RacaoViewModel>> InserirRacao([FromBody] RacaoInputModel racaoInputModel)
        {
            try
            {
                var racao = await _racaoService.Inserir(racaoInputModel);

                return Ok(racao);
            }
            catch (RacaoJaCadastradaException ex)
            {
                return UnprocessableEntity("Já existe uma ração com este nome para esta marca");
            }
        }

        /// <summary>
        /// Atualizar uma ração no catálogo
        /// </summary>
        /// /// <param name="idRacao">Id a ser atualizado</param>
        /// <param name="racaoInputModel">Novos dados para atualizar a ração indicada</param>
        /// <response code="200">Caso seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista  com este Id</response>   
        [HttpPut("{idRacao:guid}")]
        public async Task<ActionResult> AtualizarRacao([FromRoute] Guid idRacao, [FromBody] RacaoInputModel racaoInputModel)
        {
            try
            {
                await _racaoService.Atualizar(idRacao, racaoInputModel);

                return Ok();
            }
            catch (RacaoNaoCadastradaException ex)
            {
                return NotFound("Esta ração não existe!");
            }
        }

        /// <summary>
        /// Atualizar o preço de
        /// </summary>
        /// /// <param name="idracao">Id da ração ser atualizada</param>
        /// <param name="preco">Novo preço</param>
        /// <response code="200">Caos o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista uma com este Id</response>   
        [HttpPatch("{idRacao:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarRacao([FromRoute] Guid idRacao, [FromRoute] double preco)
        {
            try
            {
                await _racaoService.Atualizar(idRacao, preco);

                return Ok();
            }
            catch (RacaoNaoCadastradaException ex)
            {
                return NotFound("Esta ração não existe!");
            }
        }

        /// <summary>
        /// Excluir uma ração
        /// </summary>
        /// /// <param name="idRacao">Id da ração ser excluída</param>
        /// <response code="200">Caso o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista com este Id</response>   
        [HttpDelete("{idRacao:guid}")]
        public async Task<ActionResult> ApagarRacao([FromRoute] Guid idRacao)
        {
            try
            {
                await _racaoService.Remover(idRacao);

                return Ok();
            }
            catch (RacaoNaoCadastradaException ex)
            {
                return NotFound("Esta ração não existe!");
            }
        }

    }
}