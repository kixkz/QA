using System.Text;
using FluentAssertions.Execution;
using GoRestSpecflow.Support;
using Newtonsoft.Json;
using TechTalk.SpecFlow.Assist;

namespace GoRestSpecflow.StepDefinitions
{
    [Binding]
    public sealed class Steps
    {
        private HttpClient _httpClient;
        private readonly BaseConfig _baseConfig;
        private HttpResponseMessage _response;
        private GoRestUser _user;
        private TestContextContainer _context;

        public Steps(BaseConfig baseConfig, TestContextContainer context)
        {
            _baseConfig = baseConfig;
            _context = context;
        }

        [Given(@"I want to prepare a request")]
        public void GivenIWantToPrepareARequest()
        {
            //_httpClient.DefaultRequestHeaders.Add("Authorization", _baseConfig.HttpClientConfig.Token);
        }

        //[When(@"I get all users from the (.*) endpoint")]
        //public void WhenIGetAllUsersFromTheUsersEndpoint(string endpoint)
        //{
        //    _response = _context.HttpClient.GetAsync($"{_baseConfig.HttpClientConfig.BaseUrl}{endpoint}").Result;
        //}

        [Then(@"Response status code should be (.*)")]
        public void ThenResponseStatusCodeShouldBeOK(string statusCode)
        {
            _response.StatusCode.ToString().Should().Be(statusCode);
        }

        //[Then(@"the response should contains a list of users")]
        //public void ThenTheResponseShouldContainsAListOfUsers()
        //{
        //    var content = _response.Content.ReadAsStringAsync().Result;
        //    var expectedResponse = JsonConvert.DeserializeObject<List<GoRestUser>>(content);

        //    expectedResponse.Should().NotBeEmpty();
        //}

        [Given(@"I have the following user data")]
        public void GivenIHaveTheFollowingUserData(Table table)
        {
            _user = table.CreateInstance<GoRestUser>();
        }

        [When(@"I send a request to the (.*) endpoint")]
        public void WhenISendARequestToTheUsersEndpoint(string endpoint)
        {
            var request = JsonConvert.SerializeObject(_user);
            var requestBody = new StringContent(request, Encoding.UTF8, "application/json");
            //_httpClient.DefaultRequestHeaders.Add("Authorization", _baseConfig.HttpClientConfig.Token);

            var msgBody = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_baseConfig.HttpClientConfig.BaseUrl}{endpoint}"),
                Content = requestBody
            };

            _response = _context.HttpClient.SendAsync(msgBody).Result;
        }

        [Then(@"The user should be created successfully")]
        public void ThenTheUserShouldBeCreatedSuccessfully()
        {
            var actualresponse = JsonConvert.DeserializeObject<GoRestUser>(_response.Content.ReadAsStringAsync().Result);

            _context.UserId = actualresponse.Id;

            using (new AssertionScope())
            {
                actualresponse.Id.Should().NotBe(null);
                actualresponse.Name.Should().Be(_user.Name);
            }
        }

        //[Given(@"I want to show the hooks")]
        //public void GivenIWantToShowTheHooks()
        //{
        //    throw new PendingStepException();
        //}

        //[When(@"I execute the scenario")]
        //public void WhenIExecuteTheScenario()
        //{
        //    throw new PendingStepException();
        //}

        //[Then(@"Everyone will see the hooks")]
        //public void ThenEveryoneWillSeeTheHooks()
        //{
        //    throw new PendingStepException();
        //}



    }
}