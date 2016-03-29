using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using PGS.Wykop.Domain.Links;
using PGS.Wykop.ReadModel.WeeksLinks;
using TechTalk.SpecFlow;

namespace PGS.Wykop.Tests.ReadModels
{
    [Binding]
    public class WeeksLinksSteps
    {
        private readonly ISubmitterDetailsService _submitterDetailsService;
        private readonly WeeksLinks _weeksLinks;

        public WeeksLinksSteps()
        {
            _submitterDetailsService = A.Fake<ISubmitterDetailsService>();
            _weeksLinks = new WeeksLinks(_submitterDetailsService, new FakeEntityContext());
        }

        [Given(@"Submitter '(.*)' has full name '(.*)'")]
        public void GivenSubmitterHasFullName(SubmitterId submitterId, string fullName)
        {
            A.CallTo(() => _submitterDetailsService.GetFullNameOf(submitterId)).Returns(fullName);
        }

        [When(@"Link has been submitted")]
        public void WhenLinkHasBeenSubmitted(LinkSubmitted linkSubmitted)
        {
            _weeksLinks.AddLink(linkSubmitted);
        }
        
        [Then(@"Following link should have been added")]
        public void ThenLinkShouldHaveBeenAdded(WeeksLink weeksLink)
        {
            _weeksLinks[weeksLink.Week].Single().ShouldBeEquivalentTo(weeksLink);
        }

        [StepArgumentTransformation]
        public WeeksLink TableToLinkSubmittedEvent(Table table)
        {
            return (from row in table.Rows
                    select new WeeksLink
                    {
                        Url = row["Url"],
                        Week = int.Parse(row["Week"]),
                        Description = row["Description"],
                        SubmitterFullName = row["SubmitterFullName"]
                    }).Single();
        }
    }

    public class FakeEntityContext : IEntityContext
    {
        public IDbSet<WeeksLink> Links { get; } = new FakeDbSet.InMemoryDbSet<WeeksLink>();

        public int SaveChanges()
        {
            return 0;
        }
    }
}
