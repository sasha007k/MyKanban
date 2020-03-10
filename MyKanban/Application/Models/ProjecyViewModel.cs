using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models
{
    public class ProjectViewModel
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Issue> Issuess { get; set; } = new List<Issue>();
    }
}
