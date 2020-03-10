using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models
{
    public class ProjectSearchForTable
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public int Page { get; set; }
        public string OrderBy { get; set; }
    }
}
