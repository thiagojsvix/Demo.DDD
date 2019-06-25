namespace Demo.DDD.Shared.Commands
{
    public interface ICommandResult
    {
        bool Sucess { get; set; }
        string Message { get; set; }
    }
}
