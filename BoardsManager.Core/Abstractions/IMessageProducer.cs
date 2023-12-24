namespace BoardsManager.Core.Abstractions
{
    public interface IMessageProducer
    {
        void Send<T> (T message);
    }
}