using AMDespachante.Domain.Core.Message;

namespace AMDespachante.Domain.Core.DomainObjects
{
    public class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        private List<Event> _events;
        public IReadOnlyCollection<Event> Events => _events?.AsReadOnly();


        public void AddEvent(Event @event)
        {
            _events = _events ?? new List<Event>();
            _events.Add(@event);
        }
        public void ClearEvents()
        {
            _events?.Clear();
        }
        public void RemoveEvent(Event @event)
        {
            _events?.Remove(@event);
        }



        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }
        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }
        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }
        public override int GetHashCode()
        {
            return (GetType().GetHashCode() ^ 93) + Id.GetHashCode();
        }
        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }
    }
}
