using System.Text;
using Bogus;
using FluentAssertions.Execution;
using GoRestEmo.Core.Config;
using GoRestEmo.Core.ContextContainers;
using GoRestEmo.Core.Support.Models;
using Newtonsoft.Json;

namespace GoRestEmo.Tests.StepDefinitions
{
    [Binding]
    public sealed class Steps
    {
        private readonly BaseConfig _baseConfig;
        private HttpResponseMessage _response;
        private User _user;
        private NewUser _newUser;
        private TestContextContainer _contextContainer;
        

        public Steps(BaseConfig baseConfig, TestContextContainer contextContainer)
        {
            _baseConfig = baseConfig;
            _contextContainer = contextContainer;
        }

        [Given(@"I want to prepare a request")]
        public void GivenIWantToPrepareARequest()
        {
            _contextContainer.HttpClient.DefaultRequestHeaders.Add("Authorization", _baseConfig.HttpClientConfig.Token);
        }

        [When(@"I get all users from the (.*) endpoint")]
        public void WhenIGetAllUsersFromTheUsersEndpoint(string endpoint)
        {
            _response = _contextContainer.HttpClient.GetAsync($"{_baseConfig.HttpClientConfig.BaseUrl}{endpoint}").Result;
        }

        [Then(@"Response status code should be (.*)")]
        public void ThenResponseStatusCodeShouldBeOK(string statusCode)
        {
            _response.StatusCode.ToString().Should().Be(statusCode);
        }

        [Then(@"the response should contains a list of users")]
        public void ThenTheResponseShouldContainsAListOfUsers()
        {
            var content = _response.Content.ReadAsStringAsync().Result;
            var expectedResponse = JsonConvert.DeserializeObject<List<User>>(content);

            expectedResponse.Should().NotBeEmpty();
        }

        [Given(@"I have the following user data")]
        public void GivenIHaveTheFollowingUserData()
        {
            _user = CreateNewUser();
        }

        [When(@"I send a request to the (.*) endpoint")]
        public void WhenISendARequestToTheUsersEndpoint(string endpoint)
        {
            var request = JsonConvert.SerializeObject(_user);
            var requestBody = new StringContent(request, Encoding.UTF8, "application/json");

            var msgBody = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_baseConfig.HttpClientConfig.BaseUrl}{endpoint}"),
                Content = requestBody
            };

            _response = _contextContainer.HttpClient.SendAsync(msgBody).Result;
        }

        [Then(@"The user should be created successfully")]
        public void ThenTheUserShouldBeCreatedSuccessfully()
        {
            var actualresponse = JsonConvert.DeserializeObject<User>(_response.Content.ReadAsStringAsync().Result);

        }

        [Given(@"I have a created user already")]
        public void GivenIHaveACreatedUserAlready()
        {
            throw new PendingStepException();
        }

        [When(@"I send update request to the users endpoint")]
        public void WhenISendUpdateRequestToTheUsersEndpoint()
        {
            throw new PendingStepException();
        }

        [Then(@"The user should be updated successfully")]
        public void ThenTheUserShouldBeUpdatedSuccessfully()
        {
            throw new PendingStepException();
        }

        [When(@"I send a delete request to the users endpoint")]
        public void WhenISendADeleteRequestToTheUsersEndpoint()
        {
            throw new PendingStepException();
        }

        [Then(@"The delete request status code should be No Content")]
        public void ThenTheDeleteRequestStatusCodeShouldBeNoContent()
        {
            throw new PendingStepException();
        }




        private User CreateNewUser()
        {
            var fakerUser = new Faker<User>()
                .RuleFor(u => u.Name, (f, u) => f.Name.FullName())
                .RuleFor(u => u.Gender, f => f.PickRandom<Gender>().ToString())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email())
                .RuleFor(u => u.Status, f => f.PickRandom<Status>().ToString());

            return fakerUser.Generate();
        }

    }

}
