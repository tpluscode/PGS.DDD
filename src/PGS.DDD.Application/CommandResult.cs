using PGS.DDD.Domain;

namespace PGS.DDD.Application
{
    public class CommandResult
    {
        private CommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
            Events = new DomainEvent[0];
        }

        public bool IsSuccess { get; private set; }

        public DomainEvent[] Events { get; private set; }

        public static CommandResult Success(params DomainEvent[] domainEvents)
        {
            return new CommandResult(true)
            {
                Events = domainEvents
            };
        }

        public static CommandResult Failure()
        {
            return new CommandResult(false);
        }
    }
}
