using Domain.dtos;
using Domain.interfaces.services;
using Microsoft.AspNetCore.Mvc;
using TarefasWeb.Controllers;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController(ITarefaService tarefaService, ILogger<WeatherForecastController> logger) : ControllerBase
    {
        private readonly ITarefaService _tarefaService = tarefaService ?? throw new ArgumentNullException(nameof(tarefaService));
        private readonly ILogger<WeatherForecastController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<TarefaRetornoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TarefaRetornoDto>>> Get()
        {
            var resultado = await _tarefaService.RetornaTarefas();

            return resultado == null || !resultado.Any() ? NotFound(): Ok(resultado);
        }
        [HttpGet("{id}/tarefa")]
        [ProducesResponseType(typeof(TarefaRetornoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TarefaRetornoDto>> Get(int id)
        {
            var resultado = await _tarefaService.GetTarefa(id);
            return resultado == null ? NotFound() : Ok(resultado);
        }

        [HttpPost]
        [ProducesResponseType(typeof(TarefaRetornoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TarefaRetornoDto>> CreateTarefa(TarefaCreateDto tarefa)
        {
            await _tarefaService.CreateTarefa(tarefa);
            return Ok();
        }

        [HttpPut("{id}/{status}")]
        [ProducesResponseType(typeof(TarefaRetornoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TarefaRetornoDto>> UpdateTarefa(int id, char status)
        {
            await _tarefaService.AtualizarStatusTarefa(id, status);
            return Ok();
        }
    }
}
