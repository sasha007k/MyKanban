using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models
{
    public class IssueModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public int ProjectId { get; set; }

        public string UserId { get; set; }
    }
}
