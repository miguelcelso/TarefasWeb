using Domain.dtos;

namespace Domain.interfaces.services
{
    public interface ITarefaService
    {
        Task<IEnumerable<TarefaRetornoDto>> RetornaTarefas();
        Task CreateTarefa(TarefaCreateDto tarefa);
        Task<TarefaRetornoDto> GetTarefa(int id);
        Task AtualizarStatusTarefa(int id, char status);
    }
}
