namespace Domain.interfaces.messagebrokers
{
    public interface IMensagemService
    {
        Task LerMensagem(Func<string, Task> action);
        Task EnviarMensagem(string message);
    }
}
