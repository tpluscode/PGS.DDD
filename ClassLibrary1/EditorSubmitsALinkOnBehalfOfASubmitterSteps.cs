using System;
using System.Linq;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace ClassLibrary1
{
    [Binding]
    public class EditorSubmitsALinkOnBehalfOfASubmitterSteps
    {
        private string linkUrl;
        private string linkDescritpion;
        private SubmitterId submitterId;

        [Given(@"an URL '(.*)'")]
        public void GivenAnURL(string url)
        {
            linkUrl = url;
        }
        
        [Given(@"a description '(.*)'")]
        public void GivenADescription(string description)
        {
            linkDescritpion = description;
        }

        [Given(@"Submitter is '(.*)'")]
        public void GivenSubmitterIs(string submitterId)
        {
            this.submitterId = new SubmitterId(submitterId);
        }

        [When(@"Submitter submits a Link")]
        public void WhenEditorAddsALinkOnBehalfOfSubmitter()
        {
            var linkSubmitted = new LinkSubmitted(new Uri(linkUrl), linkDescritpion, submitterId);
            ScenarioContext.Current.Set(linkSubmitted);
        }

        [Then(@"Link must have been submitted")]
        public void ThenLinkMustHaveBeenSubmitted()
        {
            var actualOutput = ScenarioContext.Current.Get<LinkSubmitted>();

            Assert.That(actualOutput, Is.Not.Null);
        }

        [Then(@"submitted Link contains data")]
        public void ThenSubmittedLinkContainsData(Table table)
        {
            var actualOutput = ScenarioContext.Current.Get<LinkSubmitted>();
            var expectedOutput = table.Rows.Select(
                row => new LinkSubmitted(new Uri(row["Url"]), row["Description"], new SubmitterId(row["SubmitterId"]))).Single();

            Assert.That(actualOutput.Url, Is.EqualTo(expectedOutput.Url));
            Assert.That(actualOutput.SubmitterId, Is.EqualTo(expectedOutput.SubmitterId));
            Assert.That(actualOutput.Description, Is.EqualTo(expectedOutput.Description));
        }
    }

    public class LinkSubmitted
    {
        public LinkSubmitted(Uri url, string description, SubmitterId submitterId)
        {
            Url = url;
            Description = description;
            SubmitterId = submitterId;
        }

        public Uri Url { get; private set; }

        public SubmitterId SubmitterId { get; private set; }

        public string Description { get; private set; }
    }

    public struct SubmitterId
    {
        public SubmitterId(string submitterId)
        {
            Id = submitterId;
        }

        public string Id { get; }
    }
}
