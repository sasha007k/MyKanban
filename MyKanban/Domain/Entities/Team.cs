using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Team : IEntityBase
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public Team()
        {

        }
    }
}
