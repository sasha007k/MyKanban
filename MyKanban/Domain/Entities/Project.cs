using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Project : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public User User { get; set; }
        public string UserId { get; set; }

        public ICollection<Issue> Issues { get; set; } = new HashSet<Issue>();
    }
}
