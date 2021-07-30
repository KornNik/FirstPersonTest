
namespace ExampleTemplate
{
    public interface IGameHandler
    {
        IGameHandler SetNext(IGameHandler nextHandler);
        object Handle(object request);
    }
}