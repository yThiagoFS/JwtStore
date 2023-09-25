namespace Jwt.Core.Contexts.SharedContext.ValueObjects
{
    public class Message<T>
    {
        public T Data { get; internal set; }

        public Message(T data)
        {
            Data = data;
        }
    }
}
