using Demo.DDD.Shared.Commands;

namespace Demo.DDD.Shared.Handlers
{
    public interface IHandler<T> where T : Command
    {
        ICommandResult Handle(T command);
    }
}
