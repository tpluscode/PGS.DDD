using PGS.DDD.Domain;

namespace PGS.Wykop.Tests
{
    public class TestPersonAR : AggregateRoot<int>
    {
        public TestPersonAR(int id) : base(id)
        {
        }

        public string Name { get; set; }

        public string Surname { get; set; }

        public void SetName(string name)
        {
            Handle(new NameChanged(name));
        }

        private void OnNameChanged(NameChanged ev)
        {
            Name = ev.Name;
        }

        private void OnSurnameChanged(SurnameChanged ev)
        {
            Surname = ev.Surname;
        }

        public class NameChanged : DomainEvent
        {
            public NameChanged(string name)
            {
                Name = name;
            }

            public string Name { get; set; }
        }

        public class SurnameChanged : DomainEvent
        {
            public SurnameChanged(string surname)
            {
                Surname = surname;
            }

            public string Surname { get; set; }
        }
    }
}