using System;
using System.Linq;
using FakeItEasy;
using NodaTime;
using NUnit.Framework;
using PGS.DDD.Application;
using PGS.DDD.Domain;
using PGS.Wykop.Application.Links;
using PGS.Wykop.Domain.Links;
using TechTalk.SpecFlow;

namespace PGS.Wykop.Tests.Features
{
    [Binding]
    public class SubmittingLinkSteps
    {
        private readonly SubmittingLinkService _service;
        private readonly IClock _clock;

        public SubmittingLinkSteps()
        {
            _clock = A.Fake<IClock>();
            _service = new SubmittingLinkService(A.Fake<IRepository<Link, LinkId>>(), A.Fake<IServiceBus>(), _clock);
        }

        [Given(@"The date is '(.*)'")]
        public void GivenTheDateIs(DateTime dateTime)
        {
            A.CallTo(() => _clock.Now).Returns(Instant.FromDateTimeUtc(dateTime.ToUniversalTime()));
        }

        [When(@"Submitter '(.*)' submits a Link '(.*)' described '(.*)'")]
        public void WhenSubmitterSubmitsALinkDescribed(string submitterId, Uri linkUrl, string linkDescritpion)
        {
            SubmitLinkCommand cmd = new SubmitLinkCommand
            {
                Description = linkDescritpion,
                SubmitterId = submitterId,
                 Uri = linkUrl
            };
            var events = _service.SubmitLink(cmd);

            ScenarioContext.Current.Set(events);
        }

        [When(@"Submitter '(.*)' submits a Link '([^']*)'")]
        public void WhenSubmitterSubmitsALink(string submitterId, Uri linkUrl)
        {
            WhenSubmitterSubmitsALinkDescribed(submitterId, linkUrl, string.Empty);
        }

        [Then(@"Link should have been submitted")]
        public void ThenLinkShouldHaveBeenSubmitted(LinkSubmitted expectedOutput)
        {
            var actualOutput = ScenarioContext.Current.Get<CommandResult>().Events.OfType<LinkSubmitted>().Single();

            Assert.That(actualOutput, Is.Not.Null);

            Assert.That(actualOutput.Url, Is.EqualTo(expectedOutput.Url));
            Assert.That(actualOutput.SubmitterId, Is.EqualTo(expectedOutput.SubmitterId));
            Assert.That(actualOutput.Description, Is.EqualTo(expectedOutput.Description));
        }

        [Then(@"Link should not have been submitted")]
        public void ThenLinkShouldNotHaveBeenSubmitted()
        {
            var actualOutput = ScenarioContext.Current.Get<CommandResult>();

            Assert.That(actualOutput.IsSuccess, Is.False);
        }
    }
}
