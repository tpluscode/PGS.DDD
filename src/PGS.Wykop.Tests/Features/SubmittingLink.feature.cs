﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.42000
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace PGS.Wykop.Tests.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Editor submits a Link on behalf of a Submitter")]
    public partial class EditorSubmitsALinkOnBehalfOfASubmitterFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "SubmittingLink.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Editor submits a Link on behalf of a Submitter", "", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Submitter submits a Link")]
        [NUnit.Framework.TestCaseAttribute("http://pgs-soft.com", "this is a company I work for", "wmalara", null)]
        [NUnit.Framework.TestCaseAttribute("http://pgs-soft.com", "", "wmalara", null)]
        [NUnit.Framework.TestCaseAttribute("https://pgs-soft.com", "", "wmalara", null)]
        public virtual void SubmitterSubmitsALink(string uRL, string description, string submitterId, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Submitter submits a Link", exampleTags);
#line 3
this.ScenarioSetup(scenarioInfo);
#line 4
 testRunner.Given("The date is \'2015-10-10\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 5
 testRunner.When(string.Format("Submitter \'{0}\' submits a Link \'{1}\' described \'{2}\'", submitterId, uRL, description), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "SubmitterId",
                        "Url",
                        "Description",
                        "DateSubmitted"});
            table1.AddRow(new string[] {
                        string.Format("{0}", submitterId),
                        string.Format("{0}", uRL),
                        string.Format("{0}", description),
                        "2015-10-10"});
#line 6
 testRunner.Then("Link should have been submitted", ((string)(null)), table1, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Submitter submits a non-HTTP Link")]
        [NUnit.Framework.TestCaseAttribute("http://localhost", null)]
        [NUnit.Framework.TestCaseAttribute("http://tpluskiewicz", null)]
        [NUnit.Framework.TestCaseAttribute("ssh://www.pgs-soft.com/git", null)]
        [NUnit.Framework.TestCaseAttribute("ftp://wcss.wroc.pl", null)]
        [NUnit.Framework.TestCaseAttribute("/a/relative/uri", null)]
        public virtual void SubmitterSubmitsANon_HTTPLink(string uRL, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Submitter submits a non-HTTP Link", exampleTags);
#line 15
this.ScenarioSetup(scenarioInfo);
#line 16
 testRunner.When(string.Format("Submitter \'wmalara\' submits a Link \'{0}\'", uRL), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 17
 testRunner.Then("Link should not have been submitted", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion