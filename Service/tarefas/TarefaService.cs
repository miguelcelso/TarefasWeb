using AutoMapper;
using Domain.dtos;
using Domain.interfaces.messagebrokers;
using Domain.interfaces.repositories;
using Domain.interfaces.services;
using Domain.models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Service.tarefas
{
    public class TarefaService(IMapper mapper, ITarefaRepository tarefaRepository, IMensagemService mensagemService, ILogger<TarefaService> logger) : ITarefaService
    {
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        private readonly ITarefaRepository _tarefaRepository = tarefaRepository ?? throw new ArgumentNullException(nameof(tarefaRepository));
        private readonly IMensagemService _mensagemService = mensagemService ?? throw new ArgumentNullException(nameof(mensagemService));
        private readonly ILogger<TarefaService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
       
        public async Task AtualizarStatusTarefa(int id, char status)
        {
            var resultado = await GetTarefa(id);
            if(resultado == null)
            {
                _logger.LogInformation("Por favor verificar a tarefa pois o id: {id} não foi encontrado!", id);
                throw new Exception("Por favor verificar a tarefa, a mesma não foi encontrado!");
            }
            resultado.Status = status;
            var tarefaJson = JsonConvert.SerializeObject(resultado);
            try
            {
                _logger.LogInformation("Enviar tarefa para o rabbitmq, tarefa: {tarefa}", tarefaJson);
                await _mensagemService.EnviarMensagem(tarefaJson);
                _logger.LogInformation("Tarefa enviada com sucesso para o rabbitmq");
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao enviar Tarefa para o rabbitmq, mesagem: {mensagem} e trace: {trace}", ex.Message, ex.StackTrace);
                throw new Exception("Por favor verificar o funcionamento do rabbitmq");
            }
        }

        public async Task CreateTarefa(TarefaCreateDto tarefa)
        {
            var _tarefa = _mapper.Map<Tarefa>(tarefa);
            var tarefaJson = JsonConvert.SerializeObject(_tarefa);
            try
            {
                _logger.LogInformation("Enviar tarefa para o rabbitmq, tarefa: {tarefa}", tarefaJson);
                await _mensagemService.EnviarMensagem(tarefaJson);
                _logger.LogInformation("Tarefa enviada com sucesso para o rabbitmq");
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao enviar Tarefa para o rabbitmq, mesagem: {mensagem} e trace: {trace}", ex.Message, ex.StackTrace);
                throw new Exception("Por favor verificar o funcionamento do rabbitmq");
            }
          
        }

        public async Task<TarefaRetornoDto> GetTarefa(int id)
        {
            var resultado = await _tarefaRepository.GetTarefa(id);
            var retorno = _mapper.Map<TarefaRetornoDto>(resultado);
            return retorno;
        }

        public async Task<IEnumerable<TarefaRetornoDto>> RetornaTarefas()
        {
            var resultado = await _tarefaRepository.RetornaTarefas();
            var retorno = _mapper.Map<IEnumerable<TarefaRetornoDto>>(resultado);
            return retorno;
        }
    }
}
