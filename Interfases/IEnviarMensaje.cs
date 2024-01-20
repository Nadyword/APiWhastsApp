namespace TestAPIWhastasAppv2.Interfases
{
    public interface IEnviarMensaje
    {
        Task<bool> Execute(object model);
    }
}
