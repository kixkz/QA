using System.Net;
using NUnit.Framework;
using TechTalk.SpecFlow.Infrastructure;

namespace GoRestSpecflow.Support
{
    [Binding]
    public sealed class Hooks
    {
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;
        private static ScenarioContext _scenarioContext;
        private TestContextContainer _testContainer;
        private readonly BaseConfig _baseConfig;

        public Hooks(ISpecFlowOutputHelper specFlowOutputHelper, TestContextContainer testContainer, BaseConfig baseConfig)
        {
            _specFlowOutputHelper = specFlowOutputHelper;
            _testContainer = testContainer;
            _baseConfig = baseConfig;
        }

        [BeforeScenario]
        public void TearUp()
        {
            //var example = _scenarioContext.ScenarioInfo.Tags.GetValue(1);
            _testContainer.HttpClient = new HttpClient();
        }

        [BeforeScenario("Authenticate")]
        public void Authenticate()
        {
            _testContainer.HttpClient.DefaultRequestHeaders.Add("Authorization", _baseConfig.HttpClientConfig.Token);
        }

        [BeforeScenario("Authenticate2")]
        public void Authenticate2()
        {
            _testContainer.HttpClient.DefaultRequestHeaders.Add("Authorization", _baseConfig.HttpClientConfig.Token2);
        }

        [AfterScenario]
        public void CleanUp()
        {
            _testContainer.HttpClient.DeleteAsync($"{_baseConfig.HttpClientConfig.BaseUrl}users/{_testContainer.UserId}");
            _specFlowOutputHelper.WriteLine("Delete user");
        }
    }
}
