using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public enum Status
    {
        ToDo,
        InProgress,
        Reviewing,
        Done
    }

    public class Issue : IEntityBase
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}