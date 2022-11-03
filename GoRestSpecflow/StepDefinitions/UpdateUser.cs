using System.Net;
using System.Text;
using GoRestSpecflow.Support;
using Newtonsoft.Json;

namespace GoRestSpecflow.StepDefinitions
{
    [Binding]
    public class UpdateUser
    {
        private HttpClient _httpClient = new HttpClient();
        private readonly BaseConfig _baseConfig;
        private int _id;

        public UpdateUser(BaseConfig baseConfig)
        {
            _baseConfig = baseConfig;
            _httpClient.DefaultRequestHeaders.Add("Authorization", _baseConfig.HttpClientConfig.Token);
        }

        [Given(@"I have a created user already")]
        public void GivenIHaveACreatedUserAlready()
        {
            var response = CreateUser();
            var userToUpdate = JsonConvert.DeserializeObject<GoRestUser>(response.Content.ReadAsStringAsync().Result);
            _id = userToUpdate.Id;
        }

        [When(@"I send update request to the users endpoint")]
        public void WhenISendUpdateRequestToTheUsersEndpoint()
        {
            var updateUser = new GoRestUser
            {
                Name = "asfsesgffhhfffhtsdgdfh",
                Email = "asstgtttdfgh@sdfgtydf.com",
                //Gender = "male",
                Status = "active"
            };

            var requestBody = new StringContent(JsonConvert.SerializeObject(updateUser), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{_baseConfig.HttpClientConfig.BaseUrl}users/{_id}"),
                Content = requestBody
            };

            var updateUserResponse = _httpClient.SendAsync(request).Result;
        }

        [Then(@"The user should be updated successfully")]
        public void ThenTheUserShouldBeUpdatedSuccessfully()
        {
            throw new PendingStepException();
        }

        private HttpResponseMessage CreateUser()
        {
            var user = new GoRestUser
            {
                Name = "asftsfgfdgdfh",
                Email = "assdfffgh@sdfgtydf.com",
                //Gender = "male",
                Status = "active"
            };

            var requestBody = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_baseConfig.HttpClientConfig.BaseUrl}users"),
                Content = requestBody
            };

            return _httpClient.SendAsync(request).Result;
        }
    }
}
