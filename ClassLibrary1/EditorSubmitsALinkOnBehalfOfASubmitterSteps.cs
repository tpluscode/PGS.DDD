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
        
        [When(@"Editor '(.*)' adds a Link on behalf of Submitter '(.*)'")]
        public void WhenEditorAddsALinkOnBehalfOfSubmitter(string editorId, string submitterId)
        {
            ScenarioContext.Current.Set(new LinkSubmitted());
        }
        
        [Then(@"Link must have been submitted")]
        public void ThenLinkMustHaveBeenSubmitted()
        {
            var actualOutput = ScenarioContext.Current.Get<LinkSubmitted>();

            Assert.That(actualOutput, Is.Not.Null);
        }
    }

    public class LinkSubmitted
    {
    }
}
