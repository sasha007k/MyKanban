using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser, IEntityBase
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public int AmountOfDoneIssues { get; set; }
    }
}