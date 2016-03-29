using System;
using NUnit.Framework;
using PGS.DDD.Domain;
using PGS.Wykop.Domain.Links;
using TechTalk.SpecFlow;

namespace PGS.Wykop.Tests.DomainEntities
{
    [Binding, Scope(Feature = "Link", Tag = "DomainEntities")]
    public class LinkSteps
    {
        private Link _link;

        [When(@"Link for '(.*)' is created by '(.*)' described '(.*)'")]
        public void WhenLinkForIsCreatedByDescribed(Uri linkUri, SubmitterId submitterId, string description)
        {
            _link = new Link(linkUri, submitterId, new Description(description));
        }

        [When(@"Link for '(.*)' is created by '(.*)' without description")]
        public void WhenLinkForIsCreatedByDescribed(Uri linkUri, SubmitterId submitterId)
        {
            WhenLinkForIsCreatedByDescribed(linkUri, submitterId, string.Empty);
        }

        [Then(@"Url should be '(.*)'")]
        public void ThenUrlShouldBe(Uri url)
        {
            Assert.That(_link.Url, Is.EqualTo(url));
        }
        
        [Then(@"Submitter should be '(.*)'")]
        public void ThenSubmitterShouldBe(SubmitterId submitter)
        {
            Assert.That(_link.Submitter, Is.EqualTo(submitter));
        }
        
        [Then(@"Description should be '(.*)'")]
        public void ThenDescriptionShouldBe(string description)
        {
            Assert.That(_link.Description.Text, Is.EqualTo(description));
        }
        
        [Then(@"Version number should be (.*)")]
        public void ThenVersionNumberShouldBe(int version)
        {
            Assert.That(((IAggregateRoot)_link).Version, Is.EqualTo(version));
        }
    }
}
