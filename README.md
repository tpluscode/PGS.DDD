# PGS.DDD

Base libraries to start with CQRS and Event Sourcing

## About

The solution consists of a number of projects, each published as a NuGet package on myget (https://www.myget.org/F/pgs-ddd/api/v2).

It should be ready to build out-of-the box. To work with the dependencies [Paket](http://fsprojects.github.io/Paket/) is used. 
It is best handled from the command line. There is a handy [VS plugin too](https://visualstudiogallery.msdn.microsoft.com/ce104917-e8b3-4365-9490-8432c6e75c36).

### PGS.DDD.Domain

This library should be referenced by the business domain. This is there **Aggregate Roots**, **Entities** and **Domain Events** live.

To create an event-sourced **AR**, just inherit the `AggregateRoot<TId>` class.

``` c#
public class BusinessPerson : AggregateRoot<PersonId>
{ 
  private CompanyId _employer;

  public BusinessPerson(PersonId id) : base(id) { }

  public void StartWorkAt(CompanyId company)
  {
    // maybe some buisiness checks here to ensure consistent state?
  
    // push event
    Handle(StartedWorkingAt(company));
  }
  
  // each event is handled in a OnX method
  // it should be private or protected
  private void OnStartedWorkingAt(StartedWorkingAt ev)
  {
    _employer = ev.CompanyId;
  }
  
  // keeping event classes nested in their respective Aggregate Root
  // helps prevent name clashes
  public class StartedWorkingAt : DomainEvent
  {
    public StartedWorkingAt(CompanyId company)
    {
    }
  }
}
```

### PGS.DDD.Application

This one should be referenced by Application services layer and contains base interfaces for their implementation:

* `IEventPublisher`, `IEventHandler` - for working with domain events
* `IServiceBus` - combines both above
* `IIdConverter<TId>` - to convert domain ids into event stream identifiers

### PGS.DDD.Data.EventSourced

Implementation of an in-memory event store and a **repository** backed by events persisted to hard storage

### PGS.DDD.Data.NEventStore

An implementation of event store using [NEventStore](https://github.com/NEventStore/NEventStore).

### PGS.DDD.ServiceBus

Basic, in-memory implementation of `IServiceBus`.


