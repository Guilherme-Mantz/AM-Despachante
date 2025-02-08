using MediatR;

namespace AMDespachante.Domain.Core.Message
{
    public class Event : Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }

    }
}
