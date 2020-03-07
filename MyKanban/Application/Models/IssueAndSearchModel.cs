using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models
{
    public class IssueAndSearchModel
    {
        public IssueModel IssueModel { get; set; }

        public SearchUserModel SearchUserModel { get; set; }
        public int ProjectId { get; set; }
    }
}
