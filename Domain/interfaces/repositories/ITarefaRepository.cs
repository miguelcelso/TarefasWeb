using Domain.models;

namespace Domain.interfaces.repositories
{
    public interface ITarefaRepository
    {
        Task<IEnumerable<Tarefa>> RetornaTarefas();
        Task<Tarefa> GetTarefa(int id);
    }
}
