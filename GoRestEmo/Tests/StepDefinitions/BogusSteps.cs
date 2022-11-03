using Bogus;
using GoRestEmo.Core.Support.Models;

namespace GoRestEmo.Tests.StepDefinitions
{
    [Binding]
    public class BogusSteps
    {
        [Given(@"I want to create new user request body")]
        public void GivenIWantToCreateNewUserRequestBody()
        {
            var fakerUser = new Faker<User>()
                .RuleFor(u => u.Name, (f, u) => f.Name.FullName())
                .RuleFor(u => u.Gender, f => f.PickRandom<Gender>().ToString())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email())
                .RuleFor(u => u.Status, f => f.PickRandom<Status>().ToString());

            var requestBody = fakerUser.Generate();
        }

    }
}
