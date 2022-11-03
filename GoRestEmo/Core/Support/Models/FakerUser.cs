using Bogus;
using static Bogus.DataSets.Name;

namespace GoRestEmo.Core.Support.Models
{
    public class FakerUser
    {
        public  User CreateNewUser()
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
