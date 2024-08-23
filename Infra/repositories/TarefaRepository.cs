using Domain.interfaces.repositories;
using Domain.models;
using Infra.contexts;
using Microsoft.EntityFrameworkCore;

namespace Infra.repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly ContextoTarefa _contextoTarefa;

        public TarefaRepository(ContextoTarefa contextoTarefa) => _contextoTarefa = contextoTarefa ?? throw new ArgumentNullException(nameof(contextoTarefa));

        public async Task<Tarefa> GetTarefa(int id)
        {
            var resultado = await _contextoTarefa.Tarefas.Where(x => x.Id == id).FirstOrDefaultAsync();
            return resultado;
        }

        public async Task<IEnumerable<Tarefa>> RetornaTarefas()
        {
            var resultado = await _contextoTarefa.Tarefas.ToListAsync();
            return resultado;
        }
    }
}
 