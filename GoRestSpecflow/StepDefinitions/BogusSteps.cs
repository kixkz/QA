using Bogus;
using GoRestSpecflow.Support;
using static Bogus.DataSets.Name;

namespace GoRestSpecflow.StepDefinitions
{
    [Binding]
    public class BogusSteps
    {
        [Given(@"I want to create new user request body")]
        public void GivenIWantToCreateNewUserRequestBody()
        {
            var fakerUser = new Faker<GoRestUser>()
                .RuleFor(u => u.Gender, f => f.PickRandom<Gender>())
                .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName(u.Gender))
                .RuleFor(u => u.LastName, (f, u) => f.Name.LastName(u.Gender))
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                .RuleFor(u => u.Status, f => f.PickRandom<Status>().ToString());

            var requestBody = fakerUser.Generate();
        }

    }
}
