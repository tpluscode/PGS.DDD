using System;

namespace PGS.DDD.Domain
{
    public class DomainEvent
    {
        public DateTime Date { get; private set; }

        public DomainEvent()
        {
            Date = DateTime.Now;
        }
    }

    public interface IId
    {
        string ToUniqueString();
    }
}
