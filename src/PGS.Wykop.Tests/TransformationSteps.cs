using System;
using System.Globalization;
using System.Linq;
using NodaTime;
using PGS.Wykop.Domain.Links;
using TechTalk.SpecFlow;

namespace PGS.Wykop.Tests
{
    [Binding]
    public class TransformationSteps
    {
        [StepArgumentTransformation]
        public SubmitterId StringToSubmitterId(string id)
        {
            return new SubmitterId(id);
        }

        [StepArgumentTransformation]
        public Uri StringToSubmitterUri(string uri)
        {
            return new Uri(uri, UriKind.RelativeOrAbsolute);
        }

        [StepArgumentTransformation]
        public LinkSubmitted TableToLinkSubmittedEvent(Table table)
        {
            return (from row in table.Rows
                let url = new Uri(row["Url"])
                let descr = new Description(row["Description"])
                let submitter = new SubmitterId(row["SubmitterId"])
                let date = DateTime.ParseExact(row["DateSubmitted"], "yyyy-MM-dd", CultureInfo.InvariantCulture) // note: also a value type here?
                select new LinkSubmitted(new LinkId(), url, descr, submitter, Instant.FromDateTimeUtc(date.ToUniversalTime()))).Single();
        }
    }
}